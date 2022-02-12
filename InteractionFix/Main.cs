using System;
using System.Reflection;
using Harmony12;
using InteractionFix.ModSettings;
using UnityModManagerNet;
using static UnityModManagerNet.UnityModManager;

namespace InteractionFix
{
    public class Main
    {
        private static ModEntry mod;
        public static Settings Settings { get; private set; }
        public static bool Enabled { get; private set; }

        public static ModEntry.ModLogger Logger => mod.Logger;

        public static bool Load(ModEntry modEntry)
        {
            mod = modEntry;
            Settings = Settings.Load(modEntry);

            Patch(mod);

            mod.OnToggle = OnToggle;
            mod.OnGUI = OnGUI;
            mod.OnSaveGUI = OnSaveGUI;
            mod.OnUpdate = OnUpdate;
            return true;
        }

        private static void OnUpdate(ModEntry modEntry, float arg2)
        {
            BackgroundExecution.Update(modEntry);
        }

        private static void OnSaveGUI(ModEntry modEntry)
        {
            Settings.Save(modEntry);
        }

        private static void OnGUI(ModEntry modEntry)
        {
            SettingsUI.OnGUI(modEntry, Settings);
        }

        private static bool OnToggle(ModEntry mod, bool value)
        {
            Enabled = value;
            return true;
        }

        private static void Patch(ModEntry mod)
        {
            Logger.Log("Starting InteractionFix...");

            try
            {
                var harmonyInstance = HarmonyInstance.Create(mod.Info.Id);
                harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception e)
            {
                mod.Logger.Error(e.ToString());
            }

            Logger.Log("Patching done!");
        }
    }
}