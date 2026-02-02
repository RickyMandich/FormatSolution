using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format
{
    internal class RootCommand : System.CommandLine.RootCommand
    {
        public RootCommand() : base("a command for handle and formatting to obsidian markdown files for dnd 5e notes")
        {
            this.Subcommands.Add(new spell.SpellCommand());
        }
    }
}