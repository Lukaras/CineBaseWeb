using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CineBase.Controllers
{
    public class UserController : BaseController
    {
        UserManger manager = new UserManger();

        public ActionResult Index()
        {
            List<UserDetailsViewModel> list = new List<UserDetailsViewModel>();
            string query = string.Format("SELECT [Id], [Username], [Type] FROM [User]");
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                UserDetailsViewModel item = new UserDetailsViewModel
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Type = reader.GetInt32(2),
                };
                list.Add(item);
            }
            foreach (UserDetailsViewModel item in list)
            {
                switch (item.Type)
                {
                    default:
                        item.TypeText = "Nedefinováno";
                        break;
                    case 0:
                        item.TypeText = "Uživatel";
                        break;
                    case 1:
                        item.TypeText = "Moderátor";
                        break;
                    case 2:
                        item.TypeText = "Administrátor";
                        break;
                }
            }
            return View(list);
        }

        public ViewResult Register()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }

        public ViewResult ForgottenPassword(int? id)
        {
            return ((id.HasValue) ? View(id) : View());
        }

        public void Add(UserViewModel model)
        {
            manager.Add(model);
        }

        public string _Login(UserViewModel model)
        {

            var result = Procced(model.Username, model.Password);
            return result;
        }

        private string Procced(string login, string password)
        {

            var entity = new User();

            string query = string.Format("SELECT [Id], [Username], [Password], [PasswordSalt], [Type] FROM [User] WHERE [Username] = '{0}'", login);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                entity = new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                };
                string salt = reader.GetString(3);
                int type = reader.GetInt32(4);

                var encrypted = Helper.Hashing(password + salt);
                if (entity.Password == encrypted)
                {
                    Response.Cookies.Append("userId", entity.Id.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddHours(2) });
                    Response.Cookies.Append("userType", type.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddHours(2) });
                    Response.Cookies.Append("userName", entity.Username, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddHours(2) });
                    return "ok";
                }
                return "Přihlašovací údaje nejsou správné.";
            }
            return "Uživatel neexistuje.";
        }

        public void Logout()
        {
            Response.Cookies.Delete("userId");
            Response.Cookies.Delete("userType");
            Response.Cookies.Delete("userName");
            Response.Cookies.Delete("hash");
        }

        public ActionResult ForgottenPasswordRecovery(string username)
        {
            string query = string.Format("SELECT [Id], [Question] FROM [User] WHERE [Username] = '{0}'", username);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                var entity = new PasswordRecoveryModel
                {
                    UserId = reader.GetInt32(0),
                };
                switch (reader.GetInt32(1))
                {
                    default:
                        return View();
                    case 1:
                        entity.Question = "Můj nejoblíbenější film/ seriál?";
                        break;
                    case 2:
                        entity.Question = "Nejoblíbenější herec//herečka?";
                        break;
                    case 3:
                        entity.Question = "Rodné příjmení matky?";
                        break;
                    case 4:
                        entity.Question = "Vysněná destinace pro dovolenou?";
                        break;
                    case 5:
                        entity.Question = "Jméno prvního mazlíčka?";
                        break;
                    case 6:
                        entity.Question = "Nejoblíbenější spisovate/spisovatelka?";
                        break;

                }
                return View(entity);
            }
            return View();
        }

        public string ResetPassword(int userId, string answer)
        {
            string query = string.Format("SELECT [Answer], [PasswordSalt] FROM [User] WHERE [Id] = {0}", userId);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string salt = reader.GetString(1);
            if (answer.Equals(reader.GetString(0)))
            {
                string newPass = Helper.RandomString(8);
                Database.Update("[User]", string.Format("{0} = '{1}'", "[Password]", Helper.Hashing(newPass + salt)), $"[Id] = {userId}");
                return newPass;
            }
            return "Špatná odpověď";
        }

        public string UsernameCheck(string username)
        {
            string query = string.Format("SELECT [Id] FROM [User] WHERE [Username] = '{0}'", username);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return "Uživatelské jméno je již obsazeno!";
            }
            return "ok";
        }

        public ActionResult Detail(int id)
        {
            string query = string.Format("SELECT [Username], [Created], [Type] FROM [User] WHERE [Id] = {0}", id);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                UserDetailsViewModel entity = new UserDetailsViewModel
                {
                    Id = id,
                    Username = reader.GetString(0),
                    Created = reader.GetDateTime(1),
                    Type = reader.GetInt32(2),
                };
                switch (entity.Type)
                {
                    default:
                        break;
                    case 0:
                        entity.TypeText = "Uživatel";
                        break;
                    case 1:
                        entity.TypeText = "Moderátor";
                        break;
                    case 2:
                        entity.TypeText = "Administrátor";
                        break;
                }
                entity.Ratings = new List<Rating>();
                query = string.Format("SELECT rt.[Rating], rt.[MovieId], mo.[Title] FROM [Rating] rt, [Movie] mo WHERE mo.[Id] = rt.[MovieId] AND rt.[UserId] = {0}", id);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    entity.Ratings.Add(new Rating
                    {
                        Value = reader.GetInt32(0),
                        MovieId = reader.GetInt32(1),
                        MovieName = reader.GetString(2),
                    });
                }
                query = string.Format("SELECT [Id] FROM [Comment] WHERE [UserId] = {0}", id);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    i++;
                }
                entity.Comments = i;
                return View(entity);
            }
            return View(null);
        }

        public void ChangePass(string newPass, int uId)
        {
            string query = string.Format("SELECT [PasswordSalt] FROM [User] WHERE [Id] = {0}", uId);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string salt = reader.GetString(0);
            string enc = Helper.Hashing(newPass + salt);
            Database.Update("[User]", string.Format("[Password] = '{0}'", enc), $"[ID] = {uId}");
        }

        public void Promote(int uId)
        {
            Database.Update("[User]", string.Format("[Type] = 1"), $"[ID] = {uId}");
        }
    }
}