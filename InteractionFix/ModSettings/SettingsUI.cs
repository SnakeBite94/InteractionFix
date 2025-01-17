using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityModManagerNet;
using static UnityEngine.GUILayout;

namespace InteractionFix.ModSettings
{
    public interface ISettingsGUIProvider
    {

    }

    public class SettingsUI
    {
        internal static void OnGUI(UnityModManager.ModEntry modEntry, Settings settings)
        {
            BeginHorizontal();

            BeginVertical(Width(250));
            settings.RunInBackground = Toggle(settings.RunInBackground, " Run in background");
            settings.UnlockMouse = Toggle(settings.UnlockMouse, " Unlock mouse cursor if visible and UMM is closed)");
            settings.DisableMouseSmoothing = Toggle(settings.DisableMouseSmoothing, " Disable mouse smoothing");            
            settings.TogglePauseQuitMenu = Toggle(settings.TogglePauseQuitMenu, " Pause menu key toggle mode");
            settings.ToggleInventory = Toggle(settings.ToggleInventory, " Inventory key toggle mode");
            settings.HotkeyInteractivity = Toggle(settings.HotkeyInteractivity, " Enable GUI hotkeys (see readme)");
            EndVertical();

            BeginVertical(Width(250));
            settings.FasterFades = Toggle(settings.FasterFades, " Faster fades to black (8x)");
            settings.SkipCaseOpening = Toggle(settings.SkipCaseOpening, " Hotkey case opening skip");
            settings.SkipIntroBySpace = Toggle(settings.SkipIntroBySpace, " Skip straight to main menu (see readme!)");
            settings.MakeUnrepairablePurple = Toggle(settings.MakeUnrepairablePurple, " Purple condition <15% (cheat?)");
            EndVertical();

            BeginVertical(Width(250));
            EndVertical();

            EndHorizontal();
        }
    }
}
