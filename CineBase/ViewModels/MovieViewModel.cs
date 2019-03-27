﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string OriginalTitle { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<ListItem> Genres { get; set; }

        public int Genre { get; set; }

        public string GenreText { get; set; }

        public List<int> Tags { get; set; }

        public List<string> Comments { get; set; }

        public List<Person> People { get; set; }

        public float Rating { get; set; }


    }
}
