using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Gate
    {
        public int GateId { get; set; }
        [Display(Name = "Gate")]
        public string Vieta { get; set; }
        [Display(Name = "Transport Type")]
        public virtual List<GateTransportType> TransportType { get; set; }
        [Display(Name = "Warehouse")]
        public WareHouse WareHouse { get; set; }
    }
}
