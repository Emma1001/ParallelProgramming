using System;
using System.Linq;
using System.Threading;

namespace Base51
{
    class Program
    {
        static void Main(string[] args)
        {
            Elevator elevator = new Elevator();
            Agent[] agents = new Agent[]
            {
                new Agent ("Agent 1", SecurityLevelEnum.Confidential, elevator),
                new Agent ("Agent 2", SecurityLevelEnum.Secret, elevator),
                new Agent ("Agent 3", SecurityLevelEnum.TopSecret, elevator),
            };

            var threads = agents.Select(agent => new Thread(agent.MakeAction)).ToArray();
            foreach (var t in threads) t.Start();
            foreach (var t in threads) t.Join();

            Console.WriteLine("Everybody went home! The base is closed.");
            Console.ReadKey();
        }
    }
}
