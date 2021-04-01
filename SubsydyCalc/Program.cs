using System;
using ClassLibrary1;

namespace SubsydyCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Volume volume = new Volume();
            Tariff tariff = new Tariff();
            SubsidyCalculation sb = new SubsidyCalculation();


            sb.OnNotify += ShowMessage;
            sb.OnException += SomeException;

            volume.HouseId = 1;
            tariff.HouseId = 1;
            volume.ServiceId = 1;
            tariff.ServiceId = 1;
            volume.Value = 20;
            volume.Month = new DateTime(2020, 10, 1);
            tariff.PeriodBegin = new DateTime(2020, 6, 1);
            tariff.PeriodEnd = new DateTime(2020, 10, 1);
            tariff.Value = 200;
            Charge result = sb.CalculateSubsidy(volume, tariff);
            Console.WriteLine($"{result.HouseId}, {result.ServiceId}, {result.Month}, {result.Value}");
        }

        /// <summary>
        /// Обработчик события OnNotify. Показывает время начала расчета / конца расчета.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ShowMessage(object sender, string e)
        {
            DateTime time = new DateTime();
            time = DateTime.Now;
            switch (e)
            {
                case "start": 
                    Console.WriteLine($"Расчет начат в {time:HH-mm-ss}!");
                    break;
                case "end":
                    Console.WriteLine($"Расчет успешно завершен в {time:HH-mm-ss}!");
                    break;
            }
        }



        /// <summary>
        /// Обработчик события OnException. Вызывается при некотором Exception.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SomeException(object sender, Tuple<string, Exception> e)
        {
            if ((string)sender == "defException")
            {
                switch (e.Item1)
                {
                    case "difhouse":
                        Console.WriteLine($"{e.Item2.Message}");
                        break;
                    case "difservice":
                        Console.WriteLine($"{e.Item2.Message}");
                        break;
                }
            }
            else
            {
                Console.WriteLine($"Ошибка: {e.Item1}: {e.Item2.Message}");
            }
        }
    }
}
