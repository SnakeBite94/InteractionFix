using Harmony12;
using UnityEngine;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(CaseOpening), "Update")]
    internal class CaseOpeningInteractivity
    {
        [HarmonyPostfix]
        internal static void Update(ref CaseOpening __instance, ref bool ___canAddItems)
        {
            if (!Main.Enabled || !Main.Settings.HotkeyInteractivity)
            {
                return;
            }

            if (AnimatorIsPlaying(__instance) && !Main.Settings.SkipCaseOpening)
            {
                return;
            }

            if (___canAddItems && Singleton<InputManager>.Instance.UIEnterButtonDown())
            {
                __instance.Close();
            }
        }

        private static bool AnimatorIsPlaying(CaseOpening __instance)
        {
            return __instance.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1f;
        }
    }
}