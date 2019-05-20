using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class GateTime
    {
        public int GateTimeId { get; set; }
        [Display(Name = "Expected arrival Time")]
        public DateTime Diena { get; set; }
        public Gate Gate { get; set; }
        public bool? IsUsed { get; set; }
    }
}
