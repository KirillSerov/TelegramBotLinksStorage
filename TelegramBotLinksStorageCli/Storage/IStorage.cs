using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBotLinksStorageCli.Links;

namespace TelegramBotLinksStorageCli.Storage
{
    internal interface IStorage
    {
        IEnumerable<Link> GetLinks(long chatId, string category);
        void StoreLink(long chatId, Link link);
        IEnumerable<Link> GetAllLink(long chatId);
    }
}
