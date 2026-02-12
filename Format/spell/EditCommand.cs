using Format.utils;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.spell;

internal class EditCommand : Command
{
    private readonly Option<int> indexOption;
    private readonly Option<string?> nameOption;         // --name, -n
    private readonly Option<int?> levelOption;           // --level, -l
    private readonly Option<string?> schoolOption;       // --school, -s
    private readonly Option<string?> castingTimeOption;  // --casting-time, -t
    private readonly Option<string?> rangeOption;        // --range, -r
    private readonly Option<string?> componentsOption;   // --components, -c
    private readonly Option<string?> durationOption;     // --duration, -d
    private readonly Option<string?> descriptionOption;  // --description, -e
    private readonly Option<string?> higherLevelOption;  // --higher-level, -h
    private readonly Option<string[]?> classesOption;    // --classes, -a
    private readonly Option<string?> sourceOption;       // --source, -o
    public EditCommand() : base ("edit", "modifica un incantesimo")
    {
        Options.Add(indexOption = new Option<int>("--index", "-i")
        {
            Description = "indice dell'incantesimo da modificare",
            Required = true
        });

        this.Options.Add(nameOption = new ("--name", "-n")
        {
            Description = "nome dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );

        this.Options.Add(levelOption = new("--level", "-l")
        {
            Description = "livello dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );

        this.Options.Add(schoolOption = new ("--school", "-s")
        {
            Description = "scuola dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );

        this.Options.Add(castingTimeOption = new("--casting-time", "-t")
        {
            Description = "tempo di lancio dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );


        this.Options.Add(rangeOption = new("--range", "-r")
        {
            Description = "gittata dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );

        this.Options.Add(componentsOption = new("--components", "-c")
        {
            Description = "componenti dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );

        this.Options.Add(durationOption = new("--duration", "-d")
        {
            Description = "durata dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );

        this.Options.Add(descriptionOption = new("--description", "-e")
        {
            Description = "descrizione dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );

        this.Options.Add(higherLevelOption = new("--higher-level", "-h")
        {
            Description = "effetto a livelli superiori",
            DefaultValueFactory = a => null
        }
        );

        this.Options.Add(classesOption = new("--classes", "-a")
        {
            Description = "classi che possono usare l'incantesimo",
            DefaultValueFactory = a => null,
            AllowMultipleArgumentsPerToken = true
        }
        );

        this.Options.Add(sourceOption = new("--source", "-o")
        {
            Description = "fonte dell'incantesimo",
            DefaultValueFactory = a => null
        }
        );

        SetAction(CommandHandler);
    }

    private async Task CommandHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {
        var idx = parseResult.GetValue(indexOption);
        if (idx < 0 || idx >= SpellClass.spells.Count)
        {
            Console.WriteLine($"Indice {idx} non valido. La lista contiene questi {SpellClass.spells.Count} incantesimi:");
            foreach (var (index, s) in (SpellClass.spells).Index())
            {
                Console.WriteLine($"{index})\t{s.Name}");
            }
            return;
        }
        var spell = SpellClass.spells[idx];
        spell.Name = parseResult.GetValue(nameOption) ?? spell.Name;
        spell.Level = parseResult.GetValue(levelOption) ?? spell.Level;
        spell.School = parseResult.GetValue(schoolOption) ?? spell.School;
        spell.CastingTime = parseResult.GetValue(castingTimeOption) ?? spell.CastingTime;
        spell.Range = parseResult.GetValue(rangeOption) ?? spell.Range;
        spell.Components = parseResult.GetValue(componentsOption) ?? spell.Components;
        spell.Duration = parseResult.GetValue(durationOption) ?? spell.Duration;
        spell.Description = parseResult.GetValue(descriptionOption) ?? spell.Description;
        spell.HigherLevels = parseResult.GetValue(higherLevelOption) ?? spell.HigherLevels;
        spell.Classes = parseResult.GetValue(classesOption) ?? spell.Classes;
        spell.Sorgente = parseResult.GetValue(sourceOption) ?? spell.Sorgente;
        SpellClass.spells[idx] = spell;
        SpellClass.spells.Save();
        MyConsole.WriteLine($"Incantesimo '{spell.Name}' modificato con successo.", ConsoleColor.Green);
    }
}
