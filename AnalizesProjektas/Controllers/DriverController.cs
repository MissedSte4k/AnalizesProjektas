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
        public IActionResult checkIfDriverRegistered(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var shipment = _context.Shipments.Include(x=> x.driver)
                .Include(x => x.supplier)
                .Include(x => x.delays)
                .Include(x => x.gateTime)
                .Include(x => x.gateTime.Gate)
                .Include(x => x.gateTime.Gate.WareHouse)
                .FirstOrDefault(x => x.ShipmentId == id);
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
            ViewBag.ShipmentId = shipment.ShipmentId;
            return View(shipment);
        }

        // POST: Driver/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult submitDriverForm(int ShipmentId, [Bind("DriverId","Vardas", "MasinosTipas", "MasinosNr", "MasinosModelis", "MasinosBusena")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                driver.MasinosBusena = CarStatus.Išvykus;
                var shipment = _context.Shipments.Find(ShipmentId);
                shipment.driver = driver;
                shipment.Busena = ShipmentStatus.CarrierApproved;
                _context.SaveChanges();
                return RedirectToAction("checkIfDriverRegistered", new { id = ShipmentId });
            }
            return View(driver);
        }

        public IActionResult ReggisterDelay(int? id)
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

        // GET: Driver/Edit/5
        public IActionResult RegisterArrival(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = _context.Shipments.Include(x => x.driver).Include(x => x.gateTime).FirstOrDefault(x => x.ShipmentId == id);
            if (shipment == null)
            {
                return NotFound();
            }

            var gate = _context.Gate.FirstOrDefault(x => x.TransportType.Where(y => y.PriimamoMasinosTipas == shipment.driver.MasinosTipas).Any());
            ViewBag.gateTimes = _context.GateTime.Where(x => x.Gate.GateId == gate.GateId && !_context.Shipments.Any(y => y.gateTime.GateTimeId == x.GateTimeId)).OrderBy(x => x.Diena).GroupBy(x => x.Diena.Date);
            return View(shipment);
        }

        // POST: Arrivals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> RegisterArrival(int id, int gateTimeId)
        {
            var oldShipment = _context.Shipments.Include(x => x.driver).FirstOrDefault(x => x.ShipmentId == id);

            if (oldShipment != null)
            {
                try
                {
                    oldShipment.gateTime = _context.GateTime.FirstOrDefault(x => x.GateTimeId == gateTimeId);
                    _context.Update(oldShipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            }
            return RedirectToAction(nameof(RegisterArrival));
        }

        public IActionResult RegisterDelay(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shipment = _context.Shipments.Include(x => x.driver).Include(x => x.gateTime).FirstOrDefault(x => x.ShipmentId == id);
            if (shipment == null)
            {
                return NotFound();
            }

            var gate = _context.Gate.FirstOrDefault(x => x.TransportType.Where(y => y.PriimamoMasinosTipas == shipment.driver.MasinosTipas).Any());
            ViewBag.gateTimes = _context.GateTime.Where(x => x.Gate.GateId == gate.GateId && !_context.Shipments.Any(y => y.gateTime.GateTimeId == x.GateTimeId)).OrderBy(x => x.Diena).GroupBy(x => x.Diena.Date);
            return View(shipment);
        }

        // POST: Arrivals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> RegisterDelay(int id, int gateTimeId)
        {
            var oldShipment = _context.Shipments.Include(x => x.driver).Include(x => x.delays).FirstOrDefault(x => x.ShipmentId == id);

            if (oldShipment != null)
            {
                try
                {
                    Delay delay = new Delay() { DelayId = id, NaujasAtvykimoLaikas = _context.GateTime.FirstOrDefault(e => e.GateTimeId == gateTimeId) };
                    if(oldShipment.delays == null)
                    {
                        List<Delay> list = new List<Delay>();
                        list.Add(delay);
                        oldShipment.delays = list;
                    }
                    else
                    {
                        oldShipment.delays.Add(delay);
                    }
                    
                    _context.Update(oldShipment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                }
            }
            return RedirectToAction(nameof(RegisterArrival));
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


        private bool ShipmentExists(int id)
        {
            return _context.Shipments.Any(e => e.ShipmentId == id);
        }
    }
}
