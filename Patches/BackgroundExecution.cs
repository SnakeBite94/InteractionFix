using Harmony12;
using UnityEngine;

namespace InteractionFix
{
    [HarmonyPatch(typeof(Cursor3D))]
    [HarmonyPatch("Awake")]
    internal class BackgroundExecution
    {
        private static void Prefix()
        {
            Application.runInBackground = true;
            if (Application.runInBackground)
            {
                Main.Logger.Log("Application is now running in background");
            } else
            {
                Main.Logger.Error("Could not make application to run in background!");
            }
        }        
    }
}