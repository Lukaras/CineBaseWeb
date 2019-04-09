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
            string query = string.Format("SELECT [Id], [Firstname], [Lastname], [Birthdate], [Birthplace], [Deathdate], [Bio] FROM [Person] WHERE [Id] = {0}", id);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            PersonViewModel item = new PersonViewModel
            {
                Id = reader.GetInt32(0),
                Firstname = reader.GetString(1),
                Lastname = reader.GetString(2),
                Birthplace = reader.GetString(4),
                Bio = reader.GetString(6),
            };
            try { item.Birthdate = reader.GetDateTime(3); }
            catch {; }
            try { item.Deathdate = reader.GetDateTime(5); }
            catch {; }
            query = string.Format("SELECT [MovieId], [Type] FROM [Creators] WHERE [PersonId] = {0}", id);
            cmd = new SqlCommand(query, Database.db);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                switch (reader.GetInt32(1))
                {
                    default:
                        break;
                    case 1:
                        item.DetailActors.Add(new MovieViewModel { Id = reader.GetInt32(0) });
                        break;
                    case 2:
                        item.DetailOverheadMovies.Add(new MovieViewModel { Id = reader.GetInt32(0) });
                        break;
                    case 3:
                        item.DetailScreenplayMovies.Add(new MovieViewModel { Id = reader.GetInt32(0) });
                        break;
                    case 4:
                        item.DetailCameraMovies.Add(new MovieViewModel { Id = reader.GetInt32(0) });
                        break;
                    case 5:
                        item.DetailSoundMovies.Add(new MovieViewModel { Id = reader.GetInt32(0) });
                        break;
                    case 6:
                        item.DetailModelMovies.Add(new MovieViewModel { Id = reader.GetInt32(0) });
                        break;
                }
            }
            foreach (MovieViewModel m in item.DetailModelMovies)
            {
                query = string.Format("SELECT [Title] FROM [Movie] WHERE [Id] = {0}", m.Id);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                reader.Read();
                m.Title = reader.GetString(0);
            }
            foreach (MovieViewModel m in item.DetailActors)
            {
                query = string.Format("SELECT [Title] FROM [Movie] WHERE [Id] = {0}", m.Id);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                reader.Read();
                m.Title = reader.GetString(0);
            }
            foreach (MovieViewModel m in item.DetailSoundMovies)
            {
                query = string.Format("SELECT [Title] FROM [Movie] WHERE [Id] = {0}", m.Id);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                reader.Read();
                m.Title = reader.GetString(0);
            }
            foreach (MovieViewModel m in item.DetailScreenplayMovies)
            {
                query = string.Format("SELECT [Title] FROM [Movie] WHERE [Id] = {0}", m.Id);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                reader.Read();
                m.Title = reader.GetString(0);
            }
            foreach (MovieViewModel m in item.DetailOverheadMovies)
            {
                query = string.Format("SELECT [Title] FROM [Movie] WHERE [Id] = {0}", m.Id);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                reader.Read();
                m.Title = reader.GetString(0);
            }
            foreach (MovieViewModel m in item.DetailCameraMovies)
            {
                query = string.Format("SELECT [Title] FROM [Movie] WHERE [Id] = {0}", m.Id);
                cmd = new SqlCommand(query, Database.db);
                reader = cmd.ExecuteReader();
                reader.Read();
                m.Title = reader.GetString(0);
            }
            return View(item);
        }

        public void _Add(PersonViewModel model)
        {
            Database.Add("[Person]", "[Id], [Firstname], [Lastname], [Birthdate], [Deathdate], [Birthplace], [Bio]", string.Format("{0}, '{1}', '{2}', {3}, {4}, '{5}', '{6}'", Database.GetLast("Person") + 1, model.Firstname, model.Lastname, (model.Birthdate.HasValue) ? "'" + model.Birthdate.Value.ToString(@"yyyy-MM-dd") + "'" : "null", (model.Deathdate.HasValue) ? "'" + model.Deathdate.Value.ToString(@"yyyy-MM-dd") + "'" : "null", model.Birthplace, model.Bio));
        }

        public ActionResult Edit(int id)
        {
            string query = string.Format("SELECT [Id], [Firstname], [Lastname], [Birthdate], [Birthplace], [Deathdate], [Bio] FROM [Person] WHERE [Id] = {0}", id);
            SqlCommand cmd = new SqlCommand(query, Database.db);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                PersonViewModel item = new PersonViewModel
                {
                    Id = reader.GetInt32(0),
                    Firstname = reader.GetString(1),
                    Lastname = reader.GetString(2),
                    Birthplace = reader.GetString(4),
                    Bio = reader.GetString(6),
                };
                try { item.Birthdate = reader.GetDateTime(3); }
                catch {; }
                try { item.Deathdate = reader.GetDateTime(5); }
                catch {; }
                return View(item);
            }
            return View(null);
        }

        public void _Edit(PersonViewModel model)
        {
            Database.Update("[Person]", string.Format("[Firstname] = '{0}', [Lastname] = '{1}', [Birthdate] = {2}, [Deathdate] = {3}, [Birthplace] = '{4}', [Bio] = '{5}'", model.Firstname, model.Lastname, (model.Birthdate.HasValue) ? "'" + model.Birthdate.Value.ToString(@"yyyy-MM-dd") + "'" : "null", (model.Deathdate.HasValue) ? "'" + model.Deathdate.Value.ToString(@"yyyy-MM-dd") + "'" : "null", model.Birthplace, model.Bio), $"[Id] = {model.Id}");
        }
    }
}