using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Deathdate { get; set; }

        public string Birthplace { get; set; }

        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }
    }
}
