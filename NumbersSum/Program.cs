using System;
using StackExchange.Redis;

namespace NumbersSum
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
                int num1 = Convert.ToInt32(db.StringGet("num1").ToString());
                int num2 = Convert.ToInt32(db.StringGet("num2").ToString());

                Console.WriteLine($"Сумма: {num1 + num2}");
            }
        }
    }
}
