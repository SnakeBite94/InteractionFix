using Harmony12;
using UnityEngine;

namespace InteractionFix
{    
    [HarmonyPatch(typeof(Cursor3D), "MouseMove")]
    internal class MouseMovement
    {
        [HarmonyPrefix]
        internal static bool MouseMove(ref Vector2 ___screenPos, ref RectTransform ___Cursor, ref bool ___cursorIsEnable)
        {
            if (!Main.Enabled || !Main.Settings.DisableMouseSmoothing)
            {
                return true;
            }

            if (___cursorIsEnable)
            {
                ___screenPos.x = Input.mousePosition.x;
                ___screenPos.y = Input.mousePosition.y;
                ___Cursor.transform.position = ___screenPos;
                return false; // skip original
            }
            else
            {
                return true; // FPS mouse look
            }
        }
    }
}