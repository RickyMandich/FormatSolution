using Format.utils;
using System.CommandLine;

namespace Format.spell
{
    internal class CreateCommand : Command
    {
        private readonly Option<string> nameOption;         // --name, -n [required]
        private readonly Option<int> levelOption;           // --level, -l
        private readonly Option<string> schoolOption;       // --school, -s [required]
        private readonly Option<string> castingTimeOption;  // --casting-time, -c
        private readonly Option<string> rangeOption;        // --range, -r
        private readonly Option<string> componentsOption;   // --components, -m [required]
        private readonly Option<string> durationOption;     // --duration, -d
        private readonly Option<string> descriptionOption;  // --description, -e [required]
        private readonly Option<string> higherLevelOption;  // --higher-level, -h
        private readonly Option<string[]> classesOption;    // --classes, -a
        private readonly Option<string> sourceOption;       // --source, -o

        public CreateCommand() : base("add", "crea un nuovo incantesimo")
        {
            this.Options.Add(nameOption = new Option<string>("--name", "-n")
                {
                    Description = "nome dell'incantesimo"
                }
            );

            this.Options.Add(levelOption = new Option<int>("--level", "-l")
                {
                    Description = "livello dell'incantesimo",
                    DefaultValueFactory = a => 0
                }
            );

            this.Options.Add(schoolOption = new Option<string>("--school", "-s")
                {
                    Description = "scuola dell'incantesimo"
                }
            );

            this.Options.Add(castingTimeOption = new Option<string>("--casting-time", "-c")
                {
                    Description = "tempo di lancio dell'incantesimo",
                    DefaultValueFactory = a => "1 azione"
                }
            );

            
            this.Options.Add(rangeOption = new Option<string>("--range", "-r")
                {
                    Description = "gittata dell'incantesimo",
                    DefaultValueFactory = a => "Se stesso"
                }
            );

            this.Options.Add(componentsOption = new Option<string>("--components", "-m")
                {
                    Description = "componenti dell'incantesimo"
            }
            );

            this.Options.Add(durationOption = new Option<string>("--duration", "-d")
                {
                    Description = "durata dell'incantesimo",
                    DefaultValueFactory = a => "Istantaneo"
                }
            );

            this.Options.Add(descriptionOption = new Option<string>("--description", "-e")
                {
                    Description = "descrizione dell'incantesimo"
                }
            );

            this.Options.Add(higherLevelOption = new Option<string>("--higher-level", "-h")
                {
                    Description = "effetto a livelli superiori",
                    DefaultValueFactory = a => ""
                }
            );

            this.Options.Add(classesOption = new Option<string[]>("--classes", "-a")
                {
                    Description = "classi che possono usare l'incantesimo",
                    DefaultValueFactory = a => new string[] {  },
                    AllowMultipleArgumentsPerToken = true
            }
            );

            this.Options.Add(sourceOption = new Option<string>("--source", "-o")
                {
                    Description = "fonte dell'incantesimo",
                    DefaultValueFactory = a => "homebrew"
                }
            );

            this.Validators.Add(result =>
            {
                var name = result.GetValue(nameOption);
                var school = result.GetValue(schoolOption);
                var components = result.GetValue(componentsOption);
                var description = result.GetValue(descriptionOption);

                // Se TUTTE sono piene, passa (modalità opzioni)
                bool allFilled = !string.IsNullOrEmpty(name) &&
                                !string.IsNullOrEmpty(school) &&
                                !string.IsNullOrEmpty(components) &&
                                !string.IsNullOrEmpty(description);

                if (allFilled)
                {
                    return; // OK
                }
            });

            this.SetAction(CommandCreateHandler);
        }

        private async Task CommandCreateHandler(ParseResult parseResult, CancellationToken cancellationToken)
        {
            var name = parseResult.GetValue(nameOption) ?? "";
            var level = parseResult.GetValue(levelOption);
            var school = parseResult.GetValue(schoolOption) ?? "";
            var castingTime = parseResult.GetValue(castingTimeOption) ?? "";
            var range = parseResult.GetValue(rangeOption) ?? "";
            var components = parseResult.GetValue(componentsOption) ?? "";
            var duration = parseResult.GetValue(durationOption) ?? "";
            var description = parseResult.GetValue(descriptionOption) ?? "";
            var higherLevels = parseResult.GetValue(higherLevelOption) ?? "";
            var classes = parseResult.GetValue(classesOption) ?? [];
            var source = parseResult.GetValue(sourceOption) ?? "";

            // Se TUTTE le opzioni obbligatorie sono piene, passa (modalità opzioni)
            bool allRequiredFilled = !string.IsNullOrEmpty(name) &&
                            !string.IsNullOrEmpty(school) &&
                            !string.IsNullOrEmpty(components) &&
                            !string.IsNullOrEmpty(description);

            SpellClass spell;

            if (!allRequiredFilled)
            {
                MyConsole.WriteLine("Opzioni insufficienti, entro in modalità interattiva", ConsoleColor.Yellow);
                SpellClass.spells.Add(spell = new SpellClass(""));
            }
            else
            {
                SpellClass.spells.Add(spell = new SpellClass(
                    name,
                    level,
                    school,
                    castingTime,
                    range,
                    components,
                    duration,
                    description,
                    higherLevels,
                    classes,
                    source
                ));
            }

            MyConsole.WriteLine($"Incantesimo creato:\n{spell.ToMarkdown()}", ConsoleColor.Green);
        }
    }
}