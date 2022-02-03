// See https://aka.ms/new-console-template for more information
using PolynomialFunction;


internal class Program
{
    private static void Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }
        Console.WriteLine("input points in x y representation." +

            "Type END to finish.");
        int index = 1;
        try
        {
            VandermondeMatrix matrix = new VandermondeMatrix();
            while (true)
            {
                Console.Write($"P#{index++}: ");
                String input = Console.ReadLine();
                if (input.Trim().ToUpper() == "END" || input.Trim() == "")
                    break;
                matrix.AddEquation(new PointClass(input));
            }
            matrix.toVandermonde();
            Console.WriteLine("Resulting polynomial will be of the order - " + PolynomialClass.CalculateOrder(matrix) + "\nCalculated polynomial:");
            double[] coefficients = matrix.GetResults();
            Console.WriteLine(PolynomialClass.Format(coefficients));
            Dictionary<int, double> calculatedPolynomial = PolynomialClass.Calculate(coefficients);
            foreach (KeyValuePair<int, double> value in calculatedPolynomial)
                Console.WriteLine($"f({value.Key}) = {value.Value:0.000}");
            Console.WriteLine("Derivative:\n" + PolynomialClass.CalculateDerivative(coefficients));
            double inititalGuess = 2;
            Console.WriteLine("Looking for a root with initial guess 2");
            Console.WriteLine("Root found for x = " + PolynomialClass.CalculateRoot(coefficients, inititalGuess).ToString("0.00000"));
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
