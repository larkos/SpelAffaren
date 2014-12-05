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

            var list = proxy.HämtaProdukter().OrderBy(f => f.Beställningar);

          return View(list);
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

            // Lägg in här att hämta ifrån servicen GetProductsFromGenre
            List<ProduktDto> response = service.HämtaProdukter();
            TheShopModel model = new TheShopModel();
            model.Cart = MinKV;
            model.ProductsInCategory = response;
            model.AvaliableGenre = service.GetAllGenre();
            //ViewBag.message = " this repo was created" + myrepo.ToString()+" and your cookie is "+mycookie+" there is a shopping cart with number "+MinKV.Owner+" that contains "+MinKV.Products.Count()+" Items and was created "+MinKV.Skapad;
            return View(model);
        }

        [HttpPost]
        public PartialViewResult ShoppingCart()
        {
            string mycookie = Request.Cookies["Klient"].Value;
            Kundvagn MinKV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(mycookie) select k).FirstOrDefault();
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
            return PartialView("ShoppingCart",MinKV);
        }

        public ActionResult OrderHistorik()
        {
            SpelAffarService SAS = new SpelAffarService();

            List<OrderDto> retur = new List<OrderDto>();
            if ((PersonDto)Session["User"] != null)
            {
                PersonDto pd = (PersonDto)Session["User"];
                retur = SAS.OrderHistorik(pd);
                ViewBag.Message = "Hej " + pd.Förnamn + " Här visar vi din order historik här hos oss! Här nedanför kan du tittat när du har beställt";


            }
            else
            {
                ViewBag.Message = "Din orderhistorik kan inte hämtas för du är ej inloggad";
            }
            return View(retur);
        }

        [HttpPost]
        public PartialViewResult CommentKV(string OrderComment)
        {
            Kundvagn KV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(Request.Cookies["Klient"].Value) select k).FirstOrDefault();

            KV.OrderComment = OrderComment;

            return PartialView("ShoppingCart",KV);
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
                KV.CartCostCount();
            return PartialView("ShoppingCart",KV);
        }

        [HttpPost]
        public PartialViewResult RemoveProdukt(string id)
        {
            Kundvagn KV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(Request.Cookies["Klient"].Value) select k).FirstOrDefault();

            ProduktDto exist = (from list in KV.Products where list.Id == int.Parse(id) select list).FirstOrDefault();
            KV.Products.Remove(exist);

            KV.CartCostCount();
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

            change.CartCostCount();
            return PartialView("ShoppingCart",change);
        }

        [HttpPost]
        public PartialViewResult GetByGenre(int Genre)
        {
            //Den här kodbiten kan komma att ändras beroende på hur de ser ut//
            var ProdByGenre = new List<ProduktDto>();

            SpelAffarService SAS = new SpelAffarService();
            ProdByGenre=SAS.HämtaProduktViaGenre(Genre);
            
            return PartialView("Products", ProdByGenre);
        }

        
        public PartialViewResult Pay()
        {
            OrderDto retur = new OrderDto();
            Kundvagn MinKV = (from k in KundvagnsRepo._repo.Kundvagnar where k.Owner == int.Parse(Request.Cookies["Klient"].Value) select k).FirstOrDefault();

            SpelAffarService SAS = new SpelAffarService();
            int[] Produkter = (from kv in MinKV.Products select kv.Id).ToArray();
            if (MinKV.OrderComment == null)
                MinKV.OrderComment = "";
            PersonDto p = (PersonDto)Session["User"];
            if (p != null)
            {
                retur = SAS.NyOrder(p.Id, Produkter, MinKV.OrderComment);
                MinKV.cleanout_KundVagn();

                ViewBag.Message = "Du har betalat. Tack och välkommen åter!";
            }
            else
            {
                ViewBag.Message = "Du är inte inloggad. Vänligen logga in dig innan du försöker handla!";
            }

            //SAS.Pay();
            return PartialView("Payment",retur);
        }
       
        public ActionResult Payment(OrderDto od)
        {


            return View("Payment","Home",od);
        }


    }
}