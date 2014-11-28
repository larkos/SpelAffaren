using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SpelDatabas;
using SpelAffarWCF;
namespace SpelAffarWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<ProduktDto> GetAllProducts()
        {
            List<ProduktDto> returera = new List<ProduktDto>();
using(var db=new SpelDatabas.SpelDatabasContainer())
{
    returera = (from products in db.ProduktSet
                select new ProduktDto()
                {
                    Id = products.Id,
                    Beskrivning = products.Beskrivning,
                    Namn = products.Namn,
                    Genres = (from genre in products.Genre select new GenreDto() { Id = genre.Id, Namn = genre.Namn }).ToList(),
                    Konsoler = (from konsol in products.Konsol select new KonsolDto() { Id = konsol.Id, Namn = konsol.Namn }).ToList(),
                    SpelPerOrders = (from spo in products.SpelPerOrder select new SpelPerOrderDto() { Antal = spo.Antal, Order = new OrderDto() { Id = spo.Order.Id, Kommentar = spo.Order.Kommentar } }).ToList(),
                    Utgivare = new UtgivareDto() { Id = products.Utgivare.Id, Namn = products.Namn },
                    Utgivningsår = products.Utgivningsår
                }).ToList();

   

}
return returera;
        }

        public ProduktDto GetProduct(int value)
        {
            ProduktDto returera = new ProduktDto();
            using (var db = new SpelDatabas.SpelDatabasContainer())
            {
                returera = (from products in db.ProduktSet where products.Id==value
                            select new ProduktDto()
                            {
                                Id = products.Id,
                                Beskrivning = products.Beskrivning,
                                Namn = products.Namn,
                                Genres = (from genre in products.Genre select new GenreDto() { Id = genre.Id, Namn = genre.Namn }).ToList(),
                                Konsoler = (from konsol in products.Konsol select new KonsolDto() { Id = konsol.Id, Namn = konsol.Namn }).ToList(),
                                SpelPerOrders = (from spo in products.SpelPerOrder select new SpelPerOrderDto() { Antal = spo.Antal, Order = new OrderDto() { Id = spo.Order.Id, Kommentar = spo.Order.Kommentar } }).ToList(),
                                Utgivare = new UtgivareDto() { Id = products.Utgivare.Id, Namn = products.Namn },
                                Utgivningsår = products.Utgivningsår
                            }).FirstOrDefault();



            }
            return returera;
        }
    }
}
