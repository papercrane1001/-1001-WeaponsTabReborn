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
    }
}
