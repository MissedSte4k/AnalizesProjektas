using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Yard
    {
        public int YardId { get; set; }
        public string Address { get; set; }
        public List<WareHouse> Warehouses { get; set; }
    }
}
