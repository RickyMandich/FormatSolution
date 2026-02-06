using Format.utils;
using System.IO;

namespace Format;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        // Rileviamo il debug flag immediatamente per influenzare i log di inizializzazione
        if (args.Contains("--debug") || args.Contains("-d"))
        {
            Settings.DebugOverride = true;
        }

        // Otteniamo il percorso assoluto della cartella del progetto risalendo dalla cartella bin
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string projectDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", ".."));

        Settings.BaseDir = projectDir;
        Settings.Initialize("format.config");
        Settings.AddPathOptionIfMissing("storage", Path.Combine(projectDir, "data", "spell.json"));
        Settings.AddPathOptionIfMissing("OUTPUT_DIRECTORY", Path.Combine(projectDir, "data", "incantesimi"));
        MyConsole.WriteDebugLine($"commandline args:\t{string.Join(" ", args)}");
        spell.SpellClass.spells = new();
        Settings.Set("debug", false);
        var root = new RootCommand();
        var parseResult = root.Parse(args);
        return await parseResult.InvokeAsync();

    }
}