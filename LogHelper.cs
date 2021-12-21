using System.Collections.Generic;
using HarmonyLib;
using Verse;

namespace ShowBuildCost
{
    [HarmonyPatch(typeof(Log), nameof(Log.Clear))]
    public static class LogHelper
    {
        static readonly HashSet<string> loggedText = new HashSet<string>();
        public static void Message(string text)
        {
            if (loggedText.Add(text))
            {
                Log.Message(text);
            }
        }

        static void Postfix()
        {
            loggedText.Clear();
        }
    }
}
