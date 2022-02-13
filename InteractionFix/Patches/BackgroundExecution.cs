using Harmony12;
using UnityEngine;
using static UnityModManagerNet.UnityModManager;

namespace InteractionFix
{
    [HarmonyPatch(typeof(Cursor3D), "Update")]
    internal class BackgroundExecution
    {
        [HarmonyPrefix]
        internal static void Update()
        {
            var shouldRunInBackground = Main.Enabled && Main.Settings.RunInBackground;
            if (Application.runInBackground != shouldRunInBackground)
            {
                Application.runInBackground = shouldRunInBackground;
            }
        }
    }
}