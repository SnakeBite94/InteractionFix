using System;
using System.Reflection;
using Harmony12;
using UnityEngine;
using UnityEngine.UI;
using static UnityModManagerNet.UnityModManager;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(AskWindowBehaviour), "Update")]
    internal class AskWindowCancelByEsc
    {
        [HarmonyPostfix]
        internal static void Update(ref AskWindowBehaviour __instance, ref bool ___windowJustCreated)
        {
            if (!Main.Enabled || !Main.Settings.HotkeyInteractivity)
            {
                return;
            }

            if (___windowJustCreated)
            {
                ___windowJustCreated = false;
                return;
            }

            var im = Singleton<InputManager>.Instance;
            if (im.GameplayMechanicExitButtonDown())
            {
                Main.Log("ButtonCancel");
                __instance.ButtonCancel();
            }
        }
    }
}