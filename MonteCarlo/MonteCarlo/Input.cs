using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonteCarlo
{

    internal class Input
    {
        public int HighCase { get; set; }
        public int LowCase { get; set; }
        public int AvgCase { get; set; }
        public int[] Tasks { get; set; }

        public Input(string Input)
        {
            SetArray(Input);
        }

        public void SetArray(string Input)
        {
            Input = Input.Trim();
            string[] Estimations = Input.Split(',');
            if (Estimations.Length < 2) throw new InvalidOperationException("The Task must be Separated with a comma ,");
            this.Tasks = new int[Estimations.Length];
            for (int i = 0; i < Estimations.Length; i++)
            {
                if (!int.TryParse(Estimations[i], out Tasks[i])) Console.WriteLine("wrong entry ");
            }
            this.HighCase = Tasks.Min();
            this.LowCase = Tasks.Max();
            this.AvgCase = (int)Tasks.Average();
        }
    }
}
