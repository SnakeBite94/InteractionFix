using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony12;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(ScreenFader), "FadeTo", new[] { typeof(float), typeof(float), typeof(float), typeof(bool), typeof(bool) })]
    internal class FastScreenFader
    {
        [HarmonyPrefix]
        public static bool FadeTo(ref float time, float alphaFrom, float alphaTo, bool useBlur, bool disableOnComplete)
        {
            if (!Main.Enabled && !Main.Settings.FasterFades)
            {
                return true;
            }

            time = time / 8;

            return true;
        }
    }
}
