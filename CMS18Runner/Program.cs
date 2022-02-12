using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CMS18Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Starting CMS18");
            var p = Process.Start(@"steam://rungameid/645630");
            p.WaitForInputIdle();
            var cms = default(Process);
            for (int retry = 0; retry < 10 && cms == null; retry++)
            {
                Thread.Sleep(500);
                Console.Write(".");
                cms = Process.GetProcessesByName("cms2018.exe").FirstOrDefault();
            }

        }
    }
}
