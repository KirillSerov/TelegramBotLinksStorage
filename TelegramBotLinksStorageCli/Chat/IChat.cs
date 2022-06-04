using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotLinksStorageCli.Handler;

namespace TelegramBotLinksStorageCli.Chat
{
    internal interface IChat
    {
        void Start(CommandHandler handler);
        void Stop();
    }
}
