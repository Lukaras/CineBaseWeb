using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class Tag
    {
	[Index]
	public int Id { get; set; }

	public string Content { get; set; }
    }
}
