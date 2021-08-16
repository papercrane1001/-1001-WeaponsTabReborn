using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Verse;

namespace WeaponsTabReborn
{
    public static class SubLoadoutDictionary
    {
        public static SubLoadoutDatabase CurrentSubLoadoutDatabase;

        private static Dictionary<Pawn, VariableReference<Pawn_LoadoutTracker>> SubLoadoutRefsByPawn = new Dictionary<Pawn, VariableReference<Pawn_LoadoutTracker>>();

        public static SubLoadoutDatabase GetCurrentSubLoadoutDatabase()
        {
            CurrentSubLoadoutDatabase = CurrentSubLoadoutDatabase ?? new SubLoadoutDatabase();
            return CurrentSubLoadoutDatabase;
        }

        public static Pawn_LoadoutTracker GetLoadoutTracker(Pawn pawn)
        {
            if (!SubLoadoutRefsByPawn.ContainsKey(pawn) || SubLoadoutRefsByPawn.TryGetValue(pawn) == null)
            {
                var subLoadout = new Pawn_SubLoadoutTracker(pawn);
                var newSubLoadoutRef = new VariableReference<Pawn_SubLoadoutTracker>(subLoadout);
                SubLoadoutRefsByPawn.SetOrAdd(pawn, newSubLoadoutRef);
                return subLoadout;
            }

            var subLoadoutRef = SubLoadoutRefsByPawn.TryGetValue(pawn);
            if (subLoadoutRef.Get() == null)
            {
                var subLoadout = new Pawn_SubLoadoutTracker(pawn);
                subLoadoutRef.Set(subLoadout);

                return subLoadout;
            }

            return SubLoadoutRefsByPawn.TryGetValue(pawn).Get();
        }

        public static VariableReference<Pawn_LoadoutTracker> GetLoadoutTrackerRef(Pawn pawn)
        {
            if (!SubLoadoutRefsByPawn.ContainsKey(pawn) || SubLoadoutRefsByPawn.TryGetValue(pawn) == null)
            {
                var subLoadout = new Pawn_SubLoadoutTracker(pawn);
                var loadoutRef = new VariableReference<Pawn_SubLoadoutTracker>(subLoadout);
                SubLoadoutRefsByPawn.SetOrAdd(pawn, subLoadoutRef);
                return subLoadoutRef;
            }

            return SubLoadoutRefsByPawn.TryGetValue(pawn);
        }
    }
}
