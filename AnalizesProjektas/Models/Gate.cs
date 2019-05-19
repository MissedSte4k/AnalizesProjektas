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
        public string Vieta { get; set; }
        public virtual List<GateTransportType> TransportType { get; set; }
        public WareHouse WareHouse { get; set; }
    }
}
