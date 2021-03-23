using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosixKernel.Cash.Commands
{
    class Help
    {
        public static void Main()
        {
            Terminal.WriteLine("=== Help ===");
            Terminal.WriteLine("help - Display this!");
            Terminal.WriteLine("ver - Get the version number");
            Terminal.WriteLine("mode (0-2) - Set text resolution");
            Terminal.WriteLine("crash - Crashes the OS");
        }
    }
}
