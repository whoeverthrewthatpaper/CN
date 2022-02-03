using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNG
{
    class Built_in : IRNG
    {
        Random rng = new Random();
        public long Next()
        {
            return rng.Next(int.MinValue, int.MaxValue);
        }
        public long Next(int maxValue)
        {
            return rng.Next(0, maxValue);
        }
        public long Next(int minValue, int maxValue)
        {
            return rng.Next(minValue, maxValue);
        }

        public void Visualize(int minValue, int maxValue, int n)
        {
            Stopwatch sw = new Stopwatch();
            int[] counter = new int[maxValue - minValue];
            sw.Restart();
            for (int i = 0; i < n; i++)
            {
                long number = Next(minValue, maxValue);
                counter[number - minValue]++;
            }
            sw.Stop();
            int id = 0;
            Console.WriteLine("Results: ");
            for (int i = minValue; i < maxValue; i++)
            {
                Console.Write($"{i}: ");
                Console.Write($"{counter[id++] * 100.0 / n}%");
                Console.WriteLine();
            }
            Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} milliseconds");
        }
    }
}
