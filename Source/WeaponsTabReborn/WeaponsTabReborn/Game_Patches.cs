using System;
using HarmonyLib;
using Verse;

namespace WeaponsTabReborn
{
    [HarmonyPatch(typeof(Game), "ExposeSmallComponents")]
    static class Game_ExposeSmallComponents_Patch
    {
        public static void Prefix(Game __instance, ref Action __state)
        {
            Scribe_Deep.Look(ref LoadoutDictionary.CurrentLoadoutDatabase, "loadoutDatabase");
        }
    }

    [HarmonyPatch(typeof(Game))]
    [HarmonyPatch(MethodType.Constructor)]
    static class Game_Constructor_Patch
    {
        public static void Postfix(Game __instance, Action __state)
        {
            LoadoutDictionary.CurrentLoadoutDatabase = new LoadoutDatabase();
        }
    }
}
