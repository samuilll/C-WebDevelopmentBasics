using System;
using System.Threading;

namespace Task01_EvenNumbersThread
{
    class StartUp
    {

        static void Main(string[] args)
        {
            int firstNumber = int.Parse(Console.ReadLine());
            int lastNumber = int.Parse(Console.ReadLine());

            Thread thread = new Thread(() => PrintEvenNumbers(firstNumber, lastNumber));

            thread.Start();
            thread.Join();
        }

        private static void PrintEvenNumbers(int firstNumber, int lastNumber)
        {
            for (int i = firstNumber; i <= lastNumber; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
    
}
