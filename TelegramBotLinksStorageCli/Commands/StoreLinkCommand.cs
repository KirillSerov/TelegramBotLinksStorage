using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotLinksStorageCli.Links;
using TelegramBotLinksStorageCli.Storage;

namespace TelegramBotLinksStorageCli.Commands
{
    internal class StoreLinkCommand : ICommand
    {
        private IStorage _storage;
        private CommandRepository _repository;
        private string _category;
        private long _chatId;
        private int _step;                          // В переменную сохраняется шаг, на котором находится выполнение команды.
        private IEnumerable<string> _categories;
        public StoreLinkCommand(IStorage storage, CommandRepository repository, long chatId, IEnumerable<string> categories)
        {
            _storage = storage;
            _repository = repository;
            _chatId = chatId;
            _step = 0;
            _categories = categories;
        }
        public string Execute(string text)
        {
            if (_step == 0)
            {
                Console.WriteLine("Вызвана команда /store-link");
                _step = 1;
                _repository.AddCommand(_chatId, this);
                return "Выберите категорию:";
            }
            else if (_step == 1)
            {
                _step = 2;
                Console.WriteLine($"Выбрана категория {text}");
                _category = text.ToLower();
                return "Текст ссылки:";
            }
            else if(_step == 2)
            {
                _step = 0;
                Link link = new Link(text, _category);
                _storage.StoreLink(_chatId,link);
                _repository.DeleteCommand(_chatId);
                return "Ссылка сохранена";
            }
            return "";
        }
    }
}
