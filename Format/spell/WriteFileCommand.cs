using Format.utils;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.spell;

internal class WriteFileCommand : Command
{
    private readonly Option<int?> indexOption;
    private readonly Option<bool> forceOverwriteOption;
    public WriteFileCommand() : base("print", "salva su file tutti gli incantesimi")
    {
        Options.Add(indexOption = new Option<int?>("--index", "-i")
        {
            Description = "l'indice dell'incantesimo da salvare su file",
            DefaultValueFactory = a => null
        });

        Options.Add(forceOverwriteOption = new Option<bool>("--force-overwrite", "-f")
        {
            Description = "ignora l'esistenza di file già esistenti per gli incantesimi da salvare",
            DefaultValueFactory = a => false
        });

        SetAction(CommandHandler);
    }

    private async Task CommandHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {
        var forceOverwrite = parseResult.GetValue(forceOverwriteOption);
        MyConsole.WriteDebugLine($"Force Overwrite:\t{forceOverwrite}");
        var idx = parseResult.GetValue(indexOption);
        if (idx is null)
        {
            foreach (var spell in SpellClass.spells)
            {
                spell.PrintToFile(forceOverwrite: forceOverwrite);
            }
            return;
        }
        if (idx < 0 || idx >= SpellClass.spells.Count)
        {
            Console.WriteLine($"Indice {idx} non valido. La lista contiene {SpellClass.spells.Count} incantesimi.");
            return;
        }
        int index = idx.Value;
        SpellClass.spells[index].PrintToFile(forceOverwrite:forceOverwrite);
    }
}
