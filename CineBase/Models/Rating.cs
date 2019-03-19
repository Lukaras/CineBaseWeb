using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class Rating
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public int UserId { get; set; }

        public int MovieId { get; set; }
    }
}
