using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnalizesProjektas.Models;

namespace AnalizesProjektas.Controllers
{
    public class SupplierController : Controller
    {
        private readonly AnalizesProjektasContext _context;

        public SupplierController(AnalizesProjektasContext context)
        {
            _context = context;
        }

        public IActionResult register(int id)
        {
            createDummy();
            Shipment shipment = _context.Shipments.First();
            bool state = shipment.checkState();
            List<SendingProduct> prod = shipment.GetShippmentProducts();
            return View(shipment);
        }

        public void createDummy()
        {
            var prodd = new List<SendingProduct>();
            SendingProduct a = new SendingProduct() { SendingProductId = 0, Name = "a", Amount = 3, Weight = 15, Type = ProductType.ProdA };
            prodd.Add(a);

            _context.SendingProducts.Add(a);
            _context.SaveChanges();
            var ship = new Shipment() { ShipmentId = 0, CreationDate = DateTime.Now, SupplierLink = "jop", Busena = ShipmentStatus.PendingApproval, delays = null, gateTime = null, driver = null, Products = prodd };
            _context.Shipments.Add(ship);
            _context.SaveChanges();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult submitSupplierForm([Bind("SupplierId", "ImonesPavadinimas", "TelefonoNr", "VardasPavarde", "fkShipmentId")] Supplier supplier)
        {
            if (DataIsValid(supplier))
            {
                _context.Add(supplier);
                Shipment shipment = _context.Shipments.First(e => e.ShipmentId == supplier.fkShipmentId);
                shipment.UpdateShipmentDB(supplier);
                _context.SaveChanges();
                return View(shipment);
            }
            return register(supplier.fkShipmentId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DataIsValid(Supplier supplier)
        {
            if (supplier.fkShipmentId != null && supplier.ImonesPavadinimas != "" && supplier.TelefonoNr != "" && supplier.VardasPavarde != "")
            {
                return true;
            }
            return false;
        }

        // POST: Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.






    }
}
