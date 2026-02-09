using Format.utils;
using System.CommandLine;

namespace Format.setting;

internal class CreateCommand : Command
{
    private readonly Option<string> keyOption;      // --key, -k [required]
    private readonly Option<string> valueOption;    // --value, -c [required]

    public CreateCommand() : base("add", "crea una nuova impostazione")
    {
        Options.Add(keyOption = new Option<string>("--key", "-k")
        {
            Description = "la chiave della nuova impostazione",
            Required = true
        });

        Options.Add(valueOption = new Option<string>("--value", "-c")
        {
            Description = "il valore della nuova impostazione",
            Required = true
        });

        this.SetAction(CommandCreateHandler);
    }

    private async Task CommandCreateHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {
        var key = parseResult.GetRequiredValue(keyOption);
        var value = parseResult.GetRequiredValue(valueOption);
        if(!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
        {
            if(Settings.Add(key, value))
            {
                MyConsole.WriteLine($"ho aggiunto l'impostazione `{key}` con valore `{value}`");
            }
            else
            {
                MyConsole.WriteLine($"non sono riuscito a aggiungere l'impostazione `{key}` con valore `{value}`", ConsoleColor.Red);
            }
        }
        else
        {
            MyConsole.WriteLine("key o value è vuoto", ConsoleColor.Red);
            MyConsole.WriteDebugLine($"key => {key}\nValue => {value}");
        }
    }
}