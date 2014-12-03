using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpelAffaren.Models;
using SpelAffarWCF;
namespace SpelAffaren.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           
            KundvagnsRepo.initRepo();
            KundvagnsRepo._repo.Kundvagnar.Add(new Kundvagn(Response,Request));

            SpelAffarWCF.SpelAffarService proxy = new SpelAffarService();
            

            //Funktion för hämta det populäraste produkterna//
            List<spelprodukt> Populärast = new List<spelprodukt>();
            Populärast.Add(new spelprodukt() { antal = 2, Beskriving = "a false accusation", Namn = "The False accusation", UtgivningsAr = 1988, pris = 249, Spelkostnad = 249 * 2, GenreId = 1, KonsolId = 2, Id = 13 });
 
          return View(Populärast);
        }

        public ActionResult TheShop()
        {
            KundvagnsRepo.initRepo();
            //ViewBag.Message = "Your application description page.";
            DateTime myrepo = KundvagnsRepo._repo.RepoCreated;

            string mycookie = Request.Cookies["Klient"].Value;
            Kundvagn MinKV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(mycookie) select k).FirstOrDefault();
            //if(MinKV.Products.Count()<1)
            //{
            //    MinKV.Products.Add(new spelprodukt() { antal = 2, Beskriving = "Bomber o granater", Namn = "Pirater!", UtgivningsAr = 1992, pris = 149, Spelkostnad = 149 * 2, GenreId = 1, KonsolId = 2, Id = 12 });
            //    MinKV.Products.Add(new spelprodukt() { antal = 2, Beskriving = "a false accusation", Namn = "The False accusation", UtgivningsAr = 1988, pris = 249, Spelkostnad = 249 * 2, GenreId = 1, KonsolId = 2, Id = 13 });
            //    MinKV.Products.Add(new spelprodukt() { antal = 2, Beskriving = "Star wars", Namn = "Star Wars", UtgivningsAr = 1978, pris = 449, Spelkostnad = 449 * 2, GenreId = 1, KonsolId = 2, Id = 14 });
            //}
            
            MinKV.CartCostCount();

            SpelAffarService service = new SpelAffarService();
            // skicka med en lista med produkter till vyn istället för service som här nedan

            //List<ProduktDto> response = service.HämtaProdukter();
            //ViewBag.message = " this repo was created" + myrepo.ToString()+" and your cookie is "+mycookie+" there is a shopping cart with number "+MinKV.Owner+" that contains "+MinKV.Products.Count()+" Items and was created "+MinKV.Skapad;
            return View(new TheShopModel { Cart = MinKV, Service = service });

            SpelAffarService service = new SpelAffarService();
			// Lägg in här att hämta ifrån servicen GetProductsFromGenre
            List<ProduktDto> response = service.HämtaProdukter();

            //ViewBag.message = " this repo was created" + myrepo.ToString()+" and your cookie is "+mycookie+" there is a shopping cart with number "+MinKV.Owner+" that contains "+MinKV.Products.Count()+" Items and was created "+MinKV.Skapad;
            return View(new TheShopModel { Cart = MinKV,ProductsInCategory=response,AvaliableGenre=service.GetAllGenre()});
        }

        public PartialViewResult ShoppingCart()
        {
            //DateTime myrepo = KundvagnsRepo._repo.RepoCreated;

            //string mycookie = Request.Cookies["Klient"].Value;
            //Kundvagn MinKV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(mycookie) select k).FirstOrDefault();
            //if (MinKV.Products.Count() < 1)
            //{
            //    MinKV.Products.Add(new spelprodukt() { antal = 2, Beskriving = "Bomber o granater", Namn = "Pirater!", UtgivningsAr = 1992, pris = 149, Spelkostnad = 149 * 2, GenreId = 1, KonsolId = 2, Id = 12 });
            //    MinKV.Products.Add(new spelprodukt() { antal = 2, Beskriving = "a false accusation", Namn = "The False accusation", UtgivningsAr = 1988, pris = 249, Spelkostnad = 249 * 2, GenreId = 1, KonsolId = 2, Id = 13 });
            //    MinKV.Products.Add(new spelprodukt() { antal = 2, Beskriving = "Star wars", Namn = "Star Wars", UtgivningsAr = 1978, pris = 449, Spelkostnad = 449 * 2, GenreId = 1, KonsolId = 2, Id = 14 });
            //}

            //MinKV.CartCostCount();
            return PartialView("ShoppingCart");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View("MyTestShop");
        }

        [HttpPost]
        public PartialViewResult AddProdukt(int sp)
        {
            Kundvagn KV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(Request.Cookies["Klient"].Value) select k).FirstOrDefault();

            //ProduktDto exist = (from list in KV.Products where list.Id == sp select list).FirstOrDefault();
            SpelAffarService connect = new SpelAffarService();
            ProduktDto exist = connect.HämtaProdukt(sp);
            //if(exist != null)
            //{
            //    exist.antal += sp.antal;
            //    exist.Spelkostnad += sp.Spelkostnad;
            //}
            //else
            //{
                KV.Products.Add(exist);
            //}

            return PartialView("ShoppingCart",KV);
        }

        [HttpPost]
        public PartialViewResult RemoveProdukt(string id)
        {
            Kundvagn KV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(Request.Cookies["Klient"].Value) select k).FirstOrDefault();

            ProduktDto exist = (from list in KV.Products where list.Id == int.Parse(id) select list).FirstOrDefault();
            KV.Products.Remove(exist);

            return PartialView("ShoppingCart", KV);
        }

        [HttpPost]
        public PartialViewResult UpdateCart(List<produktupdater> produkt)
        {
              
            //List<spelprodukt> Changedspelprodukt=new List<spelprodukt>();
            Kundvagn change = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(Request.Cookies["Klient"].Value) select k).FirstOrDefault();
            for(int i=0;i<produkt.Count();i++)
            {

                
                ProduktDto tochange = (from p in change.Products where p.Id == produkt[i].id select p).FirstOrDefault();

                
                //tochange.Spelkostnad = produkt[i].antal * tochange.pris;
            }
           
            
            return PartialView("ShoppingCart",change);
        }

        [HttpPost]
        public PartialViewResult GetByGenre(int Genre)
        {
            //Den här kodbiten kan komma att ändras beroende på hur de ser ut//
            List<ProduktDto> ProdByGenre = new List<ProduktDto>();

            SpelAffarService SAS = new SpelAffarService();
            ProdByGenre=SAS.HämtaFrånGenre(Genre);
            
            return PartialView("Products", ProdByGenre);
        }

        [HttpPost]
        public ActionResult Pay()
        {
            OrderDto retur = new OrderDto();
            Kundvagn MinKV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(Request.Cookies["Klient"].Value) select k).FirstOrDefault();

            SpelAffarService SAS = new SpelAffarService();
            int[] Produkter = (from kv in MinKV.Products select kv.Id).ToArray();
            string OrderComment = "";
            PersonDto p = (PersonDto)Session["User"];
            if (p != null)
            {
                retur = SAS.NyOrder(p.Id, Produkter, OrderComment);
                MinKV.cleanout_KundVagn();
            }

            //SAS.Pay();
            return RedirectToAction("Payment",retur);
        }
       
        public ActionResult Payment(OrderDto od)
        {


            return View("Payment","Home",od);
        }


    }
}