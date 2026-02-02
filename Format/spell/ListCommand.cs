using Format.utils;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.spell;

internal class ListCommand : Command
{
    private readonly Option<bool> onlyNameOption;   // --name, -n
    public ListCommand() : base("list", "mostra la lista degli incantesimi")
    {
        this.Options.Add(onlyNameOption = new Option<bool>("--name", "-n")
            {
                Description = "mostra solo i nomi degli incantesimi",
                DefaultValueFactory = a => false
            }
        );

        this.SetAction(CommandListHandler);
    }

    private async Task CommandListHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {

        if(parseResult.GetValue(onlyNameOption))
        {
            foreach(var (index, spell) in SpellClass.spells.Index())
            {
                Console.WriteLine($"{index})\t{spell.Name}");
            }
        }
        else
        {
            StringBuilder sb = new StringBuilder();
            foreach(var spell in SpellClass.spells)
            {
                sb.AppendLine(spell.ToMarkdown());
            }
            Console.WriteLine(sb.ToString());
        }
    }
}
