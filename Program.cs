using System;
using System.Collections.Generic;

namespace ComputerNetworks
{
    class Program
    {
        public static List<Router> routers = new List<Router>();
        public const string Meniu = "[1] Run simulation (RightArrow - Add time, Escape - back to Meniu) \n" +
                                    "[2] Add Packet (Source Destination Information)\n"+
                                    "[3] Change Weight (Vertex1 Vertex2 Weight) \n";
        public static void Sender()
        {
            foreach (var rout in routers)
            {
                rout.Send(routers);
            }
            foreach (var rout in routers)
            {
                rout.IsReceived = false;
            }
            PrintPackets();
        }
        public static void PrintPackets()
        {
            foreach (var rout in routers)
            {
                if (rout.ContainedPackets.Count == 0)
                {
                    Console.WriteLine(rout.Id + " There is not packets in router");
                }
                else
                {
                    for (int i = 0; i < rout.ContainedPackets.Count; i++)
                    {
                        Console.WriteLine(rout.Id + " packets : " + rout.ContainedPackets[i].ToString());

                    }
                }
            }
            Console.WriteLine("\n\n\n");
        }
        static void Main(string[] args)
        {
            Algorithm e = new Algorithm();
            Router A = new Router();
            Router B = new Router();
            Router C = new Router();
            Router D = new Router();
            Router E = new Router();

            int NumberOfVertex = 5;
            Comercial packet = new Comercial("Yikes", 0, 3);
            routers.Add(A);
            routers.Add(B);
            routers.Add(C);
            routers.Add(D);
            routers.Add(E);

            A.AddNeighbor(B, 5);
            A.AddNeighbor(C, 3);

            A.InitializeVector(NumberOfVertex);

            B.AddNeighbor(A, 5);
            B.AddNeighbor(C, 4);
            B.AddNeighbor(D, 1);


            B.InitializeVector(NumberOfVertex);

            C.AddNeighbor(A, 3);
            C.AddNeighbor(B, 4);
            C.AddNeighbor(D, 5);

            C.InitializeVector(NumberOfVertex);

            D.AddNeighbor(B, 1);
            D.AddNeighbor(C, 5);
            D.AddNeighbor(E, 3);

            D.InitializeVector(NumberOfVertex);

            E.AddNeighbor(D, 3);

            E.InitializeVector(NumberOfVertex);

            A.AddPacket(packet);

            

            int i = 0;
            bool loop = true;
            while (true)
            {
                Console.WriteLine(Meniu);
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("[1] Run simulation(RightArrow -Add time, Escape - back to Meniu) \n");
                        while (loop)
                        {
                        ConsoleKeyInfo input = Console.ReadKey();
                        switch (input.Key)
                        {
                            case ConsoleKey.RightArrow:

                                    
                                    if (i % 3 == 0)
                                    {
                                        foreach (var rout in routers)
                                        {
                                            e.SendTables(routers);
                                        }
                                    }
                                 
                                    Sender();
                                    foreach (var rout in routers)
                                    {
                                        rout.PrintVector();
                                    }
                                    PrintPackets();
                                    Console.WriteLine("Time: " + i);
                                    i++;
                                    break;
                            case ConsoleKey.Escape:
                                    loop = false;
                                    break;
                            default:
                                    Console.WriteLine("\nWrong input (RightArrow - Add time, Escape - back to Meniu)\n");
                                break;
                        }  
                        }
                        loop = true;
                        break;
                    case "2":
                        Console.WriteLine("Enter Source:\n");
                        string temp = Console.ReadLine();
                        int source;
                        int.TryParse(temp, out source);
                        Console.WriteLine("Enter Destination:\n");
                        temp = Console.ReadLine();
                        int destination;
                        int.TryParse(temp,out destination);
                        Console.WriteLine("Enter Message:\n");
                        temp = Console.ReadLine();
                        CreatePacket(source, destination, temp);
                        break;
                    case "3":
                        Console.WriteLine("Vertex1: \n");
                        string tempo = Console.ReadLine();
                        int vertex1;
                        int.TryParse(tempo,out vertex1);
                        Console.WriteLine("Vertex2: \n");
                        tempo = Console.ReadLine();
                        int vertex2;
                        int.TryParse(tempo, out vertex2);
                        Console.WriteLine("Weight: \n");
                        tempo = Console.ReadLine();
                        int weight;
                        int.TryParse(tempo, out weight);
                       ChangeWeight(vertex1,vertex2,weight);
                        break;
                    default:
                        Console.WriteLine("\nWrong input!\n");
                        break;
                }
                

            }
        }

        private static void ChangeWeight(int vertex1, int vertex2, int weight)
        {
            Router vertex1R = null;
            Router vertex2R = null;
            foreach (var router in routers)
            {
                if (router.Id == vertex1)
                {
                     vertex1R = router;
                }
                else if (router.Id == vertex2)
                {
                     vertex2R = router;
                }
            }
            vertex1R.ChangeWeight(vertex2R, weight);
            vertex2R.ChangeWeight(vertex1R,weight);
            vertex1R.PrintVector();
        }

        private static void CreatePacket(int source, int destination, string temp)
        {
            Comercial packet = new Comercial(temp,source, destination);
            foreach (var router in routers)
            {
                if (router.Id == source)
                {
                    router.AddPacket(packet);
                }
            }
            PrintPackets();

        }
    }
}
