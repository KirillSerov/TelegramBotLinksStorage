using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotLinksStorageCli.Links;

namespace TelegramBotLinksStorageCli.Storage
{
    //TODO: Сделать нормальное сохранение
    internal class MemoryStorage : IStorage
    {
        private Dictionary<long, List<Link>> _links;

        public MemoryStorage()
        {
            _links = new Dictionary<long, List<Link>>();
        }
        public IEnumerable<Link> GetLinks(long chatId, string category)
        {
            if (!_links.ContainsKey(chatId))
                return null;
            return _links[chatId].Where(l => l.Category.Equals(category));
        }

        public IEnumerable<Link> GetAllLink(long chatId)
        {
            if (!_links.ContainsKey(chatId))
                return null;
            return _links[chatId];
        }

        public void StoreLink(long chatId, Link link)
        {
            if (!_links.ContainsKey(chatId))
                _links.Add(chatId, new List<Link>());
            _links[chatId].Add(link);
        }
    }
}
