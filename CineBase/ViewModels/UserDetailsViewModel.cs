using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime Created { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public List<Rating> Ratings { get; set; }

        public int Type { get; set; }

        public string TypeText { get; set; }

        public int Comments { get; set; }
    }
}
