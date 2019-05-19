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
    public class GatesController : Controller
    {
        private readonly AnalizesProjektasContext _context;

        public GatesController(AnalizesProjektasContext context)
        {
            _context = context;
        }

        // GET: Gates
        public async Task<IActionResult> Index()
        {
            var gates = await _context.Gate.Include(x => x.WareHouse).ToListAsync();
            foreach (var gate in gates)
            {
                _context.Entry(gate).Collection(x => x.TransportType).Load();
            }
            return View(gates);
        }

        // GET: Gates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gate = await _context.Gate
                .FirstOrDefaultAsync(m => m.GateId == id);
            if (gate == null)
            {
                return NotFound();
            }

            return View(gate);
        }

        // GET: Gates/Create
        public IActionResult Create()
        {
            ViewBag.Warehouses = _context.WareHouse.ToList();
            return View();
        }

        // POST: Gates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GateId,Vieta")] Gate gate,[Bind("TransportType")] int transportType,[Bind("WareHouse")] int wareHouse)
        {
            if (ModelState.IsValid && gate.Vieta.Length != 0 && wareHouse != 0)
            {
                gate.WareHouse = _context.WareHouse.Where(x => x.WareHouseId == wareHouse).FirstOrDefault();
                gate.TransportType = new List<GateTransportType>();
                gate.TransportType.Add(new GateTransportType { PriimamoMasinosTipas = (CarType)transportType });
               

                _context.Add(gate);
                await _context.SaveChangesAsync();

                for (int d = 1; d <= 7; d++)
                {
                    for (int i = 8; i <= 17; i++)
                    {
                        _context.Add(new GateTime { Diena = DateTime.Today.AddDays(d).AddHours(i), Gate = gate});
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gate);
        }

        // GET: Gates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gate = await _context.Gate.FindAsync(id);
            if (gate == null)
            {
                return NotFound();
            }
            ViewBag.Warehouses = _context.WareHouse.ToList();
            return View(gate);
        }

        // POST: Gates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("GateId,Vieta")] Gate gate, 
            [Bind("TransportType")] int transportType, 
            [Bind("WareHouse")] int wareHouse)
        {
            gate.GateId = id;

            if (ModelState.IsValid)
            {
                try
                {
                    gate.WareHouse = _context.WareHouse.Where(x => x.WareHouseId == wareHouse).FirstOrDefault();
                    gate.TransportType = new List<GateTransportType>();
                    gate.TransportType.Add(new GateTransportType { PriimamoMasinosTipas = (CarType)transportType });
                    _context.Update(gate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GateExists(gate.GateId))
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
            return View(gate);
        }

        // GET: Gates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gate = await _context.Gate
                .FirstOrDefaultAsync(m => m.GateId == id);
            if (gate == null)
            {
                return NotFound();
            }

            return View(gate);
        }

        // POST: Gates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gate = await _context.Gate.FindAsync(id);
            _context.Gate.Remove(gate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GateExists(int id)
        {
            return _context.Gate.Any(e => e.GateId == id);
        }
    }
}
