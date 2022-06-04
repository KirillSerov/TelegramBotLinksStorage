using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotLinksStorageCli.Links;
using TelegramBotLinksStorageCli.Storage;

namespace TelegramBotLinksStorageCli.Commands
{
    internal class CommandFactory
    {
        private IStorage _storage;
        private CommandRepository _repository;
        private IEnumerable<string> _categories;
        public CommandFactory(IStorage storage, CommandRepository repository, IEnumerable<string> categories)
        {
            _storage = storage;
            _repository = repository;
            _categories = categories;
        }
        public ICommand GetCommand(string message, long chatId)
        {
            switch (message)
            {
                case "/start": return new StartCommand();
                case "/help": return new HelpCommand();
                case "/store_link": return new StoreLinkCommand(_storage, _repository, chatId, _categories);
                case "/get_links": return new GetLinksCommand(_storage, _repository, chatId);
                default: return null;
            }

        }


    }
}
