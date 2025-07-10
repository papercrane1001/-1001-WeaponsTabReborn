using System;
using System.Text;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace WeaponsTabReborn
{

	public class LoadoutDatabase : IExposable
	{
		private List<Loadout> loadouts = new List<Loadout>();

		public List<Loadout> AllLoadouts => loadouts;

		public LoadoutDatabase()
		{
			GenerateStartingLoadouts();
		}

		public void ExposeData()
		{
			Scribe_Collections.Look(ref loadouts, "loadouts", LookMode.Deep);
		}

		public Loadout DefaultLoadout()
		{
			if (loadouts.Count == 0)
			{
				MakeNewLoadout();
			}
			return loadouts[0];
		}

		public AcceptanceReport TryDelete(Loadout loadout)
		{
			foreach (Pawn item in PawnsFinder.AllMapsCaravansAndTravelingTransportPods_Alive)
			{
				var loadouts = LoadoutDictionary.GetLoadoutTracker(item);
				if (loadouts != null && loadouts.CurrentLoadout == loadout)
				{
					return new AcceptanceReport("LoadoutInUse".Translate(item));
				}
			}
			foreach (Pawn item2 in PawnsFinder.AllMapsWorldAndTemporary_AliveOrDead)
			{
				var loadouts = LoadoutDictionary.GetLoadoutTracker(item2);
				if (loadouts != null && loadouts.CurrentLoadout == loadout)
				{
					loadouts.CurrentLoadout = null;
				}
			}
			loadouts.Remove(loadout);
			return AcceptanceReport.WasAccepted;
		}

		public Loadout MakeNewLoadout()
		{
			int uniqueID = (!loadouts.Any()) ? 1 : (loadouts.Max((Loadout o) => o.uniqueID) + 1);
			Loadout loadout = new Loadout(uniqueID, "Loadout".Translate() + " " + uniqueID.ToString());
			loadout.filter.SetAllow(ThingCategoryDefOf.Weapons, allow: true);
			loadouts.Add(loadout);
			return loadout;
		}

		private void GenerateStartingLoadouts()
		{
			MakeNewLoadout().label = "LoadoutAnything".Translate();
			Loadout loadout2 = MakeNewLoadout();
			loadout2.label = "LoadoutPacifist".Translate();
			loadout2.filter.SetDisallowAll();
		}
	}
}

