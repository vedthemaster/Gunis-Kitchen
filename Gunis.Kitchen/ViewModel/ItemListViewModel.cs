using Gunis.Kitchen.Models;
using System.Collections.Generic;

namespace Gunis.Kitchen.ViewModel
{
    public class ItemListViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public string CurrentCategory { get; set; }
    }
}
