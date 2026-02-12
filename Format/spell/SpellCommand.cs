using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.spell
{
    internal class SpellCommand : Command
    {
        public SpellCommand() : base("spell", "il comando per gestire gli incantesimi")
        {
            Subcommands.Add(new CreateCommand());
            Subcommands.Add(new ListCommand());
            Subcommands.Add(new EditCommand());
            Subcommands.Add(new RemoveCommand());
            Subcommands.Add(new WriteFileCommand());
        }
    }
}
