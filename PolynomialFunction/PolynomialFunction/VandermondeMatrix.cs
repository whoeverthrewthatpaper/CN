namespace PolynomialFunction
{
    internal class VandermondeMatrix : Matrix
    {
        public void toVandermonde()
        {
            for (int i = 0; i < this.EquationsList.Count; i++)
            {
                this.SetEquation(i, new Equation($"{Math.Pow(EquationsList[i].Array.First(), 2)} {EquationsList[i].Array.First()} {1} {EquationsList[i].Array.Last()}"));
            }
        }
    }
}
