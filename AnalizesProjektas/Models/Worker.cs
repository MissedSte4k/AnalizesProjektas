using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public WorkerGroup Type { get; set; }
        public bool Deleted { get; set; }
    }
}
