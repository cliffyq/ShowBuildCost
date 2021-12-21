using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ShowBuildCost
{
    //[HarmonyPatch(typeof(MainTabWindow_Research), "DrawUnlockableHyperlinks")]
    //class Patch1
    //{
    //    static void Postfix(Rect rect, ResearchProjectDef project)
    //    {
    //        LogHelper.Message($"PostFix MainTabWindow_Research.DrawUnlockableHyperlinks. rect = {rect}, project = {project}");
    //    }
    //}

    //[HarmonyPatch(typeof(Widgets), "HyperlinkWithIcon")]
    //class Patch2
    //{
    //    static void Postfix(Rect rect, Dialog_InfoCard.Hyperlink hyperlink)
    //    {
    //        LogHelper.Message($"PostFix Widgets.HyperlinkWithIcon. rect = {rect}, hyperlinkDef = {hyperlink.def}");
    //    }
    //}

    //[HarmonyPatch(typeof(Dialog_InfoCard.Hyperlink), "ActivateHyperlink")]
    //class Patch3
    //{
    //    static void Postfix(Dialog_InfoCard.Hyperlink __instance)
    //    {
    //        LogHelper.Message($"PostFix Dialog_InfoCard.ActivateHyperlink. def = {__instance.def}, thing = {__instance.thing}");
    //    }
    //}

    //[HarmonyPatch(typeof(StatsReportUtility), nameof(StatsReportUtility.DrawStatsReport), new[] { typeof(Rect), typeof(Def), typeof(ThingDef) })]
    //class Patch4
    //{
    //    static void Postfix(List<StatDrawEntry> ___cachedDrawEntries)
    //    {
    //        LogHelper.Message($"DrawStatsReport.cachedDrawEntries:{System.String.Join(",", ___cachedDrawEntries)}");
    //    }
    //}

    //[HarmonyPatch(typeof(Dialog_InfoCard), nameof(Dialog_InfoCard.DoWindowContents))]
    //class Patch5
    //{
    //    static void Postfix()
    //    {
    //        LogHelper.Message("DoWindowContents...");
    //    }
    //}

    //[HarmonyPatch(typeof(Dialog_InfoCard), "FillCard")]
    //class Patch6
    //{
    //    static void Postfix(Thing ___thing, Def ___def, ThingDef ___stuff)
    //    {
    //        LogHelper.Message($"FillCard. thing={___thing}, def={___def}, stuff={___stuff}");
    //    }
    //}

    [HarmonyPatch(typeof(BuildableDef), nameof(BuildableDef.SpecialDisplayStats))]
    class Patch7
    {
        static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> originalReturnValues, StatRequest req, BuildableDef __instance)
        {
            foreach (StatDrawEntry entry in originalReturnValues)
            {
                yield return entry;
            }
            List<ThingDefCountClass> list = __instance.CostListAdjusted(req.StuffDef, errorOnNullStuff: false);
            if (list.Count > 0)
            {
                // TODO: detailed descriptions(base, difficulty etc.), meal recipe, cache?
                var costString = String.Join(", ", list.Select(l => l.Summary));
                yield return new StatDrawEntry(StatCategoryDefOf.Basics, "Cost", costString, "Cost of this item.", 1100);
            }
        }
    }
}