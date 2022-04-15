using System;
using System.Reflection;
using Harmony12;
using InteractionFix.ModSettings;
using static UnityModManagerNet.UnityModManager;

namespace InteractionFix
{
    public class Main
    {
        private static ModEntry mod;
        public static Settings Settings { get; private set; }
        public static bool Enabled { get; private set; }

        public static bool Load(ModEntry modEntry)
        {
            mod = modEntry;
            Settings = Settings.Load(modEntry);

            Patch(mod);

            mod.OnToggle = OnToggle;
            mod.OnGUI = OnGUI;
            mod.OnSaveGUI = OnSaveGUI;
            return true;
        }

        internal static void Log(string what)
        {
            mod.Logger.Log("InteractionFix: " + what);
        }

        internal static void LogException(Exception e)
        {
            mod.Logger.LogException(e);
        }

        private static bool OnToggle(ModEntry mod, bool value)
        {
            Enabled = value;
            return true;
        }

        private static void OnGUI(ModEntry modEntry)
        {
            SettingsUI.OnGUI(modEntry, Settings);
        }

        private static void OnSaveGUI(ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }

        private static void Patch(ModEntry mod)
        {
            try
            {
                var harmonyInstance = HarmonyInstance.Create(mod.Info.Id);
                harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                mod.Logger.Error(e.ToString());
            }
        }

    }
}