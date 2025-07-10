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
    public class PawnColumnWorker_WeaponHealth : PawnColumnWorker_Text
    {
        protected override string GetTextFor(Pawn pawn)
        {
            var weapon = pawn?.equipment?.Primary;

            if (weapon == null) return null;

            float weaponHealth = (float)weapon.HitPoints / (float)weapon.MaxHitPoints;

            return weaponHealth.ToStringPercent();
        }
    }
}
