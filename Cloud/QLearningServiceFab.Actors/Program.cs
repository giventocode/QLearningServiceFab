using System;
using System.Fabric;
using System.Threading;
using Microsoft.ServiceFabric.Actors;

namespace QLearningServiceFab.Actors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (FabricRuntime fabricRuntime = FabricRuntime.Create())
                {
                    fabricRuntime.RegisterActor<QTicTacToeState>();
                    fabricRuntime.RegisterActor<QTrainedState>();
                    

                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
