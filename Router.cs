using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace ComputerNetworks
{
    public class Router
    {
        private static int _counter = -1;
        public int Id { get; set; }

        public bool IsReceived = false;
        public List<Packet> ContainedPackets = new List<Packet>();

        public Dictionary<int, int> neighbors = new Dictionary<int,int>();
        public int[,] Vector { get; set; }
        public Router()
        {
            Id = System.Threading.Interlocked.Increment(ref _counter);
        }
        public void AddPacket(Packet pac)
        {
            ContainedPackets.Add(pac);
        }
        public void SendPacket(List<Router> routers)
        {
            if (ContainedPackets.Count != 0)
            {
                Packet packet = ContainedPackets[0];
                int Dest = packet.Destination;
                if (Dest == this.Id)
                {
                    int rm = 0;
                    Console.WriteLine("Packed reached his destiny");
                    for (int i= 0;i<ContainedPackets.Count;i++)
                    {
                        if (ContainedPackets[i].Destination == this.Id)
                        {
                            rm = i;
                        }
                    }
                    ContainedPackets.RemoveAt(index: rm);


                }
                else
                {
                    for (int i = 0; i < Vector.Length / 3; i++)
                    {
                        if (Vector[i, 0] == Dest)
                        {
                            int id = Vector[i, 2];
                            foreach (var router in routers)
                            {
                                if (router.Id == id)
                                {
                                    router.ContainedPackets.Add(packet);
                                    router.IsReceived = true;
                                    this.ContainedPackets.RemoveAt(0);
                                }
                            }
                        }
                    }

                }
            }

        }
        public void Send(List<Router> routers)
        {
            if (IsReceived == false)
            {
                SendPacket(routers);
            }
            else if (ContainedPackets.Count > 1)
            {
                SendPacket(routers);
            }
        }
        public void InitializeVector(int NumberOfNodes)
        {
            Vector = new int[NumberOfNodes,3];
            for (int i = 0; i < NumberOfNodes;i++)
            {
                Vector[i,0]=i;
                if(Vector[i,0] == this.Id)
                {
                    Vector[i, 1] = 0;
                    Vector[i, 2] = i;
                }
            }

            foreach ( var neighbor in neighbors)
            {
                for (int i = 0; i< NumberOfNodes;i++)
                {
                    if (Vector[i,0] == neighbor.Key)
                    {
                        Vector[i, 1] = neighbor.Value;
                        Vector[i, 2] = neighbor.Key;
                    }
                }
            }
            for (int i = 0; i < NumberOfNodes; i++)
            {
                if (!neighbors.ContainsKey(Vector[i, 0]))
                {
                    if (Vector[i,0] != this.Id)
                    {
                        Vector[i, 1] = -9999;
                        Vector[i, 2] = -9999;
                    }
                }
            }
        }

        internal void ChangeWeight(Router b, int v)
        {
            foreach (var neighbor in neighbors)
            {
                if (neighbor.Key == b.Id)
                {
                    neighbors[neighbor.Key] = v;
                    break;
                }
            }
            UpdateVector(b.Id, v);
        }

        private void UpdateVector(int id, int v)
        {
            for (int i = 0; i < Vector.Length/3; i++)
            {
                if (Vector[i, 0] == id)
                {
                    Vector[i, 1] = v;
                }
                if (Vector[i,2] == id)
                {
                    Vector[i, 1] += v;
                }
            }
        }

        public void AddNeighbor(Router router, int weight)
        {
            neighbors.Add(router.Id,weight);
        }
        public void PrintVector()
        { 
            Console.WriteLine("Dest Cost NextHop");
            for (int i = 0; i < Vector.Length/3; i++)
            {
                Console.Write(Vector[i,0]+" |-| ");
                Console.Write(Vector[i,1]+" |-| ");
                Console.WriteLine(Vector[i,2]+" \n");
                
            }
        }
    }
}
