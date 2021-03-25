using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using System.IO;
using Cosmos.HAL.Network;
using Sys = Cosmos.System;

namespace CosixKernel
{
    public class Kernel : Sys.Kernel
    {
        private static uint pid = 0;
        public static uint Controlling { get { return pid; } }
        public static string file;
        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos loaded. Booting the kernel!");
            Console.WriteLine("Initializing components.");
            Console.WriteLine("FS Driver");
            var fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.WriteLine("VGA Driver");
            VGADriverII.Initialize(VGAMode.Text90x60);
            Terminal.Clear();

        }
        protected override void Run()
        {
            try
            {
                if (Modules.CGM.VStateGet() == 0)
                {
                    RunInit();
                    Cash.Cash.Shell();
                    ProgramStop();
                }
                else
                {
                    Modules.CGM.Run();
                }
            }
            catch (Exception e)
            {
                Crash(e);
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
            else
            {
                VGADriverII.Clear(229);
                VGAGraphics.DrawString(0, 0, "A fatal exception occured!\n" + e.ToString() + "\nPlease report this to the Cosix devs.", VGAColor.White, VGAFont.Font8x8);
                if (Modules.CGM.VStateGet() == 2)
                {
                    VGADriverII.Display();
                }
                while (true)
                {
                    
                }
            }
        }
        public static void RunInit()
        {
            pid++;
        }
        public static void ProgramStop()
        {
            while (Drivers.VirtualRam.UnAlloc())
            {

            }
            pid--;
        }
    }
}
