using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string ImonesPavadinimas { get; set; }
        public string TelefonoNr { get; set; }
        public string VardasPavarde { get; set; }
        public Shipment Shipment { get; set; }


        public Supplier SaveSupplier(int supplierId, string imonesPavadinimas, string telefonoNr, string vardasPavarde, Shipment ShipmentId)
        {
            SupplierId = supplierId;
            ImonesPavadinimas = imonesPavadinimas;
            TelefonoNr = telefonoNr;
            VardasPavarde = vardasPavarde;
            Shipment = ShipmentId;
            return this;
        }
    }
}
