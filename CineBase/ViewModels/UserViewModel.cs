using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class UserViewModel
    {
	[Required]
	[Display(Name="Uživatelské jméno:")]
	public string Username { get; set; }

	[Required]
	[Display(Name = "Heslo:")]
	[UIHint("_PasswordEditor")]
	public string Password { get; set; }

	[Required]
	[Display(Name = "Email:")]
	public string Email { get; set; }
    }
}
