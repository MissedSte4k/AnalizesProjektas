using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Gate
    {
        public int GateId { get; set; }
        public string Vieta { get; set; }
        public List<GateTransportType> TransportTypes { get; set; }
    }
}
