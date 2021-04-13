using System;
using StackExchange.Redis;

namespace TwoNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();


            Console.WriteLine("Введите 'exit' для выхода.");
            while (Console.ReadLine() != "exit")
            {
                Console.Write("Введите первое число: ");
                db.StringSet("num1", Console.ReadLine());
                Console.Write("Введите второе число: ");
                db.StringSet("num2", Console.ReadLine());
            }
        }
    }
}
