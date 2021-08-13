using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace WeaponsTabReborn
{
	public class Pawn_LoadoutTracker : IExposable
	{
		public Pawn pawn;

		public int nextEquipLoadoutTick;

		private Loadout curLoadout;

		public Loadout CurrentLoadout
		{
			get
			{
				if (curLoadout == null)
				{
					curLoadout = LoadoutDictionary.GetCurrentLoadoutDatabase().DefaultLoadout();
				}
				return curLoadout;
			}
			set
			{
				if (curLoadout != value)
				{
					curLoadout = value;
					nextEquipLoadoutTick = Find.TickManager.TicksGame;
				}
			}
		}

		public Pawn_LoadoutTracker()
		{
		}

		public Pawn_LoadoutTracker(Pawn pawn)
		{
			this.pawn = pawn;
		}

		public void ExposeData()
		{
			Scribe_References.Look(ref curLoadout, "curLoadout");
		}
	}
}
