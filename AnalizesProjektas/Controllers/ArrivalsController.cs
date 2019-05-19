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
    public class ArrivalsController : Controller
    {
        private readonly AnalizesProjektasContext _context;

        public ArrivalsController(AnalizesProjektasContext context)
        {
            _context = context;
        }

        // GET: Arrivals
        public async Task<IActionResult> Index()
        {
            var arrivals = await _context.Shipments.Include(x => x.driver).Include(x => x.gateTime).Include(x => x.gateTime.Gate).ToListAsync();
            return View(await _context.Shipments.ToListAsync());
        }

        // POST: Arrivals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Busena")] Shipment shipment)
        {
            var oldShipment = _context.Shipments.Include(x => x.driver).FirstOrDefault(x => x.ShipmentId == id);

            if (oldShipment != null)
            {
                try
                {
                    oldShipment.driver.MasinosBusena = (CarStatus)shipment.Busena;
                    if (oldShipment.driver.MasinosBusena == CarStatus.Atvykus)
                    {
                        oldShipment.driver.AtvykimoLaikas = DateTime.Now;
                    }
                    if (oldShipment.driver.MasinosBusena == CarStatus.Išvykus)
                    {
                        oldShipment.driver.IsvykimoLaikas = DateTime.Now;
                    }
                    _context.Update(oldShipment);
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

        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.ShipmentId == id);
        }
    }
}
