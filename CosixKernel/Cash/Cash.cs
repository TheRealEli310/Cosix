using System;
using System.Collections.Generic;
using System.Text;
using LibDotNetParser.DotNet;

namespace CosixKernel.Cash
{
    public class Cash
    {
        public static void Shell()
        {
            Console.Write("root@cosix:/#");
            Call(Console.ReadLine());
        }
        public static void Call(string cmdfull)
        {
            string[] cmdsplit = cmdfull.Split(" ");
            string cmd = cmdsplit[0];
            switch (cmd)
            {
                case "help":
                    Commands.Help.Main();
                    break;
                case "ver":
                    Commands.Ver.Main();
                    break;
                default:
                    Console.WriteLine("cash: Command not found");
                    break;
            }
        }
        public static void RunFile(string path)
        {
            throw new NotImplementedException();
        }
    }
}
