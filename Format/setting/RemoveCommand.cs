using Format.utils;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.setting;

internal class RemoveCommand : Command
{
    private readonly Option<int> indexOption;
    public RemoveCommand() : base("remove", "rimuovi l'impostazione all'indice indicato")
    {
        Options.Add(indexOption = new("--index", "-i")
        {
            Description = "l'indice dell'impostazione da rimuovere",
            Required = true
        });

        SetAction(CommandHandler);
    }

    private async Task CommandHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {
        var index = parseResult.GetValue(indexOption);
        if(index < 0 || index >= Settings.List().Count)
        {
            Console.WriteLine("non esiste un'impostazione a questo indice");
            foreach (var (idx, setting) in Settings.List().Index())
            {
                Console.WriteLine($"{idx}\t{setting.Key}: {setting.Value}");
            }
            return;
        }
        if (Settings.Remove(index))
        {
            MyConsole.WriteLine($"{Settings.List().ElementAt(index).Key} settings removed successfuly", ConsoleColor.Green);
        }
    }
}
