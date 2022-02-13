using System;
using Harmony12;
using UnityEngine;
using UnityEngine.UI;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(GameScript), "Update")]
    internal class InventoryInteractivity
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

            var isInventoryActive = ui.IsActive(UIWindows.Inventory.ToString());
            var isWarehourseActive = ui.IsActive(UIWindows.Warehouse.ToString());
            var isShopActive = ui.IsActive(UIWindows.Shop.ToString());

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

            var isKeyboardUsed = ui.IsActive("Ask") || ui.IsActive("SortingMenu") ||
                ui.IsShopSearchFieldFocused() || ui.IsInventorySearchFieldFocused() || ui.IsWarehouseSearchFieldFocused();

            if (!isKeyboardUsed)
            {
                if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Z))
                {
                    if (isInventoryActive) ui.PrevCategoryInInventory();
                    if (isWarehourseActive) ui.PrevCategoryInWarehouse();
                    if (isShopActive) ui.PrevCategoryInShop();
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (isInventoryActive) ui.NextCategoryInInventory();
                    if (isWarehourseActive) ui.NextCategoryInWarehouse();
                    if (isShopActive) ui.NextCategoryInShop();
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Main.Logger.Log("F pressed");
                    if (isInventoryActive) ActivateSearch("Inventory");
                    if (isWarehourseActive) ActivateSearch("Warehouse");
                    if (isShopActive)
                    {
                        // TODO: Does not work :(
                        foreach (var shopID in Enum.GetNames(typeof(ShopID)))
                        {
                            ActivateSearch("Shop", shopID);                            
                        }
                    }
                    
                }
            }
        }

        private static void ActivateSearch(string canvasName, string name = null)
        {
            var component = UIManager.Get().transform.parent.Find($"Canvas{canvasName}/{name ?? canvasName}/InputField").GetComponent<InputField>();
            component.text = string.Empty;
            component.ActivateInputField();
        }
    }
}