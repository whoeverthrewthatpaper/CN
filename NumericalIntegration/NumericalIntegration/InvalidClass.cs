namespace NumericalIntegration
{
    internal class InvalidClass : Exception
    {
        public InvalidClass()
        {
        }
        public InvalidClass(string Note) : base($"Wrong equation: {Note}")
        {
        }
    }
}
