using Format.utils;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.spell;

internal class ListCommand : Command
{
    private readonly Option<bool> onlyNameOption;   // --name, -n
    private readonly Option<int?> indexOption;   // --index, -i
    public ListCommand() : base("list", "mostra la lista degli incantesimi")
    {
        this.Options.Add(onlyNameOption = new Option<bool>("--name", "-n")
            {
                Description = "mostra solo i nomi degli incantesimi",
                DefaultValueFactory = a => false
            }
        );

        this.Options.Add(indexOption = new Option<int?>("--index", "-i")
            {
                Description = "mostra l'incantesimo all'indice specificato"
            }
        );

        this.SetAction(CommandListHandler);
    }

    private async Task CommandListHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {
        MyConsole.WriteDebugLine("eseguo list");
        if (parseResult.GetValue(indexOption) is int)
        {
            int index = parseResult.GetValue(indexOption)!.Value;
            if (index < 0 || index >= SpellClass.spells.Count)
            {
                Console.WriteLine($"Indice {index} fuori dai limiti (0 - {SpellClass.spells.Count - 1})");
                return;
            }
            var spell = SpellClass.spells[index];
            if (parseResult.GetValue(onlyNameOption))
            {
                Console.WriteLine(spell.Name);
            }
            else
            {
                Console.WriteLine(spell.ToMarkdown());
            }
            return;
        }
        if(parseResult.GetValue(onlyNameOption))
        {
            foreach(var (index, spell) in SpellClass.spells.Index())
            {
                Console.WriteLine($"{index})\t{spell.Name}");
            }
            return;
        }
        StringBuilder sb = new StringBuilder();
        foreach(var spell in SpellClass.spells)
        {
            sb.AppendLine(spell.ToMarkdown());
        }
        Console.WriteLine(sb.ToString());
    }
}
