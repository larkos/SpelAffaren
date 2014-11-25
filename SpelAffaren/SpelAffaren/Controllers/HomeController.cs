using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpelAffaren.Models;
namespace SpelAffaren.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            KundvagnsRepo.initRepo();
            KundvagnsRepo._repo.Kundvagnar.Add(new Kundvagn(Response,Request));
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //DateTime myrepo=KundvagnsRepo._repo.RepoCreated;

            //string mycookie = Request.Cookies["Klient"].Value;
            //Kundvagn MinKV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(mycookie) select k).FirstOrDefault();
            //ViewBag.message = " this repo was created" + myrepo.ToString()+" and your cookie is "+mycookie+" there is a shopping cart with number "+MinKV.Owner+" that contains "+MinKV.Products.Count()+" Items and was created "+MinKV.Skapad;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}