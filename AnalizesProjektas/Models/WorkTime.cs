using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class WorkTime
    {
        public int WorkTimeId { get; set; }
        public DateTime Pradzia { get; set; }
        public DateTime Pabaiga { get; set; }
    }
}
