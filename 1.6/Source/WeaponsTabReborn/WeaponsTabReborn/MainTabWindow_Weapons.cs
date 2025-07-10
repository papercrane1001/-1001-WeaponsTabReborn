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
    public class MainTabWindow_Weapons : MainTabWindow_PawnTable
    {
        protected override PawnTableDef PawnTableDef => PawnTableDefOf.WeaponsTab;

        public override void DoWindowContents(Rect rect)
        {
            base.DoWindowContents(rect);
        }
        public override void PostOpen()
        {
            base.PostOpen();
            Find.World.renderer.wantedMode = RimWorld.Planet.WorldRenderMode.None;
        }
    }
}
