using UnityEngine;
using static UnityModManagerNet.UnityModManager;

namespace InteractionFix
{
    internal class BackgroundExecution
    {
        internal static void Update(ModEntry entry)
        {
            var shouldRunInBackground = Main.Enabled && Main.Settings.RunInBackground;
            if (Application.runInBackground != shouldRunInBackground)
            {
                Application.runInBackground = shouldRunInBackground;
            }
        }
    }
}