using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony12;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(IntroPlayer), "Update")]    
    internal class SkipIntro
    {
        [HarmonyPrefix]
        internal static bool Update()
        {
            if (!Main.Enabled || !Main.Settings.SkipIntroBySpace)
            {
                return true;
            }

            SceneManager.LoadScene(4);

            return true;
        }
    }
}
