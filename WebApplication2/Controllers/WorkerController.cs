using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class WorkerController : Controller
    {
        private readonly WebApplication2Context _context;

        public WorkerController(WebApplication2Context context)
        {
            _context = context;
        }

        // GET: Worker
        public async Task<IActionResult> WorkersList()
        {
            Worker temp = new Worker();
            List<Worker> list = await temp.getWorkers(_context);
            return View(list);
        }

        // GET: Worker/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker
                .SingleOrDefaultAsync(m => m.ID == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // GET: Worker/Create
        public IActionResult WorkerData()
        {
            return View();
        }
        
        // POST: Worker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> passWorkerData([Bind("ID,name,password,type,deleted")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                IPasswordCheck passwordCheck = new PasswordCheck();
                if (passwordCheck.CheckPassword(worker.password) && worker.name != null)
                {
                    Worker temp = new Worker();
                    temp.saveWorker(_context, worker);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(WorkersList));
                }
                else
                {
                    ViewBag.Name = "*password must be longer than 5 symbols";
                    return View(nameof(WorkerData), worker);
                }
            }
            return View(worker);
        }

        // GET: Worker/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker.SingleOrDefaultAsync(m => m.ID == id);
            if (worker == null)
            {
                return NotFound();
            }
            return View(worker);
        }

        // POST: Worker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,name,password,type,deleted")] Worker worker)
        {
            if (id != worker.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                IPasswordCheck passwordCheck = new PasswordCheck();
                if (passwordCheck.CheckPassword(worker.password))
                {
                    try
                    {
                        Worker temp = new Worker();
                        temp.updateWorker(_context, worker);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!WorkerExists(worker.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(WorkersList));
                }
                else
                {
                    ViewBag.Name = "*password must be longer than 5 symbols";
                }
            }
            return View(worker);
        }

        // GET: Worker/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var worker = await _context.Worker
                .SingleOrDefaultAsync(m => m.ID == id);
            if (worker == null)
            {
                return NotFound();
            }

            return View(worker);
        }

        // POST: Worker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var worker = await _context.Worker.SingleOrDefaultAsync(m => m.ID == id);
            Worker temp = new Worker();
            temp.deleteWorker(_context, worker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(WorkersList));
        }

        private bool WorkerExists(int id)
        {
            return _context.Worker.Any(e => e.ID == id);
        }
    }
}
