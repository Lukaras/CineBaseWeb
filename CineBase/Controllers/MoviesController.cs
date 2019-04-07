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
            string query = string.Format("SELECT [Id], [Title], [OriginalTitle], [Genre] FROM [Movie]");
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                MovieViewModel item = new MovieViewModel
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    OriginalTitle = reader.GetString(2),
                    Genre = reader.GetInt32(3),
                };
                list.Add(item);
            }
            foreach (MovieViewModel item in list)
            {
                query = string.Format("SELECT [Content] FROM [List] WHERE [Id] = {0}", item.Genre);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                reader.Read();
                item.GenreText = reader.GetString(0);
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
            string query = string.Format("SELECT [Id], [Title], [OriginalTitle], [Description], [Genre] FROM [Movie] WHERE [Id] = {0}", id);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int genre = reader.GetInt32(4);
            MovieViewModel item = new MovieViewModel
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                OriginalTitle = reader.GetString(2),
                Description = reader.GetString(3),
            };
            query = string.Format("SELECT [Content] FROM [List] WHERE [Id] = {0}", genre);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            reader.Read();
            item.GenreText = reader.GetString(0);
            List<Person> people = new List<Person>();
            query = string.Format("SELECT [Id], [Firstname], [Lastname] FROM [Person]");
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Person entity = new Person
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1) + " " + reader.GetString(2)
                };
                people.Add(entity);
            }
            item.People = people;
            List<int> overhead = new List<int>();
            query = string.Format("SELECT [PersonId] FROM [Creators] WHERE [MovieId] = {0} AND [Type] = {1}", id, (int)PersonType.Overhead);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                overhead.Add(reader.GetInt32(0));
            }
            item.OverheadPeople = overhead;
            List<int> model = new List<int>();
            query = string.Format("SELECT [PersonId] FROM [Creators] WHERE [MovieId] = {0} AND [Type] = {1}", id, (int)PersonType.Model);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                model.Add(reader.GetInt32(0));
            }
            item.ModelPeople = overhead;
            List<int> screenplay = new List<int>();
            query = string.Format("SELECT [PersonId] FROM [Creators] WHERE [MovieId] = {0} AND [Type] = {1}", id, (int)PersonType.Scenarist);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                screenplay.Add(reader.GetInt32(0));
            }
            item.ScreenplayPeople = screenplay;
            List<int> camera = new List<int>();
            query = string.Format("SELECT [PersonId] FROM [Creators] WHERE [MovieId] = {0} AND [Type] = {1}", id, (int)PersonType.Cameraman);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                camera.Add(reader.GetInt32(0));
            }
            List<int> sound = new List<int>();
            query = string.Format("SELECT [PersonId] FROM [Creators] WHERE [MovieId] = {0} AND [Type] = {1}", id, (int)PersonType.Sound);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                sound.Add(reader.GetInt32(0));
            }
            List<int> actors = new List<int>();
            query = string.Format("SELECT [PersonId] FROM [Creators] WHERE [MovieId] = {0} AND [Type] = {1}", id, (int)PersonType.Actor);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                actors.Add(reader.GetInt32(0));
            }
            item.CameraPeople = camera;
            item.SoundPeople = sound;
            item.Actors = actors;
            List<Comment> comments = new List<Comment>();
            query = string.Format("SELECT [UserId], [Comment] FROM [Comment] WHERE [MovieId] = {0}", id);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string userQuery = string.Format("SELECT [Username] FROM [User] WHERE [Id] = {0}", reader.GetInt32(0));
                SqlCommand newCmd = new SqlCommand(query, Database.db);                
                comments.Add(new Comment
                {
                    UserId = reader.GetInt32(0),
                    Content = reader.GetString(1)
                });
            }
            for (int i = 0; i < comments.Count; i++)
            {
                int uId = comments[i].UserId;
                query = string.Format("SELECT [Username] FROM [User] WHERE [Id] = {0}", uId);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                reader.Read();
                comments[i].UserName = reader.GetString(0);
            }
            item.Comments = comments;
            List<int> ratings = new List<int>();
            query = string.Format("SELECT [Rating] FROM [Rating] WHERE [MovieId] = {0}", id);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ratings.Add(reader.GetInt32(0));
            }
            item.Rating = (float)ratings.Average();
            return View(item);
        }

        public void _Add(MovieViewModel model)
        {
            int id = Database.GetLast("[Movie]") + 1;
            foreach (int i in model.OverheadPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, (int)PersonType.Overhead));
            foreach (int i in model.ModelPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, (int)PersonType.Model));
            foreach (int i in model.ScreenplayPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, (int)PersonType.Scenarist));
            foreach (int i in model.CameraPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, (int)PersonType.Cameraman));
            foreach (int i in model.SoundPeople)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, (int)PersonType.Sound));
            foreach (int i in model.Actors)
                Database.Add("[Creators]", "[Id], [MovieId], [PersonId], [Type]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Creators") + 1, id, i, (int)PersonType.Actor));

            Database.Add("[Movie]", "[Id], [Title], [OriginalTitle], [Description], [Genre]", string.Format("{0}, '{1}', '{2}', '{3}', {4}", id, model.Title, model.OriginalTitle, model.Description, model.Genre));
        }

        public void _Rate(int movieId, int rating)
        {
            string query = string.Format("SELECT [Id] FROM [Rating] WHERE [MovieId] = {0} AND [UserId] = {1}", movieId, Request.Cookies["userId"]);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {                
                int id = reader.GetInt32(0);
                Database.Update("[Rating]", string.Format("[Rating] = {0}", rating), string.Format("[Id] = {0}", id));
                return;
            }
            var userId = Request.Cookies["userId"];
            Database.Add("[Rating]", "[Id], [MovieId], [UserId], [Rating]", string.Format("{0}, {1}, {2}, {3}", Database.GetLast("Rating") + 1, movieId, Request.Cookies["userId"], rating));
        }


        public void _Comment(int movieId, string comment)
        {
            Database.Add("[Comment]", "[Id], [UserId], [MovieId], [Comment]", string.Format("{0}, {1}, {2}, '{3}'", Database.GetLast("Comment") + 1, Request.Cookies["userId"], movieId, comment));
        }
    }
}