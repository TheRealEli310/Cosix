using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CosixKernel.Drivers
{
    class VirtualRam
    {
        private static byte[] ram = new byte[0];
        private static uint[] allocpid = new uint[128];
        private static uint[] alloc = new uint[256];
        public static void InitRam(uint size)
        {
            ram = new byte[size];
        }
        public static void Write(uint addr, byte value)
        {
            if (IsOwned(Kernel.Controlling,addr))
            {
                ram[addr] = value;
            }
        }
        public static bool UnAlloc()
        {
            return false;
            /*int pi = 0;
            foreach (var pidc in allocpid)
            {
                if (pidc == Kernel.Controlling)
                {
                    break;
                }
                pi++;
            }
            if (pi == 127)
            {
                return false;
            }
            uint[] region = new uint[2];
            region[0] = alloc[pi * 2];
            region[1] = alloc[(pi * 2) + 1];
            for (uint i = region[0]; i <= region[1]; i++)
            {
                ram[i] = 0;
            }
            var allocl = alloc.ToList();
            var allocpidl = allocpid.ToList();
            allocpidl.RemoveAt(pi);
            allocl.RemoveAt(pi);
            allocl.RemoveAt(pi+1);
            alloc = allocl.ToArray();
            allocpid = allocpidl.ToArray();
            return true;
            */
        }
        public static void Alloc(uint start, uint end)
        {
            /*uint[] region = new uint[2];
            region[0] = start;
            region[1] = end;
            for (uint i = region[0]; i <= region[1]; i++)
            {
                ram[i] = 0;
            }
            var allocl = alloc.ToList();
            var allocpidl = allocpid.ToList();
            allocpidl.Add(Kernel.Controlling);
            allocl.Add(region[0]);
            allocl.Add(region[1]);
            alloc = allocl.ToArray();
            allocpid = allocpidl.ToArray();
            */
        }
        public static bool IsOwned(uint pid,uint addr)
        {
            return true;
            /*int pi = 0;
            foreach (var pida in allocpid)
            {
                if (pida == pid)
                {
                    break;
                }
                pi++;
            }
            if (pi == 127)
            {
                return false;
            }
            uint[] region = new uint[2];
            region[0] = alloc[pi * 2];
            region[1] = alloc[(pi * 2) + 1];
            for (uint i = region[0]; i <= region[1]; i++)
            {
                if (i == addr)
                {
                    return true;
                }
            }
            return false;*/
        }
    }
}
