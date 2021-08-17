using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace WeaponsTabReborn
{
    class Dialog_ManageSubLoadouts : Window
    {
		public ThingFilterUI.UIState uiState = new ThingFilterUI.UIState();

		private Vector2 scrollPosition;

		private SubLoadout selSubLoadoutInt;

		public const float TopAreaHeight = 40f;

		public const float TopButtonHeight = 35f;

		public const float TopButtonWidth = 150f;

		private static ThingFilter weaponGlobalFilter;

		private SubLoadout SelectedSubLoadout
		{
			get
			{
				return selSubLoadoutInt;
			}
			set
			{
				CheckSelectedLoadoutHasName();
				selSubLoadoutInt = value;
			}
		}

		private void CheckSelectedLoadoutHasName()
		{
			if (SelectedSubLoadout != null && SelectedSubLoadout.label.NullOrEmpty())
			{
				SelectedSubLoadout.label = "Unnamed";
			}
		}

		public override Vector2 InitialSize => new Vector2(700f, 700f);

		public Dialog_ManageSubLoadouts(SubLoadout selectedSubLoadout, ThingFilter subLoadout)
		{
			forcePause = true;
			doCloseX = true;
			doCloseButton = true;
			closeOnClickedOutside = true;
			absorbInputAroundWindow = true;
			if (weaponGlobalFilter == null)
			{
				weaponGlobalFilter = new ThingFilter();
				weaponGlobalFilter.SetAllow(ThingCategoryDefOf.Weapons, allow: true);
			}
			SelectedSubLoadout = selectedSubLoadout;
		}

		public override void DoWindowContents(Rect inRect)
		{
			float num = 0f;
			Rect rect = new Rect(0f, 0f, 150f, 35f);
			num += 150f;
			if (Widgets.ButtonText(rect, "SelectSubLoadout".Translate()))
			{
				List<FloatMenuOption> list = new List<FloatMenuOption>();
				foreach (SubLoadout allSubLoadout in SubLoadoutDictionary.GetCurrentSubLoadoutDatabase().AllSubLoadouts)
				{
					SubLoadout localOut = allSubLoadout;
					list.Add(new FloatMenuOption(localOut.label, delegate
					{
						SelectedSubLoadout = localOut;
					}));
				}
				Find.WindowStack.Add(new FloatMenu(list));
			}
			num += 10f;
			Rect rect2 = new Rect(num, 0f, 150f, 35f);
			num += 150f;
			if (Widgets.ButtonText(rect2, "NewLoadout".Translate()))
			{
				SelectedSubLoadout = SubLoadoutDictionary.GetCurrentSubLoadoutDatabase().MakeNewSubLoadout();
			}
			num += 10f;
			Rect rect3 = new Rect(num, 0f, 150f, 35f);
			num += 150f;
			if (Widgets.ButtonText(rect3, "DeleteLoadout".Translate()))
			{
				List<FloatMenuOption> list2 = new List<FloatMenuOption>();
				foreach (SubLoadout allLoadout2 in SubLoadoutDictionary.GetCurrentSubLoadoutDatabase().AllSubLoadouts)
				{
					SubLoadout localOut2 = allLoadout2;
					list2.Add(new FloatMenuOption(localOut2.label, delegate
					{
						AcceptanceReport acceptanceReport = SubLoadoutDictionary.GetCurrentSubLoadoutDatabase().TryDelete(localOut2);
						if (!acceptanceReport.Accepted)
						{
							Messages.Message(acceptanceReport.Reason, MessageTypeDefOf.RejectInput, historical: false);
						}
						else if (localOut2 == SelectedSubLoadout)
						{
							SelectedSubLoadout = null;
						}
					}));
				}
				Find.WindowStack.Add(new FloatMenu(list2));
			}
			Rect rect4 = new Rect(0f, 40f, inRect.width, inRect.height - 40f - CloseButSize.y).ContractedBy(10f);
			if (SelectedSubLoadout == null)
			{
				GUI.color = Color.grey;
				Text.Anchor = TextAnchor.MiddleCenter;
				Widgets.Label(rect4, "NoLoadoutSelected".Translate());
				Text.Anchor = TextAnchor.UpperLeft;
				GUI.color = Color.white;
			}
			else
			{
				GUI.BeginGroup(rect4);
				DoNameInputRect(new Rect(0f, 0f, 200f, 30f), ref SelectedSubLoadout.label);
				//ThingFilterUI.UIState test = new ThingFilterUI.UIState();
				//uiState.scrollPosition = scrollPosition;

				ThingFilterUI.DoThingFilterConfigWindow(new Rect(0f, 40f, 300f, rect4.height - 45f - 10f), uiState, SelectedSubLoadout.filter, weaponGlobalFilter, 16, null);
				GUI.EndGroup();
			}
		}

		public override void PreClose()
		{
			base.PreClose();
			CheckSelectedLoadoutHasName();
		}

		public static void DoNameInputRect(Rect rect, ref string name)
		{
			name = Widgets.TextField(rect, name, 30, Loadout.ValidNameRegex);
		}
	}
}
