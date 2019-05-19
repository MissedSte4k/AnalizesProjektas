using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Delay
    {
        public int DelayId { get; set; }
        public DateTime VelavimoLaikas { get; set; }
        public DateTime NaujasAtvykimoLaikas { get; set; }
    }
}
