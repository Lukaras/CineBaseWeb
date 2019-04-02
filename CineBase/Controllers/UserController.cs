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

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Register()
        {
            return View();
        }

        public ViewResult Login()
        {
            return View();
        }

        public void Add(UserViewModel model)
        {
            manager.Add(model);
        }

        public ActionResult _Login(UserViewModel model)
        {

            var result = Procced(model.Username, model.Password);
            if (result == "ok")
            {
                return RedirectToAction("Index", "Home");
            }
            else ModelState.AddModelError("Login", result);

            return View(model);
        }

        private string Procced(string login, string password)
        {

            var entity = new User();

            string query = string.Format("SELECT [Id], [Username], [Password], [PasswordSalt] FROM [User] WHERE [Username] = '{0}'", login);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            entity = new User
            {
                Id = reader.GetInt32(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2),
            };
            string salt = reader.GetString(3);

            if (entity == null) return "Uživatel neexistuje.";

            var encrypted = Helper.Hashing(password + salt);
            encrypted = encrypted.Replace('�', '?');
            if (entity.Password == encrypted)
            {

                var expire = DateTime.Now.AddMinutes(240);

                Response.Cookies.Append("user", entity.Id.ToString());
                return "ok";
            }
            return "Přihlašovací údaje nejsou správné.";
        }
    }
}