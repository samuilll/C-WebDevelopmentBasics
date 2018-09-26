using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Task02_SliceFile
{
    class StartUp
    {
        static void Main()
        {

          Task task = SliceAsync();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
            }

            task.Wait();
           //Task<long> task = GetPrimesAsync(0,100);

           //string input = Console.ReadLine();

           // while (!string.Equals(input,"end"))
           // {

           //     if (string.Equals(input,"show"))
           //     {
           //         Console.WriteLine(task.IsCompleted);
           //         if (task.IsCompleted)
           //         {
           //             Console.WriteLine(task.Result);
           //         }
           //         else
           //         {
           //             Console.WriteLine("loading...");
           //         }
           //     }

           //     input = Console.ReadLine();
           // }
        }

        private static async  Task  SliceAsync()
        {
            string fileName = Console.ReadLine();
            string folderDestination = Console.ReadLine();
            int numberOfPieces = int.Parse(Console.ReadLine());
            
             await Task.Run(() => Slice(fileName, folderDestination, numberOfPieces));
         
            return;
        }

        private static void Slice(string fileToDividePath, string writeDirectoryPath, int partsNumber)
        {
            using (var reader = new FileStream(fileToDividePath, FileMode.Open, FileAccess.Read))
            {
                if (!Directory.Exists(writeDirectoryPath))
                {
                    Directory.CreateDirectory(writeDirectoryPath);
                }

                var partSize = reader.Length / partsNumber + 1;

                for (int i = 0; i < partsNumber; i++)
                {
                    using (var writer = new FileStream($"{writeDirectoryPath}/part - {i.ToString()}.mkv", FileMode.Create))
                    {
                        var buffer = new byte[4096];

                        while (writer.Length < partSize)
                        {

                            var readBytes = reader.Read(buffer, 0, buffer.Length);

                            if (readBytes == 0)
                            {
                                break;
                            }

                            writer.Write(buffer, 0, buffer.Length);
                        }

                        Console.WriteLine($"File  {i} completed");

                    }

                }
            }
        }

       private static async Task<long> GetPrimesAsync(int start, int end)
        {
            long sum = 0;

            await Task<long>.Run(() =>
            {
                // 1. Create a list of consecutive integers from 2 through n: (2, 3, 4, ..., n).
                int[] numbers = Enumerable.Range(0, end).ToArray();

                // 2. Initially, let p equal 2, the first prime number.
                int idx = 2;
                do
                {
                    // If the number is in range, add it to the result
                    if (numbers[idx] >= start)
                    {
                        sum += idx;
                    }

                    // 3. Starting from p, enumerate its multiples by counting to n in increments of p, and mark them in the list (these will be 2p, 3p, 4p, ... ; the p itself should not be marked).
                    for (var i = idx; i < end; i += idx)
                    {
                        Thread.Sleep(100);
                        numbers[i] = 0;
                    }

                    // 4. Find the first number greater than p in the list that is not marked. If there was no such number, stop. Otherwise, let p now equal this new number (which is the next prime), and repeat from step 3.
                    while (idx < end && numbers[idx] == 0)
                    {
                        idx++;
                    }
                }
                while (idx < end);

                return sum;
            });

            return sum;
        }
    }
}