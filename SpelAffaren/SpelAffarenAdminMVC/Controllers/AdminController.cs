using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpelDatabas;
using System.Data.Entity;

namespace SpelAffarenAdminMVC.Controllers
{
    public class AdminController : BaseController
    {
        public Personer GetAdmin()
        {
            using (var db = new SpelDatabasContainer())
            {
                var admin = (from a in db.PersonerSet
                             where LoggedInAdmin.AdminStatus == true
                             select a).FirstOrDefault();
                return admin ?? new Personer();
            }
        }

        public void PopulateDatabase()
        {
            using (var db = new SpelDatabasContainer())
            {
                db.PersonerSet.Add(new Personer { Förnamn = "Admin", Efternamn = "Admin", LogOnEmail = "admin@adminsson.com", Lösenord = "Admin", AdminStatus = true });
                db.SaveChanges();

                db.GenreSet.Add(new Genre { Namn = "Action" });
                db.GenreSet.Add(new Genre { Namn = "Första Person" });
                db.GenreSet.Add(new Genre { Namn = "Arkad" });
                db.GenreSet.Add(new Genre { Namn = "Simulation" });
                db.GenreSet.Add(new Genre { Namn = "Rollspel" });
                db.GenreSet.Add(new Genre { Namn = "MMO" });
                db.GenreSet.Add(new Genre { Namn = "MMORPG" });
                db.GenreSet.Add(new Genre { Namn = "Strategi" });
                db.GenreSet.Add(new Genre { Namn = "Stealth" });
                db.GenreSet.Add(new Genre { Namn = "Turordningsbaserad" });
                db.GenreSet.Add(new Genre { Namn = "Sci-Fi" });
                db.GenreSet.Add(new Genre { Namn = "Fighting" });
                db.GenreSet.Add(new Genre { Namn = "Skräck" });
                db.GenreSet.Add(new Genre { Namn = "Äventyr" });
                db.GenreSet.Add(new Genre { Namn = "Racing" });
                db.GenreSet.Add(new Genre { Namn = "MOBA" });
                db.GenreSet.Add(new Genre { Namn = "Free2Play" });
                db.SaveChanges();

                db.KonsolSet.Add(new Konsol { Namn = "Playstation 3" });
                db.KonsolSet.Add(new Konsol { Namn = "Playstation 4" });
                db.KonsolSet.Add(new Konsol { Namn = "PSP" });
                db.KonsolSet.Add(new Konsol { Namn = "Xbox 360" });
                db.KonsolSet.Add(new Konsol { Namn = "Xbox One" });
                db.SaveChanges();

                db.UtgivareSet.Add(new Utgivare { Namn = "EA Games" });
                db.UtgivareSet.Add(new Utgivare { Namn = "EA Sports" });
                db.UtgivareSet.Add(new Utgivare { Namn = "Rockstar Games" });
                db.UtgivareSet.Add(new Utgivare { Namn = "Valve" });
                db.UtgivareSet.Add(new Utgivare { Namn = "Crytek" });
                db.UtgivareSet.Add(new Utgivare { Namn = "Apogee Software" });
                db.UtgivareSet.Add(new Utgivare { Namn = "Ubisoft Entertainment" });
                db.UtgivareSet.Add(new Utgivare { Namn = "Bohemia Interactive" });
                db.UtgivareSet.Add(new Utgivare { Namn = "Blizzard Entertainment" });
                db.UtgivareSet.Add(new Utgivare { Namn = "2K Games" });
                db.UtgivareSet.Add(new Utgivare { Namn = "Telltale Games" });
                db.SaveChanges();
            }
        }

        public ActionResult Index()
        {
            
            return View();
        }

        // att implementeras

        [HttpPost]
        public ActionResult CreatePublisher(string name)
        {
            using (var db = new SpelDatabasContainer())
            {
                var utgivare = new Utgivare { Namn = name };
               
                db.UtgivareSet.Add(utgivare);
                db.Entry(utgivare).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditPublisher(int id)
        {
            using (var db = new SpelDatabasContainer())
            {
                var utgivare = (from u in db.UtgivareSet
                                where u.Id == id
                                select u).FirstOrDefault();
                db.Entry(utgivare).State = EntityState.Detached;
                if (utgivare != null)
                    return View(utgivare);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditPublisher(int id, string namn, string[] games)
        {
            using (var db = new SpelDatabasContainer())
            {
                var intGames = games == null ? new int[0] : games.Select(int.Parse);
                var utgivare = (from u in db.UtgivareSet
                                where id == u.Id
                                select u).FirstOrDefault();
                if (utgivare != null)
                {
                    foreach (var game in intGames.Select(gamet => (from g in db.ProduktSet
                                                                   where gamet == g.Id
                                                                   select g).FirstOrDefault()).Where(game => game != null))
                    {
                        utgivare.Produkt.Add(game);
                    }
                    utgivare.Namn = namn == null ? utgivare.Namn : namn;
                    db.Entry(utgivare).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        public ActionResult DeletePublisher(int id)
        {
            using (var db = new SpelDatabasContainer())
            {
                var utgivare = (from u in db.UtgivareSet
                                where u.Id == id
                                select u).FirstOrDefault();

                db.UtgivareSet.Remove(utgivare);
                db.Entry(utgivare).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateConsole(string name)
        {
            using (var db = new SpelDatabasContainer())
            {
                var konsol = new Konsol { Namn = name };
               
                db.KonsolSet.Add(konsol);
                db.Entry(konsol).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditConsole(int id)
        {
            using (var db = new SpelDatabasContainer())
            {
                var konsol = (from k in db.KonsolSet
                              where k.Id == id
                              select k).FirstOrDefault();
                db.Entry(konsol).State = EntityState.Detached;
                if (konsol != null)
                    return View(konsol);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditConsole(int id, string namn, string[] games)
        {
            using (var db = new SpelDatabasContainer())
            {
                var intGames = games == null ? new int[0] : games.Select(int.Parse);
                var konsol = (from u in db.KonsolSet
                              where id == u.Id
                              select u).FirstOrDefault();
                if (konsol != null)
                {
                    foreach (var game in intGames.Select(gamet => (from g in db.ProduktSet
                                                                   where gamet == g.Id
                                                                   select g).FirstOrDefault()).Where(game => game != null))
                    {
                        konsol.Produkt.Add(game);
                    }
                    konsol.Namn = namn == null ? konsol.Namn : namn;
                    db.Entry(konsol).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteConsole(int id)
        {
            using (var db = new SpelDatabasContainer())
            {
                var konsol = (from u in db.KonsolSet
                              where u.Id == id
                              select u).FirstOrDefault();

                db.KonsolSet.Remove(konsol);
                db.Entry(konsol).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateGenre(string name)
        {
            using (var db = new SpelDatabasContainer())
            {
                var genre = new Genre { Namn = name };

                db.GenreSet.Add(genre);
                db.Entry(genre).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult EditGenre(int id)
        {
            using (var db = new SpelDatabasContainer())
            {
                var genre = (from g in db.GenreSet
                             where id == g.Id
                             select g).FirstOrDefault();
                db.Entry(genre).State = EntityState.Detached;
                if (genre != null)
                    return View(genre);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditGenre(int id, string namn, string[] games)
        {
            using (var db = new SpelDatabasContainer())
            {
                var intGames = games == null ? new int[0] : games.Select(int.Parse);
                var genre = (from g in db.GenreSet
                             where id == g.Id
                             select g).FirstOrDefault();
                if (genre != null)
                {
                    foreach (var game in intGames.Select(gamet => (from g in db.ProduktSet
                                                                   where gamet == g.Id
                                                                   select g).FirstOrDefault()).Where(game => game != null))
                    {
                        genre.Produkt.Add(game);
                    }
                    genre.Namn = namn == null ? genre.Namn : namn;
                    db.Entry(genre).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteGenre(int id)
        {
            using (var db = new SpelDatabasContainer())
            {
                var genre = (from g in db.GenreSet
                             where id == g.Id
                             select g).FirstOrDefault();

                db.GenreSet.Remove(genre);
                db.Entry(genre).State = EntityState.Deleted;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult CreateGame(string name, string utgivareId, string[] konsoler, string[] genres, string beskrivning, string utgivningsår)
        {
            using (var db = new SpelDatabasContainer())
            {
                var utgId = utgivareId == null ? 0 : int.Parse(utgivareId);
                var utg = (from u in db.UtgivareSet where u.Id == utgId
                           select u).FirstOrDefault();
                // tänk på vad som är null här och inte 
                var spel = new Produkt
                {
                    Utgivare = utg,
                    Namn = name,
                    Utgivningsår = int.Parse(utgivningsår),
                    Beskrivning = beskrivning,
                    UtgivareId = utgId,
                };
                var intKonsoler = konsoler.Select(int.Parse).ToArray();
                var intGenres = genres.Select(int.Parse).ToArray();
                foreach (var genre in intGenres.Select(gen => (from g in db.GenreSet
                                                               where gen == g.Id
                                                               select g).FirstOrDefault()).Where(genre => genre != null))
                {
                    spel.Genre.Add(genre);
                }
                foreach (var konsol in intKonsoler.Select(konsol => (from k in db.KonsolSet
                                                                     where konsol == k.Id
                                                                     select k).FirstOrDefault()).Where(konsol => konsol != null))
                {
                    spel.Konsol.Add(konsol);
                }

                db.ProduktSet.Add(spel);
                db.Entry(spel).State = EntityState.Added;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public ActionResult EditGame(int id)
        {
            using (var db = new SpelDatabasContainer())
            {
                var spel = (from s in db.ProduktSet
                            where s.Id == id
                            select s).FirstOrDefault();
                db.Entry(spel).State = EntityState.Detached;
                if (spel != null)
                    return View(spel);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult EditGame(int id, string namn, string utgivare, string[] konsoler, string[] genres, string beskrivning, string utgivningsår)
        {
            using (var db = new SpelDatabasContainer())
            {
                var spel = (from s in db.ProduktSet
                                where s.Id == id
                                select s).FirstOrDefault();
                if (spel != null)
                {
                    spel.Namn = namn == null ? spel.Namn : namn;
                    if (utgivare != null)
                        spel.UtgivareId = int.Parse(utgivare);
                    if (beskrivning != null)
                        spel.Beskrivning = beskrivning;
                    if (utgivningsår != null)
                        spel.Utgivningsår = int.Parse(utgivningsår);

                    var intKonsoler = konsoler == null ? new int[0] : konsoler.Select(int.Parse).ToArray();
                    var intGenres = genres == null ? new int[0] : genres.Select(int.Parse).ToArray();

                    //Men om produkten redan har en koppling till en genre vid en update och du kör .Add()
                    //så kommer den försöka att skapa en ny.
                    //Så du kan tar bort eller lägger till en genre men loopar igenom alla som är kopplade till produkten.
                    //Det kan finnas duplikater i din lista och add försöker skapa på nytt i databasen
                    spel.Genre.Clear();
                    spel.Konsol.Clear();
                    db.SaveChanges();

                    foreach (var genre in intGenres.Select(gen => (from g in db.GenreSet
                                                                   where gen == g.Id
                                                                   select g).FirstOrDefault()).Where(genre => genre != null))
                    {
                        spel.Genre.Add(genre);
                    }

                    foreach (var konsol in intKonsoler.Select(konsol => (from k in db.KonsolSet
                                                                         where konsol == k.Id
                                                                         select k).FirstOrDefault()).Where(konsol => konsol != null))
                    {
                        spel.Konsol.Add(konsol);
                    }

                    db.ProduktSet.Add(spel);
                    db.Entry(spel).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult DeleteGame(int id)
        {
            using (var db = new SpelDatabasContainer())
            {
                var spel = (from s in db.ProduktSet
                            where s.Id == id
                            select s).FirstOrDefault();

                db.ProduktSet.Remove(spel);
                db.Entry(spel).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}