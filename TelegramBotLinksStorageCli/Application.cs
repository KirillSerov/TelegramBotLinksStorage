using System;
using System.Threading.Tasks;
using TelegramBotLinksStorageCli.Chat;
using TelegramBotLinksStorageCli.Commands;
using TelegramBotLinksStorageCli.Handler;
using TelegramBotLinksStorageCli.Storage;

namespace ClassesForTelegramBot
{
    internal class Application
    {
        private readonly string token = "5377516674:AAFFN5iujaOrNkEWeJDzgNyCBpN8y3ZMHto";
        public void Run()
        {
            IStorage storage = new MemoryStorage();
            CommandRepository repository = new CommandRepository();
            IChat chat = new TelegramChat(token);
            CommandHandler handler = new CommandHandler(storage, repository);

            chat.Start(handler);
        }
    }
}
