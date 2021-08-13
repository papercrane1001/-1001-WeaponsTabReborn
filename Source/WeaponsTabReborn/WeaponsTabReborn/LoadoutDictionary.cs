using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Verse;

namespace WeaponsTabReborn
{
    public static class LoadoutDictionary
    {
        public static LoadoutDatabase CurrentLoadoutDatabase;

        private static Dictionary<Pawn, VariableReference<Pawn_LoadoutTracker>> LoadoutRefsByPawn = new Dictionary<Pawn, VariableReference<Pawn_LoadoutTracker>>();

        public static LoadoutDatabase GetCurrentLoadoutDatabase()
        {
            CurrentLoadoutDatabase = CurrentLoadoutDatabase ?? new LoadoutDatabase();
            return CurrentLoadoutDatabase;
        }

        public static Pawn_LoadoutTracker GetLoadoutTracker(Pawn pawn)
        {
            if (!LoadoutRefsByPawn.ContainsKey(pawn) || LoadoutRefsByPawn.TryGetValue(pawn) == null)
            {
                var loadout = new Pawn_LoadoutTracker(pawn);
                var newLoadoutRef = new VariableReference<Pawn_LoadoutTracker>(loadout);
                LoadoutRefsByPawn.SetOrAdd(pawn, newLoadoutRef);
                return loadout;
            }

            var loadoutRef = LoadoutRefsByPawn.TryGetValue(pawn);
            if (loadoutRef.Get() == null)
            {
                var loadout = new Pawn_LoadoutTracker(pawn);
                loadoutRef.Set(loadout);

                return loadout;
            }

            return LoadoutRefsByPawn.TryGetValue(pawn).Get();
        }

        public static VariableReference<Pawn_LoadoutTracker> GetLoadoutTrackerRef(Pawn pawn)
        {
            if (!LoadoutRefsByPawn.ContainsKey(pawn) || LoadoutRefsByPawn.TryGetValue(pawn) == null)
            {
                var loadout = new Pawn_LoadoutTracker(pawn);
                var loadoutRef = new VariableReference<Pawn_LoadoutTracker>(loadout);
                LoadoutRefsByPawn.SetOrAdd(pawn, loadoutRef);
                return loadoutRef;
            }

            return LoadoutRefsByPawn.TryGetValue(pawn);
        }
    }
}
