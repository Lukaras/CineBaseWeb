using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class PersonViewModel
    {

        public PersonViewModel()
        {
            DetailActors = new List<MovieViewModel>();
            DetailCameraMovies = new List<MovieViewModel>();
            DetailModelMovies = new List<MovieViewModel>();
            DetailOverheadMovies = new List<MovieViewModel>();
            DetailScreenplayMovies = new List<MovieViewModel>();
            DetailSoundMovies = new List<MovieViewModel>();
        }

        public int Id { get; set; }

        [Display(Name = "Křestní jméno")]
        public string Firstname { get; set; }

        [Display(Name = "Příjmení")]
        public string Lastname { get; set; }

        [Display(Name = "Datum narození")]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Datum úmrtí")]
        [DataType(DataType.Date)]
        public DateTime? Deathdate { get; set; }

        [Display(Name = "Místo narození")]
        public string Birthplace { get; set; }

        [Display(Name = "Popis")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }

        [Display(Name = "Režie")]
        public List<MovieViewModel> DetailOverheadMovies { get; set; }

        [Display(Name = "Scénář")]
        public List<MovieViewModel> DetailScreenplayMovies { get; set; }

        [Display(Name = "Předloha")]
        public List<MovieViewModel> DetailModelMovies { get; set; }

        [Display(Name = "Kamera")]
        public List<MovieViewModel> DetailCameraMovies { get; set; }

        [Display(Name = "Audio")]
        public List<MovieViewModel> DetailSoundMovies { get; set; }

        [Display(Name = "Herec/herečka")]
        public List<MovieViewModel> DetailActors { get; set; }
    }
}
