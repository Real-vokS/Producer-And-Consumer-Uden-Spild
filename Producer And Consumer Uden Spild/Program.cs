using System;
using System.Threading;

namespace Producer_And_Consumer_Uden_Spild
{
    class Program
    {

        static object[] products = new object[3];

        static void Main(string[] args)
        {

            Thread t1 = new Thread(Produce);
            Thread t2 = new Thread(Consume);

            t1.Name = "Producer";
            t2.Name = "Consumer";

            t1.Start();
            Thread.Sleep(50);
            t2.Start();

            t1.Join();
            t2.Join();
        }

        static void Produce()
        {
            while (true)
            {
                while (true)
                {
                    bool isAllNull = true;
                    for (int i = 0; i < products.Length; i++)
                    {
                        if (products[i] != null)
                        {
                            isAllNull = false;
                        }

                    }
                    if (isAllNull == true)
                    {
                        break;
                    }

                    Thread.Sleep(100 / 15);

                }


                Monitor.Enter(products);
                for(int i = 0; i < products.Length; i++)
                {
                    object obj = new object();
                    products[i] = obj;
                    Console.WriteLine("Item added #" + i);

                }
                Monitor.Exit(products);

                Thread.Sleep(200);
            }
        }
        static void Consume()
        {
            while (true)
            {
                while (true)
                {
                    bool isOneNull = false;
                    for (int i = 0; i < products.Length; i++)
                    {
                        if (products[i] == null)
                        {
                            isOneNull = true;
                        }

                    }
                    if (isOneNull == false)
                    {
                        break;
                    }

                    Thread.Sleep(100 / 15);
                }

                Monitor.Enter(products);
                for (int i = 0; i < products.Length; i++)
                {
                    products[i] = null;
                    Console.WriteLine("Item Removed #" + i);
                }
                Monitor.Exit(products);

                Thread.Sleep(200);

            }
        }
    }
}
