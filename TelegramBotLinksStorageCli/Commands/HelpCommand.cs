using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotLinksStorageCli.Commands
{
    internal class HelpCommand : ICommand
    {
        public string Execute(string text)
        {
            Console.WriteLine("Вызвана команда /help");
            StringBuilder commands = new StringBuilder();
            commands.Append("/start - запуск бота\n");
            commands.Append("/help - помощь по командам\n");
            commands.Append("/store_link - сохранить ссылку\n");
            commands.Append("/get_links - получить ссылки\n");
            return commands.ToString();
        }
    }
}
