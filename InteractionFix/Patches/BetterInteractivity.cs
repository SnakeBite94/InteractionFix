using System;
using System.Reflection;
using Harmony12;
using UnityEngine;
using UnityEngine.UI;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(GameScript), "Update")]
    internal class BetterInteractivity
    {
        [HarmonyPrefix]
        internal static void Update()
        {
            if (!Main.Enabled || !Main.Settings.InventoryInteractivity)
            {
                return;
            }

            var im = Singleton<InputManager>.Instance;
            var ui = UIManager.Get();
            var isInventoryActive = ui.IsWindowActive(UIWindows.Inventory);
            var isWarehourseActive = ui.IsWindowActive(UIWindows.Warehouse);
            var isShopActive = ui.IsWindowActive(UIWindows.Shop);

            if (Input.GetKeyDown(KeyCode.X) && isInventoryActive)
            {
                ui.ShowSellPerConditionWindow();
            }

            if (im.GameplayMechanicExitButtonDown() && ui.IsActive("Ask"))
            {
                try
                {
                    // Does not work :( I have no idea why
                    ui.transform.FindDeepChild("NoButton").GetComponent<AskWindowBehaviour>().CloseWindow();
                }
                catch (Exception e)
                {
                    Main.Logger.Log(e.ToString());
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse3))
            {
                if (isInventoryActive) ui.PrevCategoryInInventory();
                if (isWarehourseActive) ui.PrevCategoryInWarehouse();
                if (isShopActive)
                {
                    ui.BackToHomePage();
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse4))
            {
                if (isInventoryActive) ui.NextCategoryInInventory();
                if (isWarehourseActive) ui.NextCategoryInWarehouse();
                if (isShopActive)
                {
                    ui.OpenPrevPage();
                }
            }

            var isKeyboardUsed = ui.IsWindowActive(UIWindows.Ask) || ui.IsWindowActive(UIWindows.SortingMenu) ||
                (ui.IsWindowActive(UIWindows.Shop) && ui.IsShopSearchFieldFocused()) ||
                (ui.IsWindowActive(UIWindows.Inventory) && ui.IsInventorySearchFieldFocused()) ||
                (ui.IsWindowActive(UIWindows.Warehouse) && ui.IsWarehouseSearchFieldFocused());

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isInventoryActive && !isKeyboardUsed) ActivateSearch("Inventory");
                if (isWarehourseActive && !isKeyboardUsed) ActivateSearch("Warehouse");
                if (isShopActive && !isKeyboardUsed) ActivateShhopSearch(ui);
                if (ui.IsWindowActive(UIWindows.CarInfo))
                {
                    try
                    {
                        Main.Logger.Log("Finish order!");
                        ui.transform.Find("CarInfo/FinishOrderRow/FinishOrderButton").GetComponent<ButtonAction>().SimulateClick();
                    }
                    catch (Exception e)
                    {
                        Main.Logger.LogException(e);
                    }
                }
            }
        }

        private static void ActivateShhopSearch(UIManager ui)
        {
            Main.Logger.Log("F pressed in shop");
            // THIS JUST DOES NOT WORK :(
            try
            {
                var gridManager = ui.GetType()
                    .GetField("currentShopPageGridManager", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(ui) as UIGridManager;
                gridManager.DeselectCurrent();
                gridManager.SetEnabled(false);
            }
            catch (System.Exception e)
            {
                Main.Logger.LogException(e);
            }
            ActivateSearch("Shop", ui.GetCurrentShopPage());
        }

        private static void ActivateSearch(string canvasName, string name = null)
        {
            var key = $"Canvas{canvasName}/{name ?? canvasName}/InputField";
            Main.Logger.Log(key);
            var component = UIManager.Get().transform.parent.Find(key).GetComponent<InputField>();
            component.text = string.Empty;
            component.ActivateInputField();
        }
    }
}