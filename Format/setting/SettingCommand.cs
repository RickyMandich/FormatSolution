using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.setting;

internal class SettingCommand : Command
{
    public SettingCommand() : base("setting", "il comando per gestire le impostazioni")
    {
        Subcommands.Add(new CreateCommand());
        Subcommands.Add(new ListCommand());
        Subcommands.Add(new RemoveCommand());
    }
}
