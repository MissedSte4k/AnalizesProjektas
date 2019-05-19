using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string Vardas { get; set; }
        public string MasinosNr { get; set; }
        public string MasinosModelis { get; set; }
        public CarType MasinosTipas { get; set; }
        public CarStatus MasinosBusena { get; set; }
        public DateTime? AtvykimoLaikas { get; set; }
        public DateTime? IsvykimoLaikas { get; set; }
    }
}
