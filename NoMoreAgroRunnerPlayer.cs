using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoMoreAgroRunner
{
	public class NoMoreAgroRunnerPlayer : ModPlayer
	{

		public static bool DebugMode { get; set; }

		public float DistanceToNearestPlayer { get; set; }
		public bool ShowWarning { get; set; }
		public float WarningTimer { get; set; }
		private int warningSoundCooldown;
		private const int WarningSoundCooldownMax = 300;

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (NoMoreAgroRunner.ToggleDebugKey.JustPressed)
			{
				DebugMode = !DebugMode;
				Main.NewText("Debug mode: " + (DebugMode ? "Enabled" : "Disabled"), 255, 255, 0);
			}
		}

		public override void PostUpdate()
		{
			base.PostUpdate();

			if (Main.netMode == NetmodeID.MultiplayerClient || DebugMode)
			{
				float nearestDistance = float.MaxValue;
				Player nearestPlayer = null;
				bool isBossTarget = false;

				if (!DebugMode)
				{
					foreach (Player player in Main.player)
					{
						if (player.active && player.whoAmI != Player.whoAmI)
						{
							foreach (NPC npc in Main.npc)
							{
								if (npc.active && npc.boss && npc.target == player.whoAmI)
								{
									isBossTarget = true;
									float distance = Vector2.Distance(Player.Center, player.Center);
									if (distance < nearestDistance)
									{
										nearestDistance = distance;
										nearestPlayer = player;
									}
								}
							}
						}
					}

					DistanceToNearestPlayer = nearestDistance;

					if (nearestPlayer != null && isBossTarget)
					{
						float warningDistanceInPixels = NoMoreAgroRunnerConfig.Instance.WarningDistanceInTiles * 16;
						float maxDistanceInPixels = NoMoreAgroRunnerConfig.Instance.MaxDistanceInTiles * 16;

						if (nearestDistance > warningDistanceInPixels)
						{
							ShowWarning = true;
							WarningTimer += 0.2f; // Increment timer faster for quicker pulsing effect

							// Play sound if cooldown is zero and based on distance
							if (warningSoundCooldown <= 0)
							{
								if (nearestDistance > maxDistanceInPixels * 0.75f)
								{
									SoundEngine.PlaySound(NoMoreAgroRunner.WarningSound3);
								}
								else if (nearestDistance > maxDistanceInPixels * 0.5f)
								{
									SoundEngine.PlaySound(NoMoreAgroRunner.WarningSound2);
								}
								else
								{
									SoundEngine.PlaySound(NoMoreAgroRunner.WarningSound1);
								}

								warningSoundCooldown = WarningSoundCooldownMax;
							}
						}
						else
						{
							// Reset the warning when the player is in range
							ShowWarning = false;
							WarningTimer = 0f;
						}
					}
					else
					{
						ShowWarning = false;
						WarningTimer = 0f;
					}
				}
				else // DebugMode is enabled
				{
					nearestDistance = 0f; // In debug mode, set the nearest distance to 0 or any other value you need for testing
					DistanceToNearestPlayer = nearestDistance;

					ShowWarning = true;
					WarningTimer += 0.2f; // Increment timer faster for quicker pulsing effect

					// Play sound if cooldown is zero
					if (warningSoundCooldown <= 0)
					{
						SoundEngine.PlaySound(NoMoreAgroRunner.WarningSound1);
						warningSoundCooldown = WarningSoundCooldownMax;
					}
				}
			}
			else
			{
				// Reset the warning when neither multiplayer nor debug mode is active
				ShowWarning = false;
				WarningTimer = 0f;
			}

			// Decrement cooldown timer if it's greater than zero
			if (warningSoundCooldown > 0)
			{
				warningSoundCooldown--;
			}
		}
	}
}
