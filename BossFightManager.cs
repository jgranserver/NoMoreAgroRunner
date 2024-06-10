using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace NoMoreAgroRunner
{
    public class BossFightManager : ModSystem
    {
        public override void PostUpdatePlayers()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                CheckPlayerDistances();
            }
        }

        private void CheckPlayerDistances()
        {
            foreach (var npc in Main.npc)
            {
                if (npc.active && npc.boss)
                {
                    Player aggroedPlayer = Main.player[npc.target];

                    if (aggroedPlayer == null || !aggroedPlayer.active)
                    {
                        continue;
                    }

                    // Find the nearest player to the aggroed player
                    Player nearestPlayer = null;
                    float nearestDistance = float.MaxValue;

                    foreach (var player in Main.player)
                    {
                        if (player.active && player.whoAmI != aggroedPlayer.whoAmI)
                        {
                            float distance = Vector2.Distance(player.Center, aggroedPlayer.Center);
                            if (distance < nearestDistance)
                            {
                                nearestDistance = distance;
                                nearestPlayer = player;
                            }
                        }
                    }

                    var modPlayer = aggroedPlayer.GetModPlayer<NoMoreAgroRunnerPlayer>();

                    // Convert config values from tiles to pixels for comparison
                    float maxDistanceInPixels = NoMoreAgroRunnerConfig.Instance.MaxDistanceInTiles * 16;
                    float warningDistanceInPixels = NoMoreAgroRunnerConfig.Instance.WarningDistanceInTiles * 16;

                    // Warn the aggroed player if the nearest player is within the warning range
                    if (nearestPlayer != null && nearestDistance > warningDistanceInPixels)
                    {
                        modPlayer.ShowWarning = true;
                        modPlayer.DistanceToNearestPlayer = nearestDistance;
                    }
                    else
                    {
                        modPlayer.ShowWarning = false;
                    }

                    // If the nearest player is farther than the maximum allowed distance, kill the aggroed player
                    if (nearestPlayer != null && nearestDistance > maxDistanceInPixels)
                    {
                        aggroedPlayer.KillMe(PlayerDeathReason.ByCustomReason($"{aggroedPlayer.name} was too far from the fight!"), 1000.0, 0);
                    }
                }
            }
        }
    }
}
