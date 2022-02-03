﻿namespace PolynomialFunction
{
    internal class Matrix
    {
        protected List<Equation> EquationsList = new List<Equation>();

        private int width = 0;

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

        public Equation SetEquation(int index, Equation e)
        {
            return EquationsList[index] = e;
        }

        public void ForwardElimination()
        {
            int First = this.Length;
            int Second = this.Width;

            for (int k = 0; k < First; k++)
            {
                for (int i = k + 1; i < Second - 1; i++)
                {
                    double factor = this.GetEquation(i).Array[k] / this.GetEquation(k).Array[k];

                    for (int j = k; j < Second; j++)
                        this.GetEquation(i).Array[j] -= factor * this.GetEquation(k).Array[j];
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
                    double[] e = new double[length];

                    for (int x = 0; x < length; x++)
                    {
                        e[x] = this.GetEquation(j).Array[x];
                        if (this.GetEquation(j).Array[i] == 0)
                            continue;
                        e[x] = e[x] / GetEquation(j).Array[i];
                    }
                    this.SetEquation(j, new Equation(e));
                }
                for (int z = i + 1; z < this.Length; z++)
                {
                    double[] f = new double[length];
                    for (int g = 0; g < length; g++)
                    {
                        f[g] = this.GetEquation(z).Array[g];
                        if (this.GetEquation(z).Array[i] != 0)
                            f[g] = f[g] - this.GetEquation(i).Array[g];
                    }
                    this.SetEquation(z, new Equation(f));
                }
            }

            return BackwardSubstitution(this);
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
                    matrix.SetEquation(z, matrix.GetEquation(column));
                    matrix.SetEquation(column, new Equation(temp));
                    swapped = true;
                }
            }
            return swapped;
        }
        public double[] BackwardSubstitution(Matrix matrix)
        {
            int length = matrix.GetEquation(0).Array.Length;
            double[] result = new double[matrix.Length];
            for (int i = matrix.Length - 1; i >= 0; i--)
            {
                double val = matrix.GetEquation(i).Array[length - 1];
                for (int j = length - 2; j > i - 1; j--)
                    val -= matrix.GetEquation(i).Array[j] * result[j];

                result[i] = val / matrix.GetEquation(i).Array[i];
            }
            return result;
        }
        public void PrintResults()
        {
            double[] result = this.SolveMatrix();

            for (int i = 0; i < result.Length; i++)
                Console.WriteLine($"x{i + 1} = {result[i]:0.0000}");
        }
        public double[] GetResults()
        {
            return this.SolveMatrix();
        }
        public void AddEquation(Equation e)
        {
            if (this.EquationsList.Count == 0)
            {
                this.width = e.Array.Length;
            }

            if (e.Array.Length != this.width)
                throw new Exception("Invalid equation, wrong dimensions");

            this.EquationsList.Add(e);
        }

        public void AddEquation(PointClass p)
        {
            this.EquationsList.Add(new Equation($"{p.X} {p.Y}"));
        }

        public void RemoveEquation(Equation e)
        {
            this.EquationsList.Remove(e);
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
