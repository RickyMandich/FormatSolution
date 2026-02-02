using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.spell;

internal class RemoveCommand : Command
{
    private readonly Option<int?> indexOption;
    public RemoveCommand() : base("remove", "rimuovi l'incantesimo all'indice indicato")
    {
        Options.Add(indexOption = new Option<int?>("--i", "-i")
            {
                Description = "l'indice dell'incantesimo da rimuovere",
                DefaultValueFactory = a => null
        }
        );

        SetAction(CommandHandler);
    }

    private async Task CommandHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {
        var idx = parseResult.GetValue(indexOption);
        if(idx is null)
        {
            foreach (var (i, spell) in SpellClass.spells.Index())
            {
                Console.WriteLine($"{i})\t{spell.Name}");
            }
            return;
        }
        if(idx < 0 || idx >= SpellClass.spells.Count)
        {
            Console.WriteLine($"Indice {idx} non valido. La lista contiene {SpellClass.spells.Count} incantesimi.");
            return;
        }
        int index = idx.Value;
        SpellClass spellToRemove = SpellClass.spells[index];
        SpellClass.spells.RemoveAt(index);
        Console.WriteLine($"Incantesimo '{spellToRemove.Name}' rimosso con successo.");
    }
}
