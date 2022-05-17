using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using TelegramBotLinksStorageCli.Handler;

namespace TelegramBotLinksStorageCli.Chat
{
    internal class TelegramChat : IChat
    {
        private TelegramBotClient _client;
        public TelegramChat(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));
            _client = new TelegramBotClient(token);
        }

        public void Start(CommandHandler handler)
        {
            var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = { }
            };

            _client.StartReceiving(
                handler.UpdateHandler,
                handler.ErrorHandler,
                receiverOptions,
                cts.Token);

            Console.WriteLine("Бот запущен");
            Console.WriteLine($"{_client.GetMeAsync().Result.Username}");
            Console.ReadLine();
            cts.Cancel();
        }

        public void Stop()
        {
            _client.CloseAsync();
        }


    }
}
