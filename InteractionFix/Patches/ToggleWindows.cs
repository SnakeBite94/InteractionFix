using System.Collections.Generic;
using System.Linq;
using Harmony12;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(GameScript), "Update")]
    internal class ToggleWindows
    {
        private static HashSet<UIWindows> currentlyToggledWindows = new HashSet<UIWindows>();

        [HarmonyPostfix]
        internal static void Update()
        {
            var im = Singleton<InputManager>.Instance;
            var settings = Main.Settings;

            ProcessToggleOffOnButtonDown(settings.TogglePauseQuitMenu, im.GameplayMechanicExitButtonDown(), UIWindows.PauseQuitMenu);
            var inventoryButtonDown = im.GameplayInventoryButton() || im.GameplayMechanicInventoryButton() || im.UIInventoryButton();
            ProcessToggleOffOnButtonDown(settings.ToggleInventory, inventoryButtonDown, UIWindows.Inventory);
        }

        private static void ProcessToggleOffOnButtonDown(bool isEnabled, bool isButtonDown, UIWindows windowToToggle)
        {
            if (!isEnabled || !Main.Enabled)
            {
                return;
            }

            var uiManager = UIManager.Get();
            
            var isWindowActive = uiManager.IsWindowActive(windowToToggle);
            if (!isWindowActive)
            {
                if (!isButtonDown)
                {
                    currentlyToggledWindows.Remove(windowToToggle);
                }
                return;
            }

            if (!isButtonDown)
            {
                return;
            }

            Main.Log("CurrentlyToggled: " + string.Join(", ", currentlyToggledWindows.Select(x => x.ToString()).ToArray()));

            if (!currentlyToggledWindows.Contains(windowToToggle))
            {
                Main.Log(windowToToggle.ToString() + " opened.");
                currentlyToggledWindows.Add(windowToToggle);
            }
            else
            {
                Main.Log("Hiding " + windowToToggle.ToString());
                currentlyToggledWindows.Remove(windowToToggle);
                uiManager.Hide(windowToToggle.ToString());
            }
        }
    }
}