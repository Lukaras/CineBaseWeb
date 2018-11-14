using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase.Models
{
    public class User
    {
	public int ID { get; set; }

	public string Username { get; set; }

	public string Password { get; set; }

	public string PasswordSalt { get; set; }

	public string Email { get; set; }

	public int CommentCount { get; set; }

	public DateTime Created { get; set; }
    }
}
