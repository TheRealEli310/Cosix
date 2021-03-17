using System;
using System.Collections.Generic;
using System.Text;
using CosixKernel.Api;

namespace CosixKernel.Cash.Commands
{
    class Ver
    {
        public static void Main()
        {
            Console.WriteLine("Cosix (" + KVersion.getBuild().ToString() + ")");
        }
    }
}
