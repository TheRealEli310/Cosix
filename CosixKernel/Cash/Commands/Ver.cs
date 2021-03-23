using System;
using System.Collections.Generic;
using System.Text;
using CosixKernel.Api;
using Cosmos.HAL;

namespace CosixKernel.Cash.Commands
{
    class Ver
    {
        public static void Main()
        {
            Terminal.WriteLine("Cosix (" + KVersion.getBuild().ToString() + ")");
        }
    }
}
