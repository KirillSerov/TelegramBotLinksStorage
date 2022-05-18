using TelegramBotLinksStorageCli.Chat;
using TelegramBotLinksStorageCli.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotLinksStorageCli.Commands;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotLinksStorageCli.Handler
{
    internal class CommandHandler
    {
        private string[] _categories = new string[] { "спорт", "новости", "соцсети" };
        private CommandFactory _factory;
        private CommandRepository _repository;                                          // Здесь хранятся незаконченные команды.
        private IStorage _storage;
        public CommandHandler(IStorage storage, CommandRepository repository)
        {
            _factory = new CommandFactory(storage, repository, _categories);
            _repository = repository;
            _storage = storage;
        }
        public async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message != null && message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                Console.WriteLine($"{DateTime.Now:dd.MM.yyyyг. HH:mm:ss:}");
                Console.Write($"{message.From.Username}:");
                Console.WriteLine($"{message.Text}");

                // '/' - проверка на то что пользователь ввел команду, а не просто текст.
                // Если это команда и она есть в репозитории, то продолжаем её выполнение.
                // иначе создаеим новую команду.
                if (!message.Text.StartsWith('/') && _repository.HasPendingCommand(message.Chat.Id))
                {
                    await CommandFromRepository(client, message);
                }
                else
                {
                    if (_repository.HasPendingCommand(message.Chat.Id))
                        _repository.DeleteCommand(message.Chat.Id);
                    await NewCommand(client, update);
                }
            }
        }
        public async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine($"Error: {exception.Message}");
            if (exception is ApiRequestException ex)
            {
                await client.SendTextMessageAsync(123, ex.ToString());
            }
        }

        private async Task NewCommand(ITelegramBotClient client, Update update)
        {
            ICommand command = _factory.GetCommand(update.Message.Text, update.Message.Chat.Id);
            string result = command?.Execute() ?? "";
            if (!string.IsNullOrWhiteSpace(result))
            {
                if (result.Equals("Выберите категорию:"))
                {
                    // savedCategories - это уже имеющиеся в хранилище категории.
                    // allCategories - это объединение базовых категорий с теми, что добавил пользователь.
                    string[] allCategories;
                    var savedCategories = _storage.GetAllLink(update.Message.Chat.Id)?.Select(link => link.Category);
                    if (savedCategories == null)
                    {
                        allCategories = _categories.ToArray();
                    }
                    else
                    {
                        allCategories = _categories.Union(savedCategories).ToArray();
                    }
                    ReplyKeyboardMarkup buttons = allCategories;
                    // Если поступила команда /get_links, то добавим вариант "Все".
                    if (command is GetLinksCommand)
                    {
                        List<string> tmp = new List<string>(allCategories);
                        tmp.Add("Все");
                        buttons = tmp.ToArray();
                    }
                    buttons.ResizeKeyboard = true;
                    buttons.OneTimeKeyboard = true;
                    await client.SendTextMessageAsync(chatId: update.Message.Chat.Id, text: result, replyMarkup: buttons);
                }
                else
                {
                    await client.SendTextMessageAsync(update.Message.Chat.Id, result);
                }
            }
        }
        private async Task CommandFromRepository(ITelegramBotClient client, Message message)
        {
            string result = _repository.GetCommand(message.Chat.Id).Execute(message.Text);
            if (!string.IsNullOrWhiteSpace(result))
                await client.SendTextMessageAsync(message.Chat.Id, result);
            else
                await client.SendTextMessageAsync(message.Chat.Id, "Пусто");
        }
    }
}
