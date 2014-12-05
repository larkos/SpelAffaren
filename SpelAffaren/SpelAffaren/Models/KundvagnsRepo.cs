using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpelAffarWCF;
namespace SpelAffaren.Models
{
    public class KundvagnsRepo
    {
        public static KundvagnsRepo _repo {get;set;}
        

        public DateTime RepoCreated { get; set; }
        public List<Kundvagn> Kundvagnar { get; set; }
        public static void initRepo()
        {
            if(_repo==null)
            {
                _repo = new KundvagnsRepo();
            }
           
        }

        public KundvagnsRepo()
        {
           RepoCreated = DateTime.Now;
            Kundvagnar = new List<Kundvagn>();
        }

        private void cleanout_KundVagn()
        {

        }

        public Kundvagn GetMyKV(string KVowner)
        {

            Kundvagn KV = (from k in _repo.Kundvagnar where k.Owner == int.Parse(KVowner) select k).FirstOrDefault();

            return KV;
        }


    }

    public class Kundvagn
    {
        public int Owner { get; set; }

        public List<ProduktDto> Products { get; set; }
        public ProduktDto heading = new ProduktDto();
        public DateTime Skapad { get; set; }
        public double Totalt { get; set; }
        public string OrderComment { get; set; }

        public void cleanout_KundVagn()
        {
            List<ProduktDto> Ny = new List<ProduktDto>();
            Products = Ny;
            OrderComment = "";
            
        }

        public Kundvagn()
        {

        }
        public Kundvagn(HttpResponseBase Response,HttpRequestBase Request)
        {



            if (Request.Cookies["Klient"] == null || KundvagnsRepo._repo.GetMyKV(Request.Cookies["Klient"].Value)==null)
            {


                HttpCookie KartOwner = new HttpCookie("Klient");
                KartOwner.Value = CookieValue();
                KartOwner.Expires = DateTime.Now.AddHours(3);

                Response.Cookies.Add(KartOwner);
                Owner = int.Parse(KartOwner.Value);
                Products = new List<ProduktDto>();
                Skapad = DateTime.Now;
                OrderComment = "Ingen kommentar..";
                KundvagnsRepo._repo.Kundvagnar.Add(new Kundvagn { Owner = Owner, Products = Products, Skapad = Skapad });
            }
            
        }

        public static string CookieValue()
        {
            //Lägg till sedan så att ett redan aktiv kundvagn inte kan bli framslumpad
            int retur;
            

                Random Slump = new Random();
                retur = Slump.Next(0, 999999);
           
            return retur.ToString();

        }

      public void AddToCart(ProduktDto sp,string Owner)
        {
          Kundvagn KV=KundvagnsRepo._repo.GetMyKV(Owner);

          ProduktDto Exist = (from p in KV.Products where sp.Id == p.Id select p).FirstOrDefault();
          //if(Exist!=null)
          //{
              
          //}
          //else
          //{
              KV.Products.Add(sp);
          //}
          

          CartCostCount();
        }
        public void DeleteFromCart(ProduktDto sp,string Owner)
      {
              Kundvagn KV=KundvagnsRepo._repo.GetMyKV(Owner);
          KV.Products.Remove(sp);
          CartCostCount();
      }

        public void CartCostCount()
        {
            double kostnad=0;
            foreach(ProduktDto sp in this.Products)
            {
                kostnad += sp.Pris;
            }

            this.Totalt = kostnad;
        }
    }

    public class spelprodukt
    {
        public int pris { get; set; }
        public int antal { get; set; }
        public int Id {get;set;}
        public string Namn {get;set;}
        public int GenreId {get;set;}
        public int KonsolId {get;set;}
        public string Beskriving {get;set;}
        public int UtgivningsAr {get;set;}

        public int Spelkostnad { get; set; }
    }

    public class produktupdater
    {
        public int id { get; set; }
        public int antal { get; set; }
    }

    
}