using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Verse;

using RimWorld;


namespace WeaponsTabReborn
{
    public class Loadout : IExposable, ILoadReferenceable
    {
        public int uniqueID;
        public string label;
        //public ThingFilter filter = new ThingFilter();
        public List<ThingFilter> allFilters = new List<ThingFilter>(); 
        public List<ThingFilter> oneFilters = new List<ThingFilter>();


        public static readonly Regex ValidNameRegex = new Regex("^[\\p{L}0-9 '\\-]*$");

        public Loadout()
        {

        }

        public Loadout(int _uniqueID, string _label)
        {
            uniqueID = _uniqueID;
            label = _label;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref uniqueID, "uniqueID", 0);
            Scribe_Values.Look(ref label, "label");
            //Scribe_Deep.Look(ref filter, "filter");
            Scribe_Deep.Look(ref allFilters, "allFilters");
            Scribe_Deep.Look(ref oneFilters, "oneFilters");
        }

        public string GetUniqueLoadID()
        {
            return "Loadout_" + label + uniqueID.ToString();
        }
    }
}
