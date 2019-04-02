using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Název")]
        public string Title { get; set; }

        [Display(Name = "Původní název")]
        public string OriginalTitle { get; set; }

        [Display(Name = "Popis")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<ListItem> Genres { get; set; }

        [Display(Name = "Žánr")]
        public int Genre { get; set; }

        public string GenreText { get; set; }

        public List<int> Tags { get; set; }

        public List<string> Comments { get; set; }

        public List<Person> People { get; set; }

        [Display(Name = "Režie")]
        public List<int> OverheadPeople { get; set; }

        [Display(Name = "Scénář")]
        public List<int> ScreenplayPeople { get; set; }

        [Display(Name = "Předloha")]
        public List<int> ModelPeople { get; set; }

        [Display(Name = "Kamera")]
        public List<int> CameraPeople { get; set; }

        [Display(Name = "Audio")]
        public List<int> SoundPeople { get; set; }

        [Display(Name = "Herci")]
        public List<int> Actors { get; set; }

        [Display(Name = "Hodnocení")]
        public float Rating { get; set; }


    }
}
