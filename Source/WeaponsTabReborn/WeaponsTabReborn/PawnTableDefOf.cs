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
    [DefOf]
    public static class PawnTableDefOf
    {
        public static PawnTableDef WeaponsTab;

        static PawnTableDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PawnTableDefOf));
        }
    }
}
