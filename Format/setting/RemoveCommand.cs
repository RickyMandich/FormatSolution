using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;

namespace Format.setting;

internal class RemoveCommand : Command
{
    private readonly Option<int> indexOption;
    public RemoveCommand() : base("remove", "rimuovi l'impostazione all'indice indicato")
    {
        

        SetAction(CommandHandler);
    }

    private async Task CommandHandler(ParseResult parseResult, CancellationToken cancellationToken)
    {
        
    }
}
