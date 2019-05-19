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
                if (id == null)
                {
                    return NotFound();
                }

                var shipment = _context.Shipments.Find(id);
                if (shipment == null)
                {
                    return NotFound();
                }
            return View(shipment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult submitSupplierForm([Bind("SupplierId", "ImonesPavadinimas", "TelefonoNr", "VardasPavarde", "Shipment")] Supplier supplier, int id)
        {
            if (DataIsValid(supplier))
            {
                Shipment shipment = _context.Shipments.Find(id);
                shipment.supplier = supplier;
                _context.Add(supplier);
                
                shipment.UpdateShipmentDB(supplier);
                _context.SaveChanges();
                return View(shipment);
            }
            return register(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DataIsValid(Supplier supplier)
        {
            if ( supplier.ImonesPavadinimas != "" && supplier.TelefonoNr != "" && supplier.VardasPavarde != "")
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
