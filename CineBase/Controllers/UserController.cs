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

            string query = string.Format("SELECT [Id], [Username], [Password], [PasswordSalt], [Type] FROM [User] WHERE [Username] = '{0}'", login);
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
            int type = reader.GetInt32(4);

            if (entity == null) return "Uživatel neexistuje.";

            var encrypted = Helper.Hashing(password + salt);
            if (entity.Password == encrypted)
            {

                var hash = Helper.Hashing(entity.Username);
                Response.Cookies.Append("userId", entity.Id.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddHours(2) });
                Response.Cookies.Append("userType", type.ToString(), new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddHours(2) });
                Response.Cookies.Append("userName", entity.Username, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddHours(2) });
                Response.Cookies.Append("hash", hash, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddHours(2) });
                return "ok";
            }
            return "Přihlašovací údaje nejsou správné.";
        }

        public void Logout()
        {
            Response.Cookies.Delete("userId");
            Response.Cookies.Delete("userType");
            Response.Cookies.Delete("userName");
            Response.Cookies.Delete("hash");
        }
    }
}