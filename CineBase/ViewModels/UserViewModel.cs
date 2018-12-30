using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class UserViewModel
    {
	[Display(Name="Uživatelské jméno:")]
	public string Username { get; set; }

	[Display(Name = "Heslo:")]
	[UIHint("_PasswordEditor")]
	public string Password { get; set; }

	[Display(Name = "Email:")]
	public string Email { get; set; }
    }
}
