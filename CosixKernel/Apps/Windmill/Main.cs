using System;
using System.Collections.Generic;
using System.Text;

namespace CosixKernel.Windmill
{
    class Windmill
    {
        public byte[] program = new byte[]
        {
            0x11, 0x00, 0x00, 0x00, 0x00, 0x02, 0x12, 0x34,
            0x12, 0x00, 0x00, 0x00, 0x02, 0x61, 0x62, 0x63, 0x00,
            0x00,
        }
        ;

        public byte[] ram;
        public int index;

        public Windmill(uint mAlloc)
        {
            ram = new byte[mAlloc];
        }

        public void RunNext()
        {
            FindCommand();
            index++;
        }

        public void FindCommand()
        {
            switch (program[index]/16)
            {
                case 0x01:
                    Lib.Memory.FindFunction(this);
                    break;
            }
        }
    }
}
