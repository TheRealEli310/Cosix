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
        private static bool pm = false;
        public static uint Controlling { get { return pid; } }
        public static string file;
        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos loaded. Booting the kernel!");
            Console.WriteLine("Initializing update package.");
            //update.BeforeRun();
            Console.WriteLine("Initializing components.");
            Console.WriteLine("FS Driver");
            var fs = new Sys.FileSystem.CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
            Console.WriteLine("VGA Driver");
            VGADriverII.Initialize(VGAMode.Text80x25);
            Terminal.Clear();
            Modules.UC.username = "root";
        }
        protected override void Run()
        {
            try
            {
                if (Modules.CGM.VStateGet() == 0)
                {
                    //RunInit();
                    Cash.Cash.Shell();
                    //update.Run();
                    //ProgramStop();
                }
                else
                {
                    Modules.CGM.Run();
                    //update.Run();
                }
            }
            catch (Exception e)
            {
                Crash(e);
            }
        }
        protected override void AfterRun()
        {
            Terminal.Clear();
            Terminal.WriteLine("The system is going down NOW!");
            Terminal.WriteLine("Saving user settings");
            Terminal.WriteLine("ACPI Shutdown/Reboot");
            if (!pm)
            {
                Sys.Power.Shutdown();
            }
            else
            {
                Sys.Power.Reboot();
            }
            
        }
        
        public static void Shutdown()
        {
            Terminal.BackColor = ConsoleColor.Black;
            Terminal.TextColor = ConsoleColor.White;
            Kernel kernel = new Kernel();
            pm = false;
            kernel.AfterRun();
        }
        public static new void Restart()
        {
            Kernel kernel = new Kernel();
            pm = true;
            kernel.AfterRun();
        }
        public static void Crash(Exception e)
        {
            if (Modules.CGM.VStateGet() == 0)
            {
                Terminal.Clear(ConsoleColor.DarkRed);
                Terminal.WriteLine("A fatal exception occured!");
                Terminal.WriteLine(e.ToString());
                Terminal.WriteLine("Please report this to the Cosix devs.");
                Terminal.DisableCursor();
                while (true)
                {
                    if (Sys.KeyboardManager.ControlPressed)
                    {
                        Terminal.BackColor = ConsoleColor.Black;
                        Terminal.Clear();
                        Terminal.EnableCursor();
                        break;
                    }
                    if (Sys.KeyboardManager.ShiftPressed) { Restart(); }
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
