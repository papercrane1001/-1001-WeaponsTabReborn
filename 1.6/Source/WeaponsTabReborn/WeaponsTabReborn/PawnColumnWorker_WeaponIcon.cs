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
    public class PawnColumnWorker_WeaponIcon : PawnColumnWorker_Icon
    {
        protected override Texture2D GetIconFor(Pawn pawn)
        {
            return pawn?.equipment?.Primary?.def?.uiIcon;
        }
    }
}
