using System;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

namespace InteractionFix.ModSettings
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool RunInBackground { get; set; } = true;
        public bool UnlockMouse { get; set; } = true;
        public bool DisableMouseSmoothing { get; set; } = true;
        public bool SkipIntroBySpace { get; set; } = true;
        public bool TogglePauseQuitMenu { get; set; } = true;
        public bool ToggleInventory { get; set; } = true;                
        public bool HotkeyInteractivity { get; set; } = true;
        public bool FasterFades { get; set; } = true;
        public bool SkipCaseOpening { get; set; } = true;

        public override void Save(ModEntry modEntry)
        {
            Save(this, modEntry);
        }

        public static Settings Load(ModEntry modEntry)
        {
            return Load<Settings>(modEntry);
        }
    }
}