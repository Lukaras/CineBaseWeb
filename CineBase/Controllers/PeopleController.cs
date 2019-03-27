using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CineBase.Controllers
{
    public class PeopleController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }

        public void _Add(PersonViewModel model)
        {
            Database.Add("[Person]", "[Id], [Firstname], [Lastname], [Birthdate], [Deathdate], [Birthplace], [Bio]", string.Format("{0}, '{1}', '{2}', {3}, {4}, '{5}', '{6}'", Database.GetLast("Person") + 1, model.Firstname, model.Lastname, model.Birthdate, model.Deathdate, model.Birthplace, model.Bio));
        }
    }
}