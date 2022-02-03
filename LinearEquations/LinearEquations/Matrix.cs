namespace LinearEquations
{
    internal class Matrix
    {
        private readonly List<Equation> EquationsList = new List<Equation>();

        private int width;

        public int Width
        {
            get
            {
                return this.width;
            }
        }

        public int Length
        {
            get
            {
                return EquationsList.Count;
            }
        }

        public Equation GetEquation(int index)
        {
            return EquationsList[index];
        }

        public Equation LayoutOfEquation(int index, Equation LinEquation)
        {
            return EquationsList[index] = LinEquation;
        }

        public void ForwardElimination()
        {
            int Rows = this.Length;
            int Column = this.Width;

            for (int LRow = 0; LRow < Rows; LRow++)
            {
                for (int i = LRow + 1; i < Column - 1; i++)
                {
                    double Element = this.GetEquation(i).Array[LRow] / this.GetEquation(LRow).Array[LRow];

                    for (int LCol = LRow; LCol < Column; LCol++)
                        this.GetEquation(i).Array[LCol] -= Element * this.GetEquation(LRow).Array[LCol];
                }
            }
        }

        private double[] SolveMatrix()
        {
            int length = this.GetEquation(0).Array.Length;

            for (int i = 0; i < this.Length - 1; i++)
            {
                if (this.GetEquation(i).Array[i] == 0 && !PivotProcedure(this, i, i))
                    throw new Exception("Can not calculate");
                for (int j = i; j < this.Length; j++)
                {
                    double[] SetUp = new double[length];

                    for (int x = 0; x < length; x++)
                    {
                        SetUp[x] = this.GetEquation(j).Array[x];
                        if (this.GetEquation(j).Array[i] != 0)
                            SetUp[x] = SetUp[x] / this.GetEquation(j).Array[i];
                    }
                    _ = this.LayoutOfEquation(j, new Equation(SetUp));
                }
                for (int y = i + 1; y < this.Length; y++)
                {
                    double[] f = new double[length];
                    for (int g = 0; g < length; g++)
                    {
                        f[g] = this.GetEquation(y).Array[g];
                        if (this.GetEquation(y).Array[i] != 0)
                            f[g] = f[g] - this.GetEquation(i).Array[g];
                    }
                    this.LayoutOfEquation(y, new Equation(f));
                }
            }

            return ReverseSubstitution(this);
        }

        private bool PivotProcedure(Matrix matrix, int row, int column)
        {
            bool swapped = false;
            for (int z = matrix.Length - 1; z > row; z--)
            {
                if (matrix.GetEquation(z).Array[row] != 0)
                {
                    _ = new double[matrix.GetEquation(0).Array.Length];
                    double[] temp = matrix.GetEquation(z).Array;
                    matrix.LayoutOfEquation(z, matrix.GetEquation(column));
                    matrix.LayoutOfEquation(column, new Equation(temp));
                    swapped = true;
                }
            }

            return swapped;
        }

        public double[] ReverseSubstitution(Matrix matrix)
        {
            int length = matrix.GetEquation(0).Array.Length;
            double[] result = new double[matrix.Length];
            for (int i = matrix.Length - 1; i >= 0; i--)
            {
                double val = matrix.GetEquation(i).Array[length - 1];
                for (int x = length - 2; x > i - 1; x--)
                {
                    if (result.Length <= x || matrix.GetEquation(i).Array.Length <= x)
                        break;

                    val -= matrix.GetEquation(i).Array[x] * result[x];
                }
                result[i] = val / matrix.GetEquation(i).Array[i];

                if (!Confirmation(result[i]))
                    throw new Exception("Can not calculate this");
            }
            return result;
        }

        private bool Confirmation(double result)
        {
            return !(double.IsNaN(result) || double.IsInfinity(result));
        }

        public void ShowResults()
        {
            double[] result = this.SolveMatrix();

            for (int i = 0; i < result.Length; i++)
                Console.WriteLine($"x{i + 1} = {result[i]}");
        }

        public void JoinEquation(Equation LinEquation)
        {
            if (this.EquationsList.Count == 0)
                this.width = LinEquation.Array.Length;

            if (LinEquation.Array.Length != this.width)
                throw new Exception("Invalid equation , wrong dimensions");

            this.EquationsList.Add(LinEquation);
        }

        public void TakeOutEquation(Equation LinEquation)
        {
            this.EquationsList.Remove(LinEquation);
        }

        public override string ToString()
        {
            String Result = "";
            foreach (Equation e in EquationsList)
                Result += String.Join(" ", e.Array) + "\n";

            return Result;
        }
    }
}
