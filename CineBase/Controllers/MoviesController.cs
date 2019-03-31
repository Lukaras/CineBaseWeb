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
            List<Person> people = new List<Person>();
            string query = string.Format("SELECT [Id], [Firstname], [Lastname] FROM [Person]");
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Person item = new Person
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1) + " " + reader.GetString(2)
                };
                people.Add(item);
            }
            List<ListItem> list = new List<ListItem>();
            query = string.Format("SELECT [Id], [Content] FROM [List] WHERE [ListType] = {0}", (int)ListType.Žánr);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
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
                People = people,
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
            int id = Database.GetLast("[Movie]") + 1;
            foreach (int i in model.OverheadPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, PersonType.Overhead));
            foreach (int i in model.ModelPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, PersonType.Model));
            foreach (int i in model.ScreenplayPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, PersonType.Scenarist));
            foreach (int i in model.CameraPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, PersonType.Cameraman));
            foreach (int i in model.SoundPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, PersonType.Sound));
            foreach (int i in model.Actors)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, PersonType.Actor));

            Database.Add("[Movie]", "[Id], [Title], [OriginalTitle], [Description], [Genre]", string.Format("{0}, '{1}', '{2}', '{3}', {4}", id, model.Title, model.OriginalTitle, model.Description, model.Genre));
        }

        public void _Rate(int movieId, int rating)
        {
            string query = string.Format("SELECT [Id] FROM [Rating] WHERE [MovieId] = {0} AND [UserId] = {1}", movieId, 1);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {                
                int id = reader.GetInt32(0);
                Database.Update("[Rating]", string.Format("[Rating] = {0}", rating), string.Format("[Id] = {0}", id));
                return;
            }
            Database.Add("[Rating]", "[Id], [MovieId], [UserId], [Rating]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Rating") + 1, movieId, 1, rating));
        }


        public void _Comment(int movieId, string comment)
        {
            Database.Add("[Comment]", "[Id], [UserId], [MovieId], [Comment]", string.Format("{0}, {1}, {2}, '{3}'", Database.GetLast("Comment") + 1, 1, movieId, comment));
        }
    }
}