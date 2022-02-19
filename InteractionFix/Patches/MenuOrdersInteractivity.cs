using Harmony12;
using UnityEngine;

namespace InteractionFix.Patches
{
    [HarmonyPatch(typeof(MenuOrders), "Update")]
    internal class MenuOrdersInteractivity
    {
        [HarmonyPostfix]
        internal static void Update(ref Transform ___orderList, ref int ___selectedObjectIndex)
        {
            if (!Main.Enabled || !Main.Settings.HotkeyInteractivity)
            {
                return;
            }

            if (UIManager.Get().IsWindowActive(UIWindows.Ask))
            {
                return;
            }
            if (___orderList.childCount == 0)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Transform child = ___orderList.GetChild(___selectedObjectIndex);
                OrderItem component = child.GetComponent<OrderItem>();
                if (!component || component.job.IsMission)
                {
                    return;
                }
                OrderGenerator.Get().CancelJob(component.job.id);
                AchievementSystem.Get().IncrementStat(13, 1);
                UnityEngine.Object.Destroy(child.gameObject);
            }

            if (Singleton<InputManager>.Instance.UIEnterButtonDown())
            {
                Transform selectedItem = ___orderList.GetChild(___selectedObjectIndex);
                OrderItem orderItem = selectedItem.GetComponent<OrderItem>();
                if (!orderItem)
                {
                    return;
                }
                NewHash hash = new NewHash(new object[]
                {
                    "WindowType",
                    "MenuOrders",
                    "Type",
                    "TakeJob",
                    "Job",
                    orderItem.job,
                    "Sender",
                    selectedItem.gameObject
                });
                GameManager.Get().ButtonAccept(hash);
            }
        }
    }
}