using HarmonyLib;
using Verse;

namespace ShowBuildCost
{
    [StaticConstructorOnStartup]
    public static class ShowBuildCost
    {
        static ShowBuildCost()
        {
            Harmony.DEBUG = true;
            Harmony harmony = new Harmony("ShowBuildCost");
            harmony.PatchAll();
        }
    }
}
