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
            if (!Main.Enabled || !Main.Settings.HotkeyInteractivity)
            {
                return;
            }

            var ui = UIManager.Get();
            var isInventoryActive = ui.IsWindowActive(UIWindows.Inventory);
            var isWarehourseActive = ui.IsWindowActive(UIWindows.Warehouse);
            var isShopActive = ui.IsWindowActive(UIWindows.Shop);

            if (Input.GetKeyDown(KeyCode.Q) && isInventoryActive)
            {
                ui.ShowSellPerConditionWindow();
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
                if (isShopActive && !isKeyboardUsed) ActivateShopSearch(ui);
                if (ui.IsWindowActive(UIWindows.CarInfo))
                {
                    Main.Logger.Log("F");
                    var window = ui.transform.Find("CarInfo").GetComponent<CarInfoWindow>();
                    if (window.CurrentWindowType == WindowType.CarInfo)
                    {
                        window.transform.Find("SellCarRow/SellButton").GetComponent<ButtonAction>().SimulateClick();
                    }
                    else if (window.CurrentWindowType == WindowType.CarBuy)
                    {
                        window.transform.Find("BuyCarRow/BuyButton").GetComponent<ButtonAction>().SimulateClick();
                    }
                    else if (window.CurrentWindowType == WindowType.CarParking)
                    {
                        window.transform.Find("MoveCarRow/MoveToGarageButton").GetComponent<ButtonAction>().SimulateClick();
                    }
                    else if (window.CurrentWindowType == WindowType.CheckOrder)
                    {
                        window.transform.Find("FinishOrderRow/FinishOrderButton").GetComponent<ButtonAction>().SimulateClick();
                    }                    
                }
            }
        }

        private static void ActivateShopSearch(UIManager ui)
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