namespace Format.utils;

internal class MyConsole
{
    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.Blue)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = previousColor;
    }

    public static void WriteDebugLine(string message, ConsoleColor color = ConsoleColor.DarkGray)
    {
        if (Settings.EnvBoolOption("debug") ?? false)
        {
            WriteLine("=======================START=====DEBUG==================");
            WriteLine(message, color);
            WriteLine("=======================END=====DEBUG==================");
        }
    }

    public static string ReadString(string prompt, ConsoleColor color = ConsoleColor.Blue)
    {
        WriteLine(prompt, color);
        var input = Console.ReadLine();
        if(input is null)
        {
            WriteLine("non inserire un valore vuoto", ConsoleColor.Red);
        }
        return input ?? ReadString(prompt, color);
    }

    public static int ReadInt(string prompt, ConsoleColor color = ConsoleColor.Blue)
    {
        WriteLine(prompt, color);
        var input = Console.ReadLine();
        if (!int.TryParse(input, out int result))
        {
            WriteLine("inserisci un intero", ConsoleColor.Red);
            return ReadInt(prompt, color);
        }
        return result;
    }
}
