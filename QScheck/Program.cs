using System;
using QSLib;

namespace QScheck
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = new int[10];
            Random r = new Random();
            for(int i=0; i < 10; i++)
            {
                arr[i] = r.Next(20);
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            arr = QSLib.QuickSort.QSort(arr, 0, 9);
            for(int i = 0; i < 10; i++)
            {
                Console.Write(arr[i] + " ");
            }
        }
    }
}
