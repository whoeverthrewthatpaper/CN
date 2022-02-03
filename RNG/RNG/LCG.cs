using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNG
{
    class LCG : IRNG
    {
        private const long increment = 1013904223;
        private const long multiplier = 1664525;
        private long seed;

        public LCG(long seed = 110103)
        {
            this.seed = seed;
        }
        public long Next()
        {
            seed = ((multiplier * seed) + increment) % 4294967296;
            return seed;
        }

        public long Next(int maxValue)
        {
            return Next() % maxValue;
        }
        public long Next(int minValue, int maxValue)
        {
            return minValue + Next() % (maxValue - minValue);
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
            };
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
