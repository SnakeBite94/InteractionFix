using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityModManagerNet;
using static UnityEngine.GUILayout;

namespace InteractionFix.ModSettings
{
    public class SettingsUI
    {
        internal static void OnGUI(UnityModManager.ModEntry modEntry, Settings settings)
        {
            BeginVertical();
            settings.RunInBackground = Toggle(settings.RunInBackground, " Run in background");
            settings.UnlockMouse = Toggle(settings.UnlockMouse, " Unlock mouse cursor if visible (and with UMM closed)");
            settings.DisableMouseSmoothing = Toggle(settings.DisableMouseSmoothing, " Disable mouse smoothing");           
            EndVertical();
        }
    }
}
