using System;
using System.Reflection;
using Harmony12;
using static UnityModManagerNet.UnityModManager;

namespace InteractionFix
{    
    public class Main
    {
        private static HarmonyInstance harmonyInstance;
        private static bool patched;
        private static ModEntry mod;
        public static ModEntry.ModLogger Logger => mod.Logger;

        public static bool Load(ModEntry mod)
        {
            Main.mod = mod;
            mod.OnToggle = OnToggle;            
            return true;
        }

        private static bool OnToggle(ModEntry mod, bool enable)
        {
            if (enable)
            {
                Patch(mod);
            }
            else
            {
                Unpatch();
            }
            return true;
        }

        private static void Patch(ModEntry mod)
        {
            Logger.Log("Starting InteractionFix...");
            if (patched)
                return;

            harmonyInstance = HarmonyInstance.Create(mod.Info.Id);
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            patched = true;
            Logger.Log("Patching done!");
        }

        private static void Unpatch()
        {
            if (!patched)
                return;

            harmonyInstance.UnpatchAll();
            patched = false;
        }

        // Config removed...
    }
}