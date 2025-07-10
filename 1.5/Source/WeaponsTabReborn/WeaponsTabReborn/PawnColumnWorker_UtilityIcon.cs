using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using System.Reflection;

namespace WeaponsTabReborn
{
    [StaticConstructorOnStartup]
    public class PawnColumnWorker_UtilityIcon : PawnColumnWorker_Icon
    {
        protected override Texture2D GetIconFor(Pawn pawn)
        {
            var utility = pawn?.apparel?.WornApparel.FirstOrDefault(apparel => apparel.def.apparel.layers.Any(layer => layer.IsUtilityLayer));
            return utility?.def?.uiIcon;
        }
    }
}
