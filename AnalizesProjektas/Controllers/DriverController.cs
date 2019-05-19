﻿using System;
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

            if (id == null)
            {
                return NotFound();
            }

            var shipment = _context.Shipments.Include(x=> x.driver).FirstOrDefault(x => x.ShipmentId == id);
            if (shipment == null)
            {
                var supplier = new Supplier() { ImonesPavadinimas = "kainava", SupplierId = 0, TelefonoNr = "8612312312", VardasPavarde = "Jonas Jonaitis" };
                shipment = new Shipment() { ShipmentId = 0, CreationDate = DateTime.Now, SupplierLink = "sss", Busena = ShipmentStatus.PendingApproval, supplier = supplier, delays = null, gateTime = null, driver = null, Products = null };
                _context.Shipments.Add(shipment);
                _context.SaveChanges();
            }
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment);
        }

        public IActionResult DriverForm(int? id)
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

        // POST: Driver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult submitDriverForm(int id,[Bind("DriverId","Vardas", "MasinosTipas", "MasinosNr", "MasinosModelis", "MasinosBusena")] Driver driver)
        {
            id = 4;
            if (ModelState.IsValid)
            {
                driver.MasinosBusena = CarStatus.Išvykus;
                var shipment = _context.Shipments.Find(id);
                shipment.driver = driver;
                shipment.Busena = ShipmentStatus.CarrierApproved;
                _context.SaveChanges();
                return RedirectToAction("checkIfDriverRegistered", new { id = id });
            }
            return View(driver);
        }

        // GET: Driver/Edit/5
        public IActionResult EditDriverForm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = _context.Shipments.Include(x => x.driver).FirstOrDefault(x => x.ShipmentId == id);
            if (shipment == null)
            {
                return NotFound();
            }
            return View(shipment.driver);
        }

        // POST: Driver/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitDriverUpdate(int id, [Bind("DriverId", "Vardas", "MasinosTipas", "MasinosNr", "MasinosModelis", "MasinosBusena")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                var shipment = _context.Shipments.Where(x => x.driver.DriverId == driver.DriverId).FirstOrDefault();

                try
                {
                    _context.Update(shipment.driver = driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShipmentExists(driver.DriverId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("checkIfDriverRegistered", new { id = shipment.ShipmentId });
            }
            return View(driver);
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
