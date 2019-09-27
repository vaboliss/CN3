using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerNetworks
{
    public class Comercial : Packet
    {
        public string information;

        public Comercial(string info, int source, int destination)
        {
            information = info;
            Source = source;
            Destination = destination;
        }
        public override string ToString()
        {
            return "Comercial Packet\n Contains: " + information + " Source: " + Source + "  Destination: " + Destination;
        }
    }
}
