using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNG
{
    class Xorshift : IRNG
    {
        private ulong seed = 110103;
        public Xorshift(ulong seed = 110103)
        {
            this.seed = seed;
        }
        public long Next()
        {
            seed ^= seed << 13;
            seed ^= seed >> 17;
            seed ^= seed << 5;
            return (long)(seed % long.MaxValue);
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
