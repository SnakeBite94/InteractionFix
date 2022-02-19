using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony12;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(Cursor3D), "Update")]
    class XAsRightClickInInventory
    {
        [HarmonyPostfix]
        public static void Update(ref SimulateInputModule ___simulateInputModule)
        {
            var ui = UIManager.Get();
            var isInventoryActive = ui.IsWindowActive(UIWindows.Inventory);
            if (Input.GetKeyDown(KeyCode.X) && isInventoryActive)
            {
                Main.Logger.Log("Right click");
                //GetLastPointerEventData(-1)
                //ui.RightClickInInventory();
                var eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;
                var results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                var qq = results.Where(r => r.gameObject.layer == 5).ToArray();
                foreach (var  q in qq)
                {
                    q.gameObject.GetComponent<ButtonAction>().SimulateRightClick();
                }
            }
        }
    }
}
