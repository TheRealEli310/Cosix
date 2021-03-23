﻿using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.HAL.Network;
using Sys = Cosmos.System;

namespace CosixKernel
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            VGADriverII.Initialize(VGAMode.Text90x60);
            Terminal.WriteLine("Cosmos loaded. Booting the kernel!");
            Terminal.WriteLine("Initializing components.");
        }

        protected override void Run()
        {
            if (Modules.CGM.VStateGet() == 0)
            {
                Cash.Cash.Shell();
            }
            else
            {
                Modules.CGM.Run();
            }
            
        }
        public static void Crash(Exception e)
        {
            if (Modules.CGM.VStateGet() == 0)
            {
                Terminal.Clear(ConsoleColor.DarkRed);
                Terminal.WriteLine("A fatal exception occured!");
                Terminal.WriteLine(e.ToString());
                Terminal.WriteLine("Please report this to the Cosix devs.");
                while (true)
                {
                    Terminal.DisableCursor();
                }
            }
        }
    }
}