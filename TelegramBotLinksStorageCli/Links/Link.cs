using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotLinksStorageCli.Links
{
    internal class Link
    {
        public Link(string linkName, string category)
        {
            Name = linkName;
            Category = category;
        }
        public string Name { get; }
        public string Category { get; }
       
    }
}
