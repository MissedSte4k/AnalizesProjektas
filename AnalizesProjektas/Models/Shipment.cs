using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AnalizesProjektas.Models
{
    public class Shipment
    {
        [Display(Name = "Shipment #")]
        public int ShipmentId { get; set; }
        [Display(Name = "Creation date")]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Supplier link")]
        public string SupplierLink { get; set; }
        [Display(Name = "Shipment Status")]
        public ShipmentStatus Busena { get; set; }
        [Display(Name = "Supplier")]
        public Supplier supplier { get; set; }
        [Display(Name = "Delays")]
        public List<Delay> delays { get; set; }
        [Display(Name = "Arrival Time")]
        public GateTime gateTime { get; set; }
        [Display(Name = "Driver")]
        public Driver driver { get; set; }
        [Display(Name = "Products")]
        public List<SendingProduct> Products { get; set; }

        public GateTime getArrival()
        {
            return gateTime;
        }

        public bool checkState()
        {
            if (supplier != null)
            {
                return false;
            }
            return true;
        }
        public string getSupplierLink()
        {
            return SupplierLink;
        }

        public bool checkIfDriverRegistered()
        {
            if (driver != null)
            {
                return true;
            }
            return false;
        }

        public List<SendingProduct> GetShippmentProducts()
        {
            return Products;
        }

        public Supplier getSupplier()
        {
            return supplier;
        }

        public void UpdateShipmentDB(Driver driv)
        {
            driver = driv;
        }

        public void UpdateShipmentDB(Supplier supp)
        {
            supplier = supp;
        }
    }
}
