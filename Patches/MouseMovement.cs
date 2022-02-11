using Harmony12;
using UnityEngine;
using UnityModManagerNet;

namespace InteractionFix
{
    [HarmonyPatch(typeof(Cursor3D))]
    [HarmonyPatch("MouseMove")]
    internal class MouseMovement
    {
        private static bool Prefix(ref Vector2 ___screenPos, ref RectTransform ___Cursor, ref bool ___cursorIsEnable)
        {
            if (___cursorIsEnable)
            {
                ___screenPos.x = Input.mousePosition.x;
                ___screenPos.y = Input.mousePosition.y;
                ___Cursor.transform.position = ___screenPos;
                return false; // skip original
            } else
            {
                return true; // FPS mouse look
            }
        }
    }
}