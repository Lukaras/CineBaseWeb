using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class ListItems
    {
        public ListItem Addable { get; set; }

        public List<ListItem> Genres { get; set; }
        public List<ListItem> Tags { get; set; }
    }
}
