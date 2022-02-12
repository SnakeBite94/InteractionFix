using Harmony12;
using UnityEngine;

namespace InteractionFix
{
    [HarmonyPatch(typeof(Cursor3D))]
    [HarmonyPatch("NormalCursorLock")]
    [HarmonyPatch(new[] { typeof(bool) })]
    internal class MouseLock
    {
        public static bool Prefix(bool isLocked, ref bool ___cursorIsEnable)
        {
            if (!Main.Enabled) // this Patch has to run regardless of its setting... It fixes mouse behavior in game.
            {
                return true;
            }

            // if UnityModManager window, we do not change the cursor or the capture, to enable users to unteract with it!
            if (UnityModManagerNet.UnityModManager.UI.Instance.Opened)
            {
                return true;
            }

            Cursor.visible = false;
            if (___cursorIsEnable)
            {
                Cursor.lockState = Main.Settings.UnlockMouse ? CursorLockMode.None : CursorLockMode.Confined;
            } else
            {                
                Cursor.lockState = CursorLockMode.Locked;
            }
            return false; // we completely change NormalCursorLock
        }
    }
}