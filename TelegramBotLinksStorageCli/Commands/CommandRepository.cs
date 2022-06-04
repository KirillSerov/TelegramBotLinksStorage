using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotLinksStorageCli.Commands
{
    /// <summary>
    /// Этот класс хранит информацию о вызванных командах.
    /// </summary>
    internal class CommandRepository
    {
        private Dictionary<long, ICommand> _commands;
        public CommandRepository()
        {
            if (_commands == null)
                _commands = new Dictionary<long, ICommand>();
        }
        /// <summary>
        /// Этот метод возвращает информацию о том, есть ли у данного чата незаконченная еоманда.
        /// </summary>
        /// <param name="chatId">id - чата</param>
        /// <returns>true  - есть незаконченная команда; false - нет незаконченной команды.</returns>
        public bool HasPendingCommand(long chatId)
        {
            if (!_commands.Keys.Contains(chatId))
                return false;
            return true;
        }
        public ICommand GetCommand(long chatId)
        {
            if (!_commands.ContainsKey(chatId))
                return null;
            return _commands[chatId];
        }
        public void AddCommand(long chatId, ICommand command)
        {
            if (_commands.ContainsKey(chatId))
            {
                _commands.Remove(chatId);
            }
                _commands.Add(chatId, command);
        }
        public void DeleteCommand(long chatId)
        {
            _commands.Remove(chatId);
        }
    }
}
