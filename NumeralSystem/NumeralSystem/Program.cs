// See https://aka.ms/new-console-template for more information
using NumeralSystem;

while (true)
{
	Console.WriteLine("Enter number:");
#pragma warning disable CS8604 // Possible null reference argument.
    Convertor.Convert(Console.ReadLine());
#pragma warning restore CS8604 // Possible null reference argument.

    Console.WriteLine(" [Esc] to quit, [Enter] to continue: ");
	if (Console.ReadKey().Key == ConsoleKey.Escape) break;
}