using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNG
{
    interface IRNG
    {
        public long Next();
        public long Next(int maxValue);
        public long Next(int minValue, int maxValue);
        public void Visualize(int minValue, int maxValue, int n);
    }
}
