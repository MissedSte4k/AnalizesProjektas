using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Worker
    {
        public int ID { get; set; }

        [Required]
        public String name { get; set; }

        [Required]
        public String password { get; set; }

        public WorkerGroup type { get; set; }

        public Boolean deleted { get; set; }

        public async Task<List<Worker>> getWorkers(WebApplication2Context _context)
        {
            return await _context.Worker.ToListAsync();
        }
        public void saveWorker(WebApplication2Context _context, Worker worker)
        {
            _context.Add(worker);
        }
        public void deleteWorker(WebApplication2Context _context, Worker worker)
        {
            _context.Worker.Remove(worker);
        }
        public void updateWorker(WebApplication2Context _context, Worker worker)
        {
            _context.Update(worker);
        }
    }
}
