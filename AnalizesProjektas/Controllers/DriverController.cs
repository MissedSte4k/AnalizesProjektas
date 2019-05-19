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
    public class DriverController : Controller
    {
        private readonly AnalizesProjektasContext _context;

        public DriverController(AnalizesProjektasContext context)
        {
            _context = context;
        }

        // GET: Driver
        public async Task<IActionResult> Index()
        {
            return View(await _context.Shipments.ToListAsync());
        }

        // GET: Driver/Details/5


        // GET: Driver/Create
        public IActionResult checkIfDriverRegistered(int id)
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


        // POST: Driver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShipmentId")] Shipment shipment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shipment);
        }

        // GET: Driver/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments.FindAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        // POST: Driver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShipmentId")] Shipment shipment)
        {
            if (id != shipment.ShipmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(shipment.ShipmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(shipment);
        }

        // GET: Driver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = await _context.Shipments
                .FirstOrDefaultAsync(m => m.ShipmentId == id);
            if (shipment == null)
            {
                return NotFound();
            }

            return View(shipment);
        }

        // POST: Driver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);
            _context.Shipments.Remove(shipment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.ShipmentId == id);
        }
    }
}
