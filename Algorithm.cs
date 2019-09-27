using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerNetworks
{
    public class Algorithm
    {
        public void SendTables(List<Router> routers)
        {
            foreach(var routerr in routers)
            {
                
           int[,] Mainvector = routerr.Vector;

           Dictionary<int, int> neig = routerr.neighbors;

            foreach (var neigbor in neig)
            {
                foreach (var router in routers)
                {
                    if(router.Id == neigbor.Key)
                    {
                        Router CRouter = router;
                        int[,] Cvector = CRouter.Vector;
                        for (int i = 0;i<Mainvector.Length/3;i++)
                        {
                            if (Mainvector[i,0] == CRouter.Id)
                            {
                                int weight = Mainvector[i, 1];
                                for (int j = 0;j< Cvector.Length/3;j++)
                                {
                                    if (Cvector[j,1] != -9999)
                                    {
                                        int partialSum = Cvector[j, 1] + weight;
                                        if (Mainvector[j,1] == -9999 || Mainvector[j,1] > partialSum)
                                        {
                                            Mainvector[j, 1] = partialSum;
                                            Mainvector[j, 2] = CRouter.Id;
                                        }
                                    }
                                    
                                }
                            }
                        }

                    }
                }
            }
        }

    }
}
}
