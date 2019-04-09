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
            Database.Add("[User]", "[Id], [Username], [Password], [PasswordSalt], [Question], [Answer], [Created], [Type]", string.Format("{0}, '{1}', '{2}', '{3}', {4}, '{5}', '{6}', {7}", Database.GetLast("User") + 1, model.Username, hashed, salt, model.Question, model.Answer, DateTime.Now.ToString(@"yyyy-MM-dd"), 0));
            return true;
        }

        public bool Login(UserViewModel model)
        {
            return true;
        }
    }
}
