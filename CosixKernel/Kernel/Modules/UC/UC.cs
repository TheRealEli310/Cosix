using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace CosixKernel.Modules
{
    class UC
    {
        private static int perml = 0;
        public static int permlevel
        {
            get { return perml; }
            set { Elevate(); }
        }
        public static void UserShell()
        {
            try
            {
                Cash.Cash.Shell();
            }
            catch (Exception e)
            {
                UserExceptionHandler(e);
            }
        }
        public static void UserExceptionHandler(Exception e)
        {
            Terminal.WriteLine("User Mode " + e.ToString());
        }
        private static void Elevate()
        {
            Terminal.WriteLine("A program in user mode is asking for elevation. Elevate to perm level " + (perml + 1).ToString() + "? (Y/N) ");
            var yn = Console.ReadKey().KeyChar.ToString();
            if (yn == "y")
            {
                perml++;
            }
        }
    }
}
