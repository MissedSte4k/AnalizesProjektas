using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public DateTime CreationDate { get; set; }
        public string SupplierLink { get; set; }
        public ShipmentStatus Busena { get; set; }
        public Supplier supplier { get; set; }
        public List<Delay> delays { get; set; }
        public GateTime gateTime { get; set; }
        public Driver driver { get; set; }
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
