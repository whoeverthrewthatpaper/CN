// See https://aka.ms/new-console-template for more information


using RNG;

static void Main(string[] args)
{
    MenuItem[] menuElements = {
                new MenuItem("C# Built-in", ChooseBuiltIn),
                new MenuItem("LCG", ChooseLCG),
                new MenuItem("Xorshift", ChooseXorshift)
            };
    Menu consoleMenu = new Menu("Choose RNG", menuElements);
    consoleMenu.RunMenu();
}

static int[] GetRange()
{
    Console.Write("Range (minValue - maxValue, Don't forget to use spaces between '-'): ");
    string input = Console.ReadLine();
    string[] inputArr = CheckRange(input);
    if (inputArr.Length == 1)
    {
        return new int[1];
    }
    int[] range = new int[2] { int.Parse(inputArr[0].Replace(" ", "")), int.Parse(inputArr[1].Replace(" ", "")) };

    return range;
}
static string[] CheckRange(string input)
{
    if (input.Split(" - ").Length != 2 || string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Incorrect input. Examples of correct input:\n 0 - 5\n-1 - 9\n 7 - 15");
        return new string[1];
    }

    foreach (var c in input.Replace("-", "").Replace(" ", ""))
    {
        if (!char.IsDigit(c))
        {
            Console.WriteLine("Only numbers are allowed. Examples of correct input:\n0 - 5\n-1 - 9\n7 - 15");
            return new string[1];
        }
    }
    if (int.Parse(input.Split(" - ")[0].Replace(" ", "")) >= int.Parse(input.Split(" - ")[1].Replace(" ", "")))
    {
        Console.WriteLine("maxValue must be greater than minValue");
        return new string[1];
    }
    return input.Split(" - ");
}
static void ChooseBuiltIn()
{
    IRNG rng = new Built_in();
    var range = GetRange();
    if (range.Length != 1)
    {
        rng.Visualize(range[0], range[1], 100000);
    }
    Console.ReadLine();
}
static void ChooseXorshift()
{
    IRNG rng = new Xorshift();
    var range = GetRange();
    if (range.Length != 1)
    {
        rng.Visualize(range[0], range[1], 100000);
    }
    Console.ReadLine();
}
static void ChooseLCG()
{
    IRNG rng = new LCG();
    var range = GetRange();
    if (range.Length != 1)
    {
        rng.Visualize(range[0], range[1], 100000);
    }
    Console.ReadLine();
}
    
