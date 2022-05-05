using System;

namespace Async
{
    class Program
    {
        // await khác wait : tại thời điểm await phương thức trả về Task luôn -> không khóa đi thread chính
        static void CatchTaxi(string mgs)
        {
            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{mgs} ... Start booking");
                Console.ResetColor();
            }

            for (int i = 1; i <= 5; i++)
            {
                lock (Console.Out)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{mgs} Time: {i}");
                    Console.ResetColor();
                }


                Thread.Sleep(1000);
            }
            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{mgs} ... Driver found");
                Console.ResetColor();

            }


        }
        static void BuyCoffe(string mgs)
        {

            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{mgs} ... Start buying coffee");
                Console.ResetColor();
            }

            for (int i = 1; i <= 7; i++)
            {
                lock (Console.Out)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{mgs} Time: {i}");
                    Console.ResetColor();
                }


                Thread.Sleep(1000);
            }
            lock (Console.Out)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{mgs} ... buying coffe done");
                Console.ResetColor();

            }
        }


        static async Task<double> Task1()
        {
            Task<double> t1 = new Task<double>(
                () =>
                {
                    double money = 10;
                    CatchTaxi("Booking...");
                    return money;
                }
                );
            t1.Start();
            var kq = await t1;
            
            Console.WriteLine("--------------Booking done-----------------");
            return kq;
        }
        static async Task<double> Task2()
        {
            Task<double> t2 = new Task<double>(
                () =>
                {
                    double money = 5;
                    BuyCoffe("Buying coffee...");
                    return money;
                }
                );
            t2.Start();
            var kq = await t2;
            Console.WriteLine("--------------Buy coffee done-----------------");
            return kq;
        }
        static async Task Main(String[] args)
        {
            Task<double> t1 = Task1();
            Task<double> t2 = Task2();




            Task.WaitAll(t1, t2);
            var moneyt1 = await t1;
            var moneyt2 = await t2;
            Console.WriteLine($"Total money in 2 task: {moneyt1 + moneyt2}$");

            Console.WriteLine("All task was done");
            Console.ReadKey();

        }
    }
}