﻿namespace PolynomialFunction
{
    internal class PolynomialClass
    {
        static public int CalculateOrder(Matrix matrix)
        {
            if (matrix == null || matrix.Length == 0) throw new Exception("Unable to calculate");

            return matrix.Length - 1;
        }

        static public string Format(double[] values)
        {
            if (values == null) return String.Empty;
            if (values.Length == 0) throw new Exception("Unable to format");

            string baseString = "f(x) = ";

            for (int i = 0; i < values.Length; i++)
            {
                baseString += values[i].ToString("0.0000");

                if (i != values.Length - 1)
                    baseString += $"x^{values.Length - 1 - i}";
            }

            return baseString;
        }

        static public Dictionary<int, double> Calculate(double[] values)
        {
            if (values == null || values.Length == 0) throw new Exception("Unable to calculate");

            Dictionary<int, double> result = new Dictionary<int, double>(3);

            for (int i = -1; i <= 1; i++)
            {
                double value = 0;
                for (int j = 0; j < values.Length; j++)
                {
                    if (j != values.Length - 1)
                        value += values[j] * Math.Pow(i, values.Length - 1 - j);
                    else
                        value += values[j];
                }

                result.Add(i, value);
            }

            return result;
        }

        static public double Calculate(double[] values, double i)
        {
            if (values == null || values.Length == 0) throw new Exception("Unable to calculate");

            double value = 0;
            for (int j = 0; j < values.Length; j++)
            {
                if (j != values.Length - 1)
                    value += values[j] * Math.Pow(i, values.Length - 1 - j);
                else
                    value += values[j];
            }

            return value;
        }

        static public string CalculateDerivative(double[] values)
        {
            if (values == null) return String.Empty;
            if (values.Length == 0) throw new Exception("Unable to calculate");

            string baseString = "f'(x) = ";

            for (int i = 0; i < values.Length - 1; i++)
            {
                baseString += (values[i] < 0 || i == 0) ? values[i].ToString("") : $"+{values[i]}";

                if (i != values.Length - 2)
                    baseString += $"x^{values.Length - 2 - i}";
            }

            return baseString;
        }

        static public double CalculateDerivative(double[] values, double value)
        {
            if (values == null || values.Length == 0) throw new Exception("Unable to calculate");

            double result = 0;

            for (int i = 0; i < values.Length - 1; i++)
            {
                if (i != values.Length - 2)
                    result += ((values.Length - 1 - i) * values[i]) * (Math.Pow(value, values.Length - 2 - i));
                else
                    result += ((values.Length - 1 - i) * values[i]);
            }

            return result;
        }

        static public double CalculateRoot(double[] coefficients, double initialGuess)
        {
            for (int i = 0; i < 100; i++)
            {
                double result = initialGuess;

                initialGuess -= (PolynomialClass.Calculate(coefficients, initialGuess) / PolynomialClass.CalculateDerivative(coefficients, initialGuess));

                double approxError = ((initialGuess - result) / initialGuess) * 100;

                if (approxError == 0)
                    return result;
            }

            throw new Exception("Root not found");
        }
    }
}
