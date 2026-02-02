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
            this.Subcommands.Add(new CreateCommand());
            this.Subcommands.Add(new ListCommand());
            this.Subcommands.Add(new RemoveCommand());
        }
    }
}
