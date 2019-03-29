using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CineBase.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult Index()
        {
            List<PersonViewModel> list = new List<PersonViewModel>();
            string query = string.Format("SELECT [Id], [Firstname], [Lastname] FROM [Person]");
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                PersonViewModel item = new PersonViewModel
                {
                    Id = reader.GetInt32(0),
                    Firstname = reader.GetString(1),
                    Lastname = reader.GetString(2),
                };
                list.Add(item);
            }
            return View(list);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
            string query = string.Format("SELECT [Id], [Firstname], [Lastname] FROM [Person] WHERE [Id] = {0}", id);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            PersonViewModel item = new PersonViewModel
            {
                Id = reader.GetInt32(0),
                Firstname = reader.GetString(1),
                Lastname = reader.GetString(2),
            };
            return View(item);
        }

        public void _Add(PersonViewModel model)
        {
            Database.Add("[Person]", "[Id], [Firstname], [Lastname], [Birthdate], [Deathdate], [Birthplace], [Bio]", string.Format("{0}, '{1}', '{2}', {3}, {4}, '{5}', '{6}'", Database.GetLast("Person") + 1, model.Firstname, model.Lastname, (model.Birthdate.HasValue) ? "'" + model.Birthdate.Value.ToString(@"yyyy-MM-dd") + "'" : "null", (model.Deathdate.HasValue) ? "'" + model.Deathdate.Value.ToString(@"yyyy-MM-dd") + "'" : "null", model.Birthplace, model.Bio));
        }
    }
}