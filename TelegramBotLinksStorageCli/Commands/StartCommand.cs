using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotLinksStorageCli.Links;

namespace TelegramBotLinksStorageCli.Commands
{
    internal class StartCommand : ICommand
    {
        public string Execute(string text)
        {
            Console.WriteLine("Вызвана команда /start");
            return "Добро пожаловать!";
        }
       
    }
}
