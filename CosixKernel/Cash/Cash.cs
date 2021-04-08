using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using CosixKernel.Apps.MIV;
using System.IO;

namespace CosixKernel.Cash
{
    public class Cash
    {
        public static void Shell()
        {
            Terminal.Write(Modules.UC.username + "@cosix:/#");
            Call(Terminal.ReadLine());
        }
        public static void Call(string cmdfull)
        {
            string[] cmdsplit = cmdfull.Split(" ");
            string cmd = cmdsplit[0];
            Kernel.RunInit();
            switch (cmd)
            {
                case "":
                    break;
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
                            case "3":
                                Modules.CGM.Init(true);
                                VGADriverII.Clear(247);
                                break;
                            default:
                                break;
                        }
                    }
                    catch { }
                    break;
                case "mill":
                    Windmill.Windmill runner = new Windmill.Windmill(4096);
                    for (; !runner.program[runner.index].Equals(0);)
                        runner.RunNext();
                    break;
                case "miv":
                    if (1 < cmdsplit.Length)
                    {
                        Kernel.file = cmdsplit[1];
                        if (File.Exists(cmdsplit[1]))
                        {
                            File.WriteAllText(@"0:\" + Kernel.file, MIV.miv(File.ReadAllText(cmdsplit[1])));
                        }
                        else
                        {
                            File.WriteAllText(@"0:\" + Kernel.file, MIV.miv(null));
                        }
                        
                    }
                    else
                    {
                        MIV.StartMIV();
                    }
                    break;
                case "dir":
                    string[] filePaths = Directory.GetFiles(@"0:\");
                    var drive = new DriveInfo("0");
                    Terminal.WriteLine("Volume in drive 0 is " + $"{drive.VolumeLabel}");
                    Terminal.WriteLine("Directory of " + @"0:\");
                    Terminal.WriteLine("\n");
                    for (int i = 0; i < filePaths.Length; ++i)
                    {
                        string path = filePaths[i];
                        Terminal.WriteLine(System.IO.Path.GetFileName(path));
                    }
                    foreach (var d in System.IO.Directory.GetDirectories(@"0:\"))
                    {
                        var dir = new DirectoryInfo(d);
                        var dirName = dir.Name;

                        Terminal.WriteLine(dirName + " <DIR>");
                    }
                    Terminal.WriteLine("\n");
                    Terminal.WriteLine("        " + $"{drive.TotalSize}" + " bytes");
                    Terminal.WriteLine("        " + $"{drive.AvailableFreeSpace}" + " bytes free");
                    break;
                case "power":
                    Commands.Power.Main(cmdsplit);
                    break;
                case "elev":
                    Modules.UC.Elevate();
                    break;
                case "login":
                    Modules.UC.username = cmdsplit[1];
                    break;
                case "clear":
                    Terminal.Clear();
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
    namespace Commands
    {
        class Power
        { 
            public static void Main(string[] args)
            {
                if (1 < args.Length)
                {
                    if (args[1] == "s")
                    {
                        Kernel.Shutdown();
                    }
                    if (args[1] == "r")
                    {
                        Kernel.Restart();
                    }
                }
                else
                {
                    
                }
            }
        }
    }
}
