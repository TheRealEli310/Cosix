using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.System.Graphics;

namespace CosixKernel.Cash
{
    public class Cash
    {
        public static void Shell()
        {
            Terminal.Write("root@cosix:/#");
            Call(Terminal.ReadLine());
        }
        public static void Call(string cmdfull)
        {
            string[] cmdsplit = cmdfull.Split(" ");
            string cmd = cmdsplit[0];
            Kernel.RunInit();
            switch (cmd)
            {
                case "help":
                    Commands.Help.Main();
                    break;
                case "ver":
                    Commands.Ver.Main();
                    break;
                case "crash":
                    Kernel.Crash(new Exception("Manual crash!"));
                    break;
                case "mode":
                    try
                    {
                        switch (cmdsplit[1])
                        {
                            case "0":
                                VGADriverII.SetMode(VGAMode.Text80x25);
                                Terminal.Clear();
                                break;
                            case "1":
                                VGADriverII.SetMode(VGAMode.Text80x50);
                                Terminal.Clear();
                                break;
                            case "2":
                                VGADriverII.SetMode(VGAMode.Text90x60);
                                Terminal.Clear();
                                break;
                            default:
                                break;
                        }
                    }
                    catch { }
                    break;
                case "vmode":
                    Modules.CGM.Init(true);
                    VGADriverII.Clear(247);
                    break;
                case "mill":
                    Windmill.Windmill runner = new Windmill.Windmill(4096);
                    for (; !runner.program[runner.index].Equals(0);)
                        runner.RunNext();
                    break;
                default:
                    Terminal.WriteLine("cash: Command not found");
                    break;
                
            }
            Kernel.ProgramStop();
        }
        public static void RunFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
