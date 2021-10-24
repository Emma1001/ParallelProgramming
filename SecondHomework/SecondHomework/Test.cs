using System;
using System.Collections.Generic;
using System.Threading;

namespace SecondHomework
{
    public class Test
    {
        private Random random = new Random();
        private Shop shop;
        private string[] productNames = new string[20];

        public List<Thread> Threads { get; set; }

        public Test()
        {
            Threads = new List<Thread>();
            shop = new Shop();

            for (int i = 0; i < 20; i++)
            {
                productNames[i] = ("Product" + i);
            }
        }

        public void PurchaseTest()
        {
            for (int i = 1; i < 81; i++)
            {
                Thread thread = new Thread(() => shop.Purchase(productNames[random.Next(0, 20)], random.Next(1, 5)));
                Threads.Add(thread);
                thread.Start();
            }
        }

        public void SupplyTest()
        {
            for (int i = 1; i < 5; i++)
            {
                Thread thread = new Thread(() => shop.Supply(productNames[random.Next(0, 20)], random.Next(1, 5)));
                Threads.Add(thread);
                thread.Start();
            }
        }
    }
}
