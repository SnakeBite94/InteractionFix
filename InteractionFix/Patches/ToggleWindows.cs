using System.Collections.Generic;
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
            if (!isButtonDown)
            {
                return;
            }
            var isWindowActive = uiManager.IsWindowActive(windowToToggle);
            if (!isWindowActive)
            {
                return;
            }

            if (!currentlyToggledWindows.Contains(windowToToggle))
            {
                currentlyToggledWindows.Add(windowToToggle);
            }
            else
            {
                currentlyToggledWindows.Remove(windowToToggle);
                uiManager.Hide(windowToToggle.ToString());
            }
        }
    }

}