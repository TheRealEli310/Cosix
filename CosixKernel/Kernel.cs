using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace CosixKernel
{
    public class Kernel : Sys.Kernel
    {
        protected override void BeforeRun()
        {
            Console.WriteLine("[OK] Cosmos loaded. Booting the kernel!");
        }

        protected override void Run()
        {
            Cash.Cash.Shell();
        }
    }
}
