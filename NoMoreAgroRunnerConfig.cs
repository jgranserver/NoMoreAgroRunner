using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Terraria.ModLoader.Config;

namespace NoMoreAgroRunner
{
    public class NoMoreAgroRunnerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Enable Debug Warning")]
        [DefaultValue(false)]
        public bool DebugWarning { get; set; }

        [Label("Maximum Distance (in tiles)")]
        [Tooltip("The maximum allowed distance (in tiles) between players during a boss fight.")]
        [Range(62.5f, 312.5f)] // Converted from pixels to tiles (1000 / 16 to 5000 / 16)
        [DefaultValue(212.5f)] // Converted from 3400 pixels to tiles (3400 / 16)
        public float MaxDistanceInTiles { get; set; }

        [Label("Warning Distance (in tiles)")]
        [Tooltip("The distance (in tiles) at which a warning will be shown to players.")]
        [Range(31.25f, 156.25f)] // Converted from pixels to tiles (500 / 16 to 2500 / 16)
        [DefaultValue(106.25f)] // Converted from 1700 pixels to tiles (1700 / 16)
        public float WarningDistanceInTiles { get; set; }

        public static NoMoreAgroRunnerConfig Instance;

        public override void OnLoaded()
        {
            Instance = this;
        }
    }
}