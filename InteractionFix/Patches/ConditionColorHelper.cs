using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony12;
using UnityEngine;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(Helper), nameof(Helper.ConditionToColor))]
    public class ConditionColorHelper
    {
        [HarmonyPrefix]
        public static bool ConditionToColor(float condition, ref Color __result)
        {
            if (!Main.Enabled || !Main.Settings.MakeUnrepairablePurple)
            {
                return true;
            }

            if ((double)condition >= 0 && (double)condition < 0.15f)
            {
                __result = new Color(0.5f, 0f, 0.5f, 1f);
                return false;
            }

            return true;
        }
    }
}
