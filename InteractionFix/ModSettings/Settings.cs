using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

namespace InteractionFix.ModSettings
{
    public class Settings : UnityModManager.ModSettings
    {
        public bool RunInBackground { get; set; }
        public bool UnlockMouse { get; set; }
        public bool DisableMouseSmoothing { get; set; }

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