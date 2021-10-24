using System;
using System.Collections.Generic;

namespace SecondHomework
{
    public class Shop
    {
        private Random random = new Random();
        private object locker = new object();

        public List<Product> Products { get; set; }

        public Shop()
        {
            Products = new List<Product>();
            for (int i = 0; i < 20; i++)
            {
                Products.Add(new Product("Product" + i, random.Next(1, 10)));
            }
        }

        public void Purchase(string productName, int quantity)
        {
            var existingProduct = GetProduct(productName);

            if (existingProduct == null)
            {
                Console.WriteLine($"Product with name {productName} not found!");
                return;
            }

            lock (locker)
            {
                if(existingProduct.Quantity >= quantity)
                {
                    existingProduct.Quantity -= quantity;
                    Console.WriteLine("PURCHASED");
                }
            }
        }

        public void Supply(string productName, int quantity)
        {
            var existingProduct = GetProduct(productName);

            if (existingProduct == null)
            {
                Console.WriteLine($"Product with name {productName} not found!");
                return;
            }

            lock (locker)
            {
                existingProduct.Quantity += quantity;
                Console.WriteLine("SUPPLIED");
            }
        }

        private Product GetProduct(string name) => Products.Find(p => p.Name == name);
    }
}
