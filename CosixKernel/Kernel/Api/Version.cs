using System;
using System.Collections.Generic;
using System.Text;

namespace CosixKernel.Api
{
    class KVersion
    {
        private static int BuildNum = 210324;
        public static int getBuild()
        {
            return BuildNum;
        }
    }
}
