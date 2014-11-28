using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpelAffarWCF;
namespace SpelAffaren.Models
{
    public class TheShopModel
    {

        public Kundvagn Cart{ get; set; }
        public List<ProduktDto> ProductsInCategory {get;set;}
        
    }
}