﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class Coment
    {	
	
	public int Id{ get; set; }

	[ForeignKey("User")]
	public int UserId { get; set; }

	[ForeignKey("Movie")]
	public int MovieId { get; set; }

	public string Content { get; set; }
    }
}
