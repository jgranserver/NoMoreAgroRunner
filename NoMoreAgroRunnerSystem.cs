using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace NoMoreAgroRunner
{
    public class NoMoreAgroRunnerSystem : ModSystem
    {
        public override void OnModLoad()
        {
            ModContent.GetInstance<NoMoreAgroRunnerConfig>();
        }
    }
}