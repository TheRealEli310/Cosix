using System;
using System.Collections.Generic;
using System.Text;

namespace CosixKernel.Windmill.Lib
{
    static class Memory
    {
        public static void FindFunction(Windmill super)
        {
            super.index++;
            switch (super.program[super.index-1] % 16)
            {
                case 0x00:
                    AddByte(super);
                    break;
                case 0x01:
                    AddByteArray(super);
                    break;
                case 0x02:
                    AddString(super);
                    break;
            }
        }

        static int GetRamLoc(Windmill super)
        {
            byte[] rawloc = new byte[4];
            rawloc[0] = super.program[super.index];
            super.index++;
            rawloc[1] = super.program[super.index];
            super.index++;
            rawloc[2] = super.program[super.index];
            super.index++;
            rawloc[3] = super.program[super.index];

            return (rawloc[0] << 24) + (rawloc[1] << 16) + (rawloc[2] << 8) + rawloc[3];
        }

        static void AddByte(Windmill super)
        {
            int loc = GetRamLoc(super);
            super.index++;
            byte val = super.program[super.index];

            super.ram[loc] = val; 
            Console.WriteLine(loc);
        }

        static void AddByteArray(Windmill super)
        {
            int loc = GetRamLoc(super);
            super.index++;

            byte len = super.program[super.index];
            Console.WriteLine(len);
            for (int i = 0; i < len; i++)
            {
                super.index++;
                super.ram[loc+i] = super.program[super.index];
                Console.WriteLine(super.ram[loc+i]);
            }
        }

        static void AddString(Windmill super)
        {
            int loc = GetRamLoc(super);

            for (int i = 0; super.program[super.index] != 0; i++)
            {
                super.index++;
                super.ram[loc + i] = super.program[super.index];
                Console.WriteLine((char)super.ram[loc + i]);
            }   
        }
    }
}
