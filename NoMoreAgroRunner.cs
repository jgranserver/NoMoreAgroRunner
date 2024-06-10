using Terraria.ModLoader;
using Terraria.UI;
using Terraria.GameInput;
using Terraria;
using Terraria.Audio;
using System;

namespace NoMoreAgroRunner
{
	public class NoMoreAgroRunner : Mod
	{
		internal static NoMoreAgroRunner Instance;
		private UserInterface warningInterface;
		private WarningUI warningUI;
		internal static ModKeybind ToggleDebugKey;

		public override void Load()
		{
			try
			{
				Instance = this;
				warningUI = new WarningUI();
				warningInterface = new UserInterface();
				warningInterface.SetState(warningUI);

				On_Main.DrawInterface_Resources_Buffs += Main_DrawPlayers;
			}
			catch (Exception e)
			{
				Logger.Error($"Error loading NoMoreAgroRunner: {e}");
			}
			ToggleDebugKey = KeybindLoader.RegisterKeybind(this, "Toggle Debug", "P"); // Default key is 'P'
		}

		public override void Unload()
		{
			ToggleDebugKey = null;
			Instance = null;
		}

		private void Main_DrawPlayers(On_Main.orig_DrawInterface_Resources_Buffs orig, Main self)
		{
			var player = Main.LocalPlayer.GetModPlayer<NoMoreAgroRunnerPlayer>();
			if (player.ShowWarning)
			{
				try
				{
					warningUI.UpdateWarning(player.DistanceToNearestPlayer, player.WarningTimer);
					warningInterface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
				}
				catch (Exception e)
				{
					Logger.Error($"Error drawing WarningUI: {e}");
				}
			}
			orig(self);
		}
	}
}
