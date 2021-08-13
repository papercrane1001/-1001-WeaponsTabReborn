using System;
using HarmonyLib;
using Verse;

namespace WeaponsTabReborn
{
    [HarmonyPatch(typeof(Pawn), "ExposeData")]
    static class Pawn_ExposeData_Patch
    {
        public static void Postfix(Pawn __instance, ref Action __state)
        {
            var trackerRef = LoadoutDictionary.GetLoadoutTrackerRef(__instance);
            Scribe_Deep.Look(ref trackerRef.Val, "loadouts", __instance);
        }
    }
}
