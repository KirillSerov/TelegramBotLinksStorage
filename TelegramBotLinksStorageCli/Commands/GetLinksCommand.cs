using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotLinksStorageCli.Links;
using TelegramBotLinksStorageCli.Storage;

namespace TelegramBotLinksStorageCli.Commands
{
    internal class GetLinksCommand : ICommand
    {
        private IStorage _storage;
        private int _step;                              // В переменную сохраняется шаг, на котором находится выполнение команды.
        CommandRepository _repository;
        private long _chatId;
        private string _category;

        public GetLinksCommand(IStorage storage, CommandRepository repository, long chatId)
        {
            _storage = storage;
            _step = 0;
            _repository = repository;
            _chatId = chatId;
        }
        public string Execute(string text)
        {
            if (_step == 0)
            {
                Console.WriteLine("Вызвана команда /get-links");
                _step = 1;
                _repository.AddCommand(_chatId, this);
                return "Выберите категорию:";
            }
            else
            {
                Console.WriteLine("Выбрана категория " + text);
                _category = text;
                _step = 0;
                _repository.DeleteCommand(_chatId);     // Если дошли до этого шага, команду можно удалить из репозитория.
                IEnumerable<Link> linksList;
                if (text.ToLower().Equals("все"))
                    linksList = _storage.GetAllLink(_chatId);
                else
                    linksList = _storage.GetLinks(_chatId, _category);
                if (linksList == null)
                    return "";
                StringBuilder links = new StringBuilder();
                foreach (var link in linksList)
                {
                    links.Append($"{link.Name}\n");
                }
                return links.ToString();
            }
        }

    }
}
