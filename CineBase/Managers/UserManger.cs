using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineBase
{
    public class UserManger
    {
	public bool Add(UserViewModel model)
	{
	    string salt = Helper.RandomString(4);
	    string hashed = Helper.Hashing(model.Password + salt);
	    Database.Add("[User]", "[Id], [Username], [Password], [PasswordSalt], [Email], [Created], [Type]", string.Format("{0}, '{1}', '{2}', '{3}', '{4}', {5}, {6}", Database.GetLast("User") + 1, model.Username, hashed, salt, model.Email, "null", 2));
	    return true;
	}
    }
}
