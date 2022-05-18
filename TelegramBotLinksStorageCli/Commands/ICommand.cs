using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotLinksStorageCli.Commands
{
    internal interface ICommand
    {
        string Execute(string text = "");
    }
}
