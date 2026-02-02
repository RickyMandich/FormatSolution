using System.CommandLine;
using System.CommandLine.Parsing;

namespace Format;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        utils.Settings.Initialize();
        utils.Settings.AddIfMissing("storage", "C:\\Users\\RickyMandich\\PROJECT\\FormatSolution\\Format\\spell.json");
        Console.WriteLine($"commandline args:\t{string.Join(" ", args)}");
        spell.SpellClass.spells = new();

        var root = new RootCommand();
        var result = root.Parse(args);

        return await result.InvokeAsync();
    }
}