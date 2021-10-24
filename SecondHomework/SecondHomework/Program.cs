namespace SecondHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();

            test.PurchaseTest();
            test.SupplyTest();

            foreach (var thread in test.Threads)
            {
                thread.Join();
            }
        }
    }
}
