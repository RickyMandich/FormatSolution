using Format.utils;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format
{
    internal class RootCommand : System.CommandLine.RootCommand
    {
        private readonly Option<bool> debugOption;
        public RootCommand() : base("a command for handle and formatting to obsidian markdown files for dnd 5e notes")
        {
            Subcommands.Add(new spell.SpellCommand());
            Options.Add(debugOption = new Option<bool>("--debug", "-d")
                {
                    Description = "indica se eseguire in debug mode",
                    DefaultValueFactory = a => false
                }
            );

            SetAction(CommandHandler);
        }

        private async Task CommandHandler(ParseResult parseResult, CancellationToken cancellationToken)
        {
            var debug = parseResult.GetValue(debugOption);
            // Non salviamo più permanentemente nel config, usiamo l'override rilevato in Main
            Settings.DebugOverride = debug;
            MyConsole.WriteDebugLine("sono nell'handler di rootCommand", ConsoleColor.Yellow);
            MyConsole.WriteLine(debug ? "Debug Mode: ON" : "Debug Mode: OFF", ConsoleColor.Blue);
        }
    }
}