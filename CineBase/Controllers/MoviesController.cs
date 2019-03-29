using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CineBase.Controllers
{
    public class MoviesController : BaseController
    {
        public ActionResult Index()
        {
            List<MovieViewModel> list = new List<MovieViewModel>();
            string query = string.Format("SELECT [Id], [Title], [OriginalTitle] FROM [Movie]");
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MovieViewModel item = new MovieViewModel
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    OriginalTitle = reader.GetString(2),
                };
                list.Add(item);
            }
            return View(list);
        }

        public ActionResult Add()
        {

            List<ListItem> list = new List<ListItem>();
            string query = string.Format("SELECT [Id], [Content] FROM [List] WHERE [ListType] = {0}", (int)ListType.Žánr);
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
            MovieViewModel model = new MovieViewModel
            {
                Genres = list,
            };
            return View(model);
        }

        public ActionResult Detail(int id)
        {
            string query = string.Format("SELECT [Id], [Title], [OriginalTitle] FROM [Movie] WHERE [Id] = {0}", id);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            MovieViewModel item = new MovieViewModel
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                OriginalTitle = reader.GetString(2),
            };
            return View(item);
        }

        public void _Add(MovieViewModel model)
        {
            Database.Add("[Movie]", "[Id], [Title], [OriginalTitle], [Description], [Genre]", string.Format("{0}, '{1}', '{2}', '{3}', {4}", Database.GetLast("[Movie]") + 1, model.Title, model.OriginalTitle, model.Description, model.Genre));
        }

        public void _Rate(int movieId, int rating)
        {
            Database.Add("[Rating]", "[Id], [MovieId], [UserId], [Rating]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Rating") + 1, movieId, 1, rating));
        }

    }
}