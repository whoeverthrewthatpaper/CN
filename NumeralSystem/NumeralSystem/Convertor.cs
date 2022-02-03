using static NumeralSystem.Parser;

namespace NumeralSystem
{
    public static class Convertor
    {
        static public void Convert(Object obj)
        {
            String decimalValue = " invalid ";
            String binaryValue = "invalid";
            String octalValue = "invalid";
            String hexValue = "invalid";

            obj = (obj as String).Replace(",", ".");

            switch (Parser.Parse(obj))
            {
                case Base.Decimal:
                    String dec = obj as string;

                    decimalValue = dec;
                    binaryValue = FromDecimal(dec, Base.Binary);
                    octalValue = FromDecimal(dec, Base.Octal);
                    hexValue = FromDecimal(dec, Base.Hex);

                    break;
                case Base.Octal:
                    String octal = obj as string;

                    decimalValue = FromOctal(octal, Base.Decimal);
                    binaryValue = FromOctal(octal, Base.Binary);
                    octalValue = octal;
                    hexValue = FromOctal(octal, Base.Hex);

                    break;
                case Base.Hex:
                    String hex = obj as string;

                    decimalValue = FromHex(hex, Base.Decimal);
                    binaryValue = FromHex(hex, Base.Binary);
                    octalValue = FromHex(hex, Base.Octal);
                    hexValue = hex;

                    break;
                default:
                    break;
            }

            Console.WriteLine("In decimal: " + decimalValue);
            Console.WriteLine("In binary: " + binaryValue);
            Console.WriteLine("In octal: " + octalValue);
            Console.WriteLine("In hex: " + hexValue);
        }


        static public String FromDecimal(String dec, Base b)
        {
            if (Parser.Parse(dec) == Base.Unknown)
                throw new InvalidArgException("Invalid");

            if (b == Base.Decimal) return dec;

            String fractionValue = "";
            if (dec.Contains("."))
            {
                String[] fraction = dec.Split('.');
                dec = fraction[0];

                fractionValue = FromDecimalFraction(fraction[1], b);
            }

            int result = int.Parse(dec);
            int divider = (int)b;
            List<int> remainders = new List<int>();

            while (result != 0)
            {
                remainders.Add((int)result % divider);
                result /= divider;
            }

            remainders.Reverse();

            String returnValue = Form.Parse(String.Join("", remainders.ToArray()), b);
            if (fractionValue != "") returnValue = $"{returnValue}.{fractionValue}";

            return returnValue;
        }

        static public String ToDecimal(String any, Base b)
        {
            if (Parser.Parse(any) == Base.Unknown)
                throw new InvalidArgException("Invalid");

            if (any.Contains("."))
                return ToDecimalFraction(any, b);

            if (b == Base.Decimal)
                return any;

            if (b == Base.Hex && any.StartsWith("0x"))
                any = any.Substring(2, any.Length - 2);
            else if (b == Base.Octal && any.StartsWith("0"))
                any = any.Substring(1, any.Length - 1);

            int baseIndex = (int)b;
            int sum = 0;

            for (int i = 0; i < any.Length; i++)
            {
                int value;
                if (!Int32.TryParse(any[i].ToString(), out value))
                    value = Form.GetHexNumber(any[i]);

                int i2 = (int)Math.Pow(baseIndex, any.Length - 1 - i);

                sum += value * i2;
            }

            return sum.ToString();
        }
        static public String ToDecimalFraction(String any, Base b)
        {
            if (Parser.Parse(any) == Base.Unknown)
                throw new InvalidArgException("Invalid");

            if (b == Base.Hex && any.StartsWith("0x"))
                any = any.Substring(2, any.Length - 2);
            else if (b == Base.Octal && any.StartsWith("0"))
                any = any.Substring(1, any.Length - 1);

            int indexOfPoint = any.IndexOf('.');
            int baseIndex = (int)b;
            double sum = 0;

            any = any.Replace(".", "");
            for (int i = 0; i < any.Length; i++)
            {
                int value;
                if (!Int32.TryParse(any[i].ToString(), out value))
                    value = Form.GetHexNumber(any[i]);

                sum += value * Math.Pow(baseIndex, indexOfPoint - 1 - i);
            }

            return sum.ToString().Replace(",", ".");
        }

        static public String FromDecimalFraction(String any, Base b)
        {
            if (Parser.Parse(any) == Base.Unknown)
                throw new InvalidArgException("Invalid");

            if (b == Base.Decimal)
                return any;

            double result = Double.Parse($"0,{any}");
            int divider = (int)b;
            List<String> integerParts = new List<String>();

            for (int i = 0; i < 10; i++)
            {
                result *= divider;
                String resultString = result.ToString();

                int indexOfPoint = resultString.IndexOf(',');
                String fractionPart = resultString.Substring(indexOfPoint + 1, resultString.Length - indexOfPoint - 1);
                String integerPart = (indexOfPoint > -1) ? resultString.Substring(0, indexOfPoint) : "0";

                result = Double.Parse($"0,{fractionPart}");

                integerParts.Add(integerPart);
            }

            return Form.Parse(String.Join("", integerParts.ToArray()), b, true);
        }

        static public String FromOctal(String octal, Base b)
        {
            if (Parser.Parse(octal) == Base.Unknown)
                throw new InvalidArgException("Invalid");

            if (b == Base.Octal) return octal;

            return FromDecimal(ToDecimal(octal, Base.Octal), b);
        }

        static public String FromHex(String hex, Base b)
        {
            if (Parser.Parse(hex) == Base.Unknown)
                throw new InvalidArgException("Invalid");

            if (b == Base.Hex) return hex;

            return FromDecimal(ToDecimal(hex, Base.Hex), b);
        }



    }
}
