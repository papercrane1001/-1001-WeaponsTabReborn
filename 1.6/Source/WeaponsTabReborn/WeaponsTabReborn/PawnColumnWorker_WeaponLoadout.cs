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
	public class PawnColumnWorker_WeaponLoadout : PawnColumnWorker
	{
		public const int TopAreaHeight = 65;

		public const int ManageLoadoutButtonHeight = 32;

		public override void DoHeader(Rect rect, PawnTable table)
		{
			base.DoHeader(rect, table);
			Verse.Sound.MouseoverSounds.DoRegion(rect);
			Rect rect2 = new Rect(rect.x, rect.y + (rect.height - 65f), Mathf.Min(rect.width, 360f), 32f);
			if (Widgets.ButtonText(rect2, "ManageLoadouts".Translate()))
			{
				Find.WindowStack.Add(new Dialog_ManageLoadouts(null));
			}
			UIHighlighter.HighlightOpportunity(rect2, "ManageLoadouts");
		}

		public override void DoCell(Rect rect, Pawn pawn, PawnTable table)
		{
			var tracker = LoadoutDictionary.GetLoadoutTracker(pawn);
			if (tracker == null)
			{
				return;
			}
			int num = Mathf.FloorToInt((rect.width - 4f) * 0.714285731f);
			int num2 = Mathf.FloorToInt((rect.width - 4f) * 0.2857143f);
			float x = rect.x;
			Rect rect2 = new Rect(x, rect.y + 2f, num, rect.height - 4f);
			if (pawn.IsQuestLodger())
			{
				Text.Anchor = TextAnchor.MiddleCenter;
				Widgets.Label(rect2, "Unchangeable".Translate().Truncate(rect2.width));
				TooltipHandler.TipRegionByKey(rect2, "QuestRelated_Loadout");
				Text.Anchor = TextAnchor.UpperLeft;
			}
			else
			{
				Widgets.Dropdown(rect2, pawn, (Pawn p) => LoadoutDictionary.GetLoadoutTracker(pawn).CurrentLoadout, Button_GenerateMenu, tracker.CurrentLoadout.label.Truncate(rect2.width), null, tracker.CurrentLoadout.label, null, null, paintable: true);
			}
			x += rect2.width;
			x += 4f;
			Rect rect3 = new Rect(x, rect.y + 2f, num2, rect.height - 4f);
			Rect rect4 = new Rect(x, rect.y + 2f, num2, rect.height - 4f);
			if (!pawn.HasExtraHomeFaction() && Widgets.ButtonText(rect4, "AssignTabEdit".Translate()))
			{
				Find.WindowStack.Add(new Dialog_ManageLoadouts(tracker.CurrentLoadout));
			}
			x += (float)num2;
		}

		private IEnumerable<Widgets.DropdownMenuElement<Loadout>> Button_GenerateMenu(Pawn pawn)
		{
			foreach (Loadout loadout in LoadoutDictionary.GetCurrentLoadoutDatabase().AllLoadouts)
			{
				yield return new Widgets.DropdownMenuElement<Loadout>
				{
					option = new FloatMenuOption(loadout.label, delegate
					{
						LoadoutDictionary.GetLoadoutTracker(pawn).CurrentLoadout = loadout;
					}),
					payload = loadout
				};
			}
		}

		public override int GetMinWidth(PawnTable table)
		{
			return Mathf.Max(base.GetMinWidth(table), Mathf.CeilToInt(194f));
		}

		public override int GetOptimalWidth(PawnTable table)
		{
			return Mathf.Clamp(Mathf.CeilToInt(251f), GetMinWidth(table), GetMaxWidth(table));
		}

		public override int GetMinHeaderHeight(PawnTable table)
		{
			return Mathf.Max(base.GetMinHeaderHeight(table), 65);
		}

		public override int Compare(Pawn a, Pawn b)
		{
			return GetValueToCompare(a).CompareTo(GetValueToCompare(b));
		}

		private int GetValueToCompare(Pawn pawn)
		{
			var tracker = LoadoutDictionary.GetLoadoutTracker(pawn);
			if (tracker != null && tracker.CurrentLoadout != null)
			{
				return tracker.CurrentLoadout.uniqueID;
			}
			return int.MinValue;
		}
	}
}
