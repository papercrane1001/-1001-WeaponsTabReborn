using System;
using System.Text;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace WeaponsTabReborn
{

	public class SubLoadoutDatabase : IExposable
	{
		private List<SubLoadout> subLoadouts = new List<SubLoadout>();

		public List<SubLoadout> AllSubLoadouts => subLoadouts;

		public SubLoadoutDatabase()
		{
			GenerateStartingSubLoadouts();
		}

		public void ExposeData()
		{
			Scribe_Collections.Look(ref subLoadouts, "subLoadouts", LookMode.Deep);
		}

		public SubLoadout DefaultLoadout()
		{
			if (subLoadouts.Count == 0)
			{
				MakeNewSubLoadout();
			}
			return subLoadouts[0];
		}

		public AcceptanceReport TryDelete(SubLoadout subLoadout)
		{
			subLoadouts.Remove(subLoadout);
			return AcceptanceReport.WasAccepted;
		}

		public SubLoadout MakeNewSubLoadout()
		{
			int uniqueID = (!subLoadouts.Any()) ? 1 : (subLoadouts.Max((SubLoadout o) => o.uniqueID) + 1);
			SubLoadout subLoadout = new SubLoadout(uniqueID, "SubLoadout".Translate() + " " + uniqueID.ToString());
			subLoadout.filter.SetAllow(ThingCategoryDefOf.Weapons, allow: true);
			subLoadouts.Add(subLoadout);
			return subLoadout;
		}

		private void GenerateStartingSubLoadouts()
		{
			MakeNewSubLoadout().label = "SubLoadoutAnything".Translate();
			SubLoadout subLoadout2 = MakeNewSubLoadout();
			subLoadout2.label = "SubLoadoutPacifist".Translate();
			subLoadout2.filter.SetDisallowAll();

			//SubLoadout subLoadout3 = MakeNewSubLoadout();
			//subLoadout3.label = "SubLoadoutRanged".Translate();
			//subLoadout3.filter


		}
	}
}

