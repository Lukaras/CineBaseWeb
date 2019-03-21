using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class ListItem
    {
        public int Id { get; set; }

        [Display(Name = "Typ")]
        [UIHint("ListTypeEditor")]
        public int Item { get; set; }

        [Display(Name = "Obsah")]
        public string Content { get; set; }
    }
}
