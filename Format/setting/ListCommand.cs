using Format.spell;
using Format.utils;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.setting;

internal class ListCommand : Command
{
    private readonly Option<string> searchOption;
    public ListCommand() : base("list", "mostra la lista delle impostazioni")
    {
        Options.Add(searchOption = new("--search", "-s")
            {
                Description = "filtra le impostazioni per chiave, come per effetto della regex .*<search>.*"
            }
        );

        this.SetAction(CommandListHandler);
    }

    private async Task CommandListHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {
        if (!Settings.Initialize())
        {
            MyConsole.WriteLine("non sono riuscito a caricare le impostazioni", ConsoleColor.Red);
            return;
        }
        else
        {
            MyConsole.WriteLine($"impostazioni caricate correttamente dal file \"{Settings.path}\"", ConsoleColor.Green);
        }
        var result = Settings.List();
        if (result != null)
        {
            foreach (var (index, item) in result.Index())
            {
                if (item.Key.Contains(parseResult.GetValue(searchOption) ?? "", StringComparison.OrdinalIgnoreCase))
                {
                    MyConsole.WriteLine($"{index})\t{item.Key} => {item.Value}");
                }
            }
        }
    }
}
