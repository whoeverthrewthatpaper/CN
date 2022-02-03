namespace NumericalIntegration
{
    internal class RPNCalculatorClass
    {
        public static string InfixToPostfix(string operation, double x)
        {
            string postfixOutput = "";
            Stack<string> Operators = new Stack<string>(10);
            foreach (var item in operation.Split(' '))
            {
                if (double.TryParse(item, out double op))
                {
                    if (postfixOutput == "")
                    {
                        postfixOutput = item;
                    }
                    else
                    {
                        postfixOutput += item + " ";
                    }
                }
                else if (item.ToLower() == "x")
                {
                    postfixOutput += x + " ";
                }
                else if (item == "(")
                {
                    Operators.Push(item);
                }
                else if (item == ")")
                {
                    while (Operators.Peek() != "(")
                    {
                        postfixOutput += Operators.Pop() + " ";
                    }
                    Operators.Pop();
                }
                else
                {
                    while (!Operators.IsEmpty()
                        && (Precedence(Operators.Peek()) > Precedence(item)
                        || Precedence(Operators.Peek()) == Precedence(item)
                        && Associativity(item) == "left"))
                    {
                        postfixOutput += Operators.Pop() + " ";
                    }

                    Operators.Push(item);
                }
            }
            while (!Operators.IsEmpty())
            {
                postfixOutput += Operators.Pop() + " ";
            }
            postfixOutput = postfixOutput.TrimEnd(' ');
            return postfixOutput;
        }
        public static double PostfixCalculator(string operationInput)
        {
            Stack<double> numStack = new Stack<double>(10);
            foreach (var item in operationInput.Split(' '))
            {
                if (double.TryParse(item, out double operand))
                {
                    numStack.Push(operand);
                }
                else
                {
                    double op2 = numStack.Pop();
                    double op1 = numStack.Pop();
                    double output = Calculate(op1, op2, item);
                    numStack.Push(output);
                }
            }
            return numStack.Pop();
        }
        public static double Calculate(double op1, double op2, string oper)
        {
            if (oper == "+")
            {
                return op1 + op2;
            }
            else if (oper == "-")
            {
                return op1 - op2;
            }
            else if (oper == "*")
            {
                return op1 * op2;
            }
            else if (oper == "/")
            {
                return op1 / op2;
            }
            else if (oper == "^")
            {
                return Math.Pow(op1, op2);
            }
            else
            {
                return 0;
            }
        }
        public static string Associativity(string op)
        {
            if (op == "^")
            {
                return "right";
            }
            else
            {
                return "left";
            }
        }
        public static int Precedence(string op)
        {
            if (op == "^")
            {
                return 4;
            }
            else if (op == "/" || op == "*")
            {
                return 3;
            }
            else if (op == "+" || op == "-")
            {
                return 2;
            }
            return -1;
        }
    }
}
