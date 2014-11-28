using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SpelAffarWCF
{
    [ServiceContract]
    public interface ISpelAffarService
    {
        [OperationContract]
        List<ProduktDto> HämtaProdukter();

        [OperationContract]
        ProduktDto HämtaProdukt(int produktId);

        [OperationContract]
        PersonDto KollaKund(string firstName, string lastName, string logOnEmail, string lösenord);

        [OperationContract]
        OrderDto NyOrder(int kundId, int[] produkter, string kommentar);
    }
}
