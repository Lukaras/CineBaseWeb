using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class Person
    {
	
	public int Id { get; set; }

	public string Name { get; set; }

	public DateTime BirthDate { get; set; }

	public string BirthPlace { get; set; }

	public DateTime? DeathDate { get; set; }

	public string Bio { get; set; }

	public PersonType Type { get; set; }
    }
}
