using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;

namespace WeaponsTabReborn
{
	class JobGiver_EquipLoadout : ThinkNode_JobGiver
	{
		private const int EquipLoadoutCheckIntervalMin = 6000;

		private const int EquipLoadoutCheckIntervalMax = 9000;

		private void SetNextEquipTick(Pawn pawn)
		{
			LoadoutDictionary.GetLoadoutTracker(pawn).nextEquipLoadoutTick = Find.TickManager.TicksGame + Rand.Range(EquipLoadoutCheckIntervalMin, EquipLoadoutCheckIntervalMax);
		}

		protected override Job TryGiveJob(Pawn pawn)
		{
			var tracker = LoadoutDictionary.GetLoadoutTracker(pawn);
			if (tracker == null)
			{
				Log.ErrorOnce(pawn + " tried to run JobGiver_EquipLoadout without an LoadoutTracker", 5643897);
				return null;
			}
			if (pawn.Faction != Faction.OfPlayer)
			{
				Log.ErrorOnce("Non-colonist " + pawn + " tried to equip loadout.", 764323);
				return null;
			}
			if (pawn.IsQuestLodger())
			{
				return null;
			}
			if (Find.TickManager.TicksGame < tracker.nextEquipLoadoutTick)
			{
				return null;
			}
			Loadout currentLoadout = tracker.CurrentLoadout;
			if (pawn.equipment.Primary != null && !currentLoadout.filter.Allows(pawn.equipment.Primary))
			{
				Job job = JobMaker.MakeJob(JobDefOf.DropEquipmentNoForbid, pawn.equipment.Primary);
				return job;
			}
			Thing thing = null;
			List<Thing> list = pawn.Map.listerThings.ThingsInGroup(ThingRequestGroup.Weapon);
			if (list.Count == 0)
			{
				SetNextEquipTick(pawn);
				return null;
			}
			QualityCategory currentQuality = QualityCategory.Awful, thingQuality = QualityCategory.Awful;
			if (pawn.equipment.Primary != null) pawn.equipment.Primary.TryGetQuality(out currentQuality);
			for (int j = 0; j < list.Count; j++)
			{
				var weapon = list[j];
				if (currentLoadout.filter.Allows(weapon) && weapon.IsInAnyStorage() 
					&& !weapon.IsForbidden(pawn) && !weapon.IsBurning())
				{
					//SpecialThingFilterWorker_BiocodedWeapons.
					if ((!CompBiocodable.IsBiocoded(weapon) || CompBiocodable.IsBiocodedFor(weapon, pawn)) && pawn.CanReserveAndReach(weapon, PathEndMode.OnCell, pawn.NormalMaxDanger()))
					{
						QualityCategory weaponQuality = QualityCategory.Awful;
						weapon.TryGetQuality(out weaponQuality);
						if (pawn.equipment.Primary == null || (weapon.def == pawn.equipment.Primary.def && (thing == null ? weaponQuality > currentQuality : weaponQuality > thingQuality)))
						{
							thingQuality = weaponQuality;
							thing = weapon;
						}
					}
				}
			}
			SetNextEquipTick(pawn);
			if (thing == null)
			{
				return null;
			}
			return JobMaker.MakeJob(RimWorld.JobDefOf.Equip, thing);
		}
	}
}
