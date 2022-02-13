using System.Reflection;
using Harmony12;
using UnityEngine;
using UnityEngine.UI;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(GameScript), "Update")]
    internal class InventoryInteractivity
    {
        private static string lastShop;

        [HarmonyPrefix]
        internal static void Update()
        {
            if (!Main.Enabled || !Main.Settings.InventoryInteractivity)
            {
                return;
            }

            var im = Singleton<InputManager>.Instance;
            var ui = UIManager.Get();

            var isInventoryActive = ui.IsActive(UIWindows.Inventory.ToString());
            var isWarehourseActive = ui.IsActive(UIWindows.Warehouse.ToString());
            var isShopActive = !string.IsNullOrEmpty(ui.GetCurrentShopPage());

            if (Input.GetKeyDown(KeyCode.X) && isInventoryActive)
            {
                ui.ShowSellPerConditionWindow();
            }

            // Does not work :( I have no idea how to hide Ask dialogs :\
            //if (im.GameplayMechanicExitButtonDown() && ui.IsActive("Ask") && isInventoryActive)
            //{
            //    Main.Logger.Log("Close sell per condition?");
            //    ui.transform.Find("Canvas/Ask/NewAskWindow(Clone)/SellPerCondition").gameObject.GetComponent<AskWindowBehaviour>().CloseWindow();
            //}

            if (Input.GetKeyDown(KeyCode.Mouse3) && !isShopActive)
            {
                if (isInventoryActive) ui.PrevCategoryInInventory();
                if (isWarehourseActive) ui.PrevCategoryInWarehouse();
                if (isShopActive)
                {
                    lastShop = ui.GetCurrentShopPage();
                    ui.BackToHomePage();
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse4) && !isShopActive)
            {
                if (isInventoryActive) ui.NextCategoryInInventory();
                if (isWarehourseActive) ui.NextCategoryInWarehouse();
                if (isShopActive && !string.IsNullOrEmpty(lastShop))
                {
                    ui.OpenShop(lastShop);
                }
            }

            var isKeyboardUsed = ui.IsActive("Ask") || ui.IsActive("SortingMenu") ||
                ui.IsShopSearchFieldFocused() || ui.IsInventorySearchFieldFocused() || ui.IsWarehouseSearchFieldFocused();

            if (!isKeyboardUsed)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {

                    if (isInventoryActive) ActivateSearch("Inventory");
                    if (isWarehourseActive) ActivateSearch("Warehouse");
                    if (isShopActive)
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
                        } catch (System.Exception e)
                        {
                            Main.Logger.LogException(e);
                        }
                        ActivateSearch("Shop", ui.GetCurrentShopPage());
                    }
                }
            }
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