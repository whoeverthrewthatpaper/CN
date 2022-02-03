namespace NumericalIntegration
{

    internal class Stack<T>
    {
        private readonly T[] Segment;
        private int SegmentNumbers;
        private readonly int Limit;
        public Stack(int size)
        {
            Segment = new T[size];
            SegmentNumbers = 0;
            Limit = size;
        }
        public void Push(T Unit)
        {
            if (SegmentNumbers == Limit - 1)
            {
                Console.WriteLine("Stack overflow");
            }
            else
            {
                Segment[SegmentNumbers] = Unit;
                SegmentNumbers++;
            }
        }
        public T Pop()
        {
            SegmentNumbers--;
            return Segment[SegmentNumbers];
        }
        public bool IsEmpty()
        {
            return SegmentNumbers == 0;
        }
        public T Peek()
        {
            return Segment[SegmentNumbers - 1];
        }
    }
}
