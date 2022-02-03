// See https://aka.ms/new-console-template for more information
using NumericalIntegration;



internal class PolynomialIntegralClass : INtegralCalculator
{
    public List<int> PolynomialCoefficients { get; set; }
    private readonly double[] _partition;
    private readonly int sizeFunc;
    public PolynomialIntegralClass(List<int> newPolynomialCoeffients, double[] boundaries)
    {
        PolynomialCoefficients = newPolynomialCoeffients;
        sizeFunc = PolynomialCoefficients.Count;
        _partition = boundaries;
    }
    public PolynomialIntegralClass(double[] boundaries)
    {
        _partition = boundaries;
    }
    public void DisplayFunction()
    {
        int exponential = PolynomialCoefficients.Count - 1;
        Console.WriteLine("The function:");
        string polynomialFunction = $"f(x) = {PolynomialCoefficients[0]}x^{exponential}";
        exponential--;
        for (int i = 1; i < sizeFunc - 1; i++)
        {
            int coefficient = PolynomialCoefficients[i];
            _ = (coefficient > 0) ?
                polynomialFunction += $" + {coefficient}x^{exponential}" :
                polynomialFunction += $" - {Math.Abs(coefficient)}x^{exponential}";
            exponential--;
        }
        int polynomial = PolynomialCoefficients[sizeFunc - 1];
        _ = (polynomial > 0) ?
        polynomialFunction += $" + {polynomial}" :
        polynomialFunction += $" - {Math.Abs(polynomial)}";
        Console.WriteLine(polynomialFunction);
    }
    private double GetFunctionValue(double x)
    {
        int exponent = PolynomialCoefficients.Count - 1;
        double result = 0;
        for (int i = 0; i < sizeFunc; i++)
        {
            result += PolynomialCoefficients[i] * Math.Pow(x, exponent);
            exponent--;
        }
        return result;
    }
    private double GetFunctionValue(string infixFunction, double x)
    {
        string postfixFunction = RPNCalculatorClass.InfixToPostfix(infixFunction, x);
        return RPNCalculatorClass.PostfixCalculator(postfixFunction);
    }
    public double SimpsonMethod(int Intervals)
    {
        double t = (_partition[1] - _partition[0]) / Intervals;
        double y = GetFunctionValue(_partition[0]) + GetFunctionValue(_partition[1]);
        for (int i = 1; i < Intervals; i++)
        {
            int coefficient = (i % 2 == 0) ? 2 : 4;
            y += coefficient * GetFunctionValue(_partition[0] + i * t);
        }
        return (t * y) / 3;
    }
    public double TrapezodalMethod(int numberOfInterval)
    {
        double t = (_partition[1] - _partition[0]) / numberOfInterval;
        double y = GetFunctionValue(_partition[0]) + GetFunctionValue(_partition[1]);
        for (int i = 1; i < numberOfInterval - 1; i++)
        {
            y += 2 * GetFunctionValue(_partition[0] + i * t);
        }
        return (t * y) / 2;
    }
    public double SimpsonMethod(string infixFunction, int numberOfInterval)
    {
        double t = (_partition[1] - _partition[0]) / numberOfInterval;
        double y = GetFunctionValue(infixFunction, _partition[0]) + GetFunctionValue(infixFunction, _partition[1]);
        for (int i = 1; i < numberOfInterval; i++)
        {
            int coefficient = (i % 2 == 0) ? 2 : 4;
            y += coefficient * GetFunctionValue(infixFunction, _partition[0] + i * t);
        }
        return (t * y) / 3;
    }
    public double TrapezodalMethod(string infixFunction, int numberOfInterval)
    {
        double t = (_partition[1] - _partition[0]) / numberOfInterval;
        double y = GetFunctionValue(infixFunction, _partition[0]) + GetFunctionValue(infixFunction, _partition[1]);
        for (int i = 1; i < numberOfInterval - 1; i++)
        {
            y += 2 * GetFunctionValue(infixFunction, _partition[0] + i * t);
        }
        return (t * y) / 2;
    }
}
