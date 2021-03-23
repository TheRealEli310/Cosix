using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos.HAL.Network
{
    class KNetworking
    {
        public static Cosmos.System.Network.UdpClient udpClient = new Cosmos.System.Network.UdpClient();
        public static void Connect(Cosmos.System.Network.IPv4.Address dest,int destPort)
        {
            udpClient.Connect(dest, destPort);
            udpClient.Send(new byte[] {67,1});
        }
    }
}
