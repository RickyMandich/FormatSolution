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
        }
    }
}