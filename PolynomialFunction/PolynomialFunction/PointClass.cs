namespace PolynomialFunction
{
    internal class PointClass
    {
        public int X { get; }
        public int Y { get; }

        public PointClass(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public PointClass(string input)
        {
            input = input.TrimEnd();
            input = input.TrimStart();

            while (input.Contains("  ")) input = input.Replace("  ", " ");

            int[] values = input.Split(' ').Select(Int32.Parse).ToArray();

            if (values.Length != 2) throw new Exception("Only 2 values could be provided");

            this.X = values[0];
            this.Y = values[1];
        }
    }
}
