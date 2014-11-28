using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SpelAffarWCF
{
    public class GenreDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Namn")]
        public string Namn { get; set; }
        [Display(Name = "Produkter")]
        public List<ProduktDto> Produkter { get; set; }
    }

    public class KonsolDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Namn")]
        public string Namn { get; set; }
        [Display(Name = "Produkter")]
        public List<ProduktDto> Produkter { get; set; }
    }

    public class OrderDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Person som gjort order")]
        public int PersonId { get; set; }
        [Display(Name = "Kommentar")]
        public string Kommentar { get; set; }
        [Display(Name = "Spel i ordern")]
        public List<SpelPerOrderDto> SpelPerOrders { get; set; }
        [Display(Name = "Datum för order")]
        public DateTime Datum { get; set; }
    }

    public class PersonDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Förnamn")]
        public string Förnamn { get; set; }
        [Display(Name = "Efternamn")]
        public string Efternamn { get; set; }
        [Display(Name = "Email")]
        public string LogOnEmail { get; set; }
        [Display(Name = "Lösenord")]
        public string Lösenord { get; set; }
        [Display(Name = "Ordrar")]
        public List<OrderDto> Ordrar { get; set; }
    }

    public class ProduktDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Namn")]
        public string Namn { get; set; }
        [Display(Name = "Beskrivning")]
        public string Beskrivning { get; set; }
        [Display(Name = "Utgivningsår")]
        public int Utgivningsår { get; set; }
        [Display(Name = "Konsoler")]
        public List<KonsolDto> Konsoler { get; set; }
        [Display(Name = "Genres")]
        public List<GenreDto> Genres { get; set; }
        [Display(Name = "Spel i ordern")]
        public List<SpelPerOrderDto> SpelPerOrders { get; set; }
        [Display(Name = "Utgivare")]
        public UtgivareDto Utgivare { get; set; }
        [Display(Name = "Beställningar")]
        public int Beställningar { get; set; }
        [Display(Name = "Betyg")]
        public double Betyg { get; set; }
        [Display(Name = "Pris")]
        public double Pris { get; set; }
        [Display(Name = "Multiplayer")]
        public bool Multiplayer { get; set; }
        [Display(Name = "Singleplayer")]
        public bool Singleplayer { get; set; }
    }

    public class SpelPerOrderDto
    {
        [Display(Name = "Antal av spelet")]
        public int Antal { get; set; }
        [Display(Name = "Spelet")]
        public int SpelId { get; set; }
        [Display(Name = "Order")]
        public int OrderId { get; set; }
    }

    public class UtgivareDto
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Display(Name = "Namn")]
        public string Namn { get; set; }
        [Display(Name = "Produkter")]
        public List<ProduktDto> Produkter { get; set; }
    }
}
