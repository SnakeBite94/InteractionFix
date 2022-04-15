using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace InteractionFix
{
    public static class TransformDump
    {
        public static void Dump(this Transform transform, int indent = 0)
        {
            if (indent == 0)
            {
                Main.Log("-- Transform dump start --");
            }
            Main.Log(new string(' ', indent) + transform.ToString());
            foreach (var t in transform.OfType<Transform>())
            {
                t.Dump(indent + 1);
            }
            if (indent == 0)
            {
                Main.Log("-- Transform dump end --");
            }
        }
    }
}
