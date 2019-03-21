using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CineBase.Controllers
{
    public class ListController : Controller
    {
        public ActionResult Index()
        {
            ListItems model = new ListItems
            {
                Addable = new ListItem(),
                Genres = Get(ListType.Žánr),
                Tags = Get(ListType.Tag)
            };
            return View(model);
        }

        public List<ListItem> Get(ListType type)
        {
            List<ListItem> list = new List<ListItem>();
            string query = string.Format("SELECT [Id], [Content] FROM [List] WHERE [ListType] = {0}", (int)type);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ListItem item = new ListItem
                {
                    Id = reader.GetInt32(0),
                    Content = reader.GetString(1)
                };
                list.Add(item);
            }
            return list;
        }

        public PartialViewResult Tags()
        {
            return PartialView();
        }

        public void _AddItem(ListItem model)
        {
            Database.Add("[List]", "[Id], [Content], [ListType]", string.Format("{0}, '{1}', {2}", Database.GetLast("List") + 1, model.Content, model.Item));
        }
    }
}