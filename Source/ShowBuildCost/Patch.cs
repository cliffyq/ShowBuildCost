using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ShowBuildCost
{
    [StaticConstructorOnStartup]
    [HarmonyPatch(typeof(BuildableDef), nameof(BuildableDef.SpecialDisplayStats))]
    class Patch
    {
        static Patch()
        {
#if DEBUG
            Harmony.DEBUG = true;
#endif
            Harmony harmony = new Harmony("Cliffyq.ShowBuildCost");
            harmony.PatchAll();
        }

        static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> originalReturnValues, StatRequest req, BuildableDef __instance)
        {
            foreach (StatDrawEntry entry in originalReturnValues)
            {
                yield return entry;
            }
            List<ThingDefCountClass> list = __instance.CostListAdjusted(req.StuffDef, errorOnNullStuff: false);
            if (list.Count > 0)
            {
                var costString = String.Join(", ", list.Select(l => l.Summary));
                yield return new StatDrawEntry(StatCategoryDefOf.Basics, "Cost", costString, "Cost of this item.", 1100);
            }
        }
    }
}