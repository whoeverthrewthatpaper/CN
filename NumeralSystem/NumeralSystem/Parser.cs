using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NumeralSystem.Parser;

namespace NumeralSystem
{
    public static class Parser
	{
		public enum Base { Decimal = 10, Binary = 2, Octal = 8, Hex = 16, Unknown };

		static public Base Parse(Object obj)
		{
			String value = obj as String;

			while (value.Contains(".")) value = value.Replace(".", ",");

			if (isOctal(value))
				return Base.Octal;
			if (isDecimal(value))
				return Base.Decimal;
			if (isHex(value))
				return Base.Hex;
			else
				return Base.Unknown;
		}

		static private bool isDecimal(String value)
		{
			double res;
			return (!value.StartsWith("0") || value.StartsWith("0,")) && Double.TryParse(value, out res);
		}

		static private bool isOctal(String value)
		{
			double res;
			return value.StartsWith("0") && !value.StartsWith("0,") && Double.TryParse(value, out res) && !value.Contains("8") && !value.Contains("9");
		}

		static private bool isHex(String value)
		{
			double res;
			if (value.StartsWith("0x"))
			{
				value = value.Substring(2, value.Length - 2);

				foreach (char c in value)
				{
					if (c == ',') continue;

					if (!Form.hexValuesDictionaty.ContainsKey(c) && !Double.TryParse(c.ToString(), out res))
						return false;
				}

				return true;
			}
			else return false;
		}
	}

	public static class Form
	{
		public static Dictionary<char, int> hexValuesDictionaty = new Dictionary<char, int>()
		{
			{'A', 10 },
			{'B', 11 },
			{'C', 12 },
			{'D', 13 },
			{'E', 14 },
			{'F', 15 },
		};

		static public String Parse(String value, Base b, bool fraction = false)
		{
			switch (b)
			{
				case Base.Octal:
					if (!value.StartsWith("0") && !fraction)
						value = $"0{value}";
					break;
				case Base.Hex:
					foreach (KeyValuePair<char, int> entry in hexValuesDictionaty)
					{
						if (value.Contains(entry.Value.ToString()))
							value = value.Replace(entry.Value.ToString(), entry.Key.ToString());
					}

					if (!value.StartsWith("0x") && !fraction)
						value = $"0x{value}";
					break;
			}
			return value;
		}

		static public int GetHexNumber(char hex)
		{
			if (hexValuesDictionaty.ContainsKey(hex))
				return hexValuesDictionaty[hex];
			else
				throw new Invalid_Hex_Exception("Hex invalid: " + hex);
		}
	}

	public class Invalid_Hex_Exception : Exception
	{
		public Invalid_Hex_Exception(string message) : base(message) { }
	}

	public class InvalidArgException : Exception
	{
		public InvalidArgException(string message) : base(message) { }
	}
}
