using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class Movie
    {
	public int Id { get; set; }

	public string Title { get; set; }

	public string OriginalTitle { get; set; }

	public string Description { get; set; }

	public Genre Genre { get; set; }
    }
}
