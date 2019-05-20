using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        [Display(Name = "Driver Name")]
        public string Vardas { get; set; }
        [Display(Name = "Car #")]
        public string MasinosNr { get; set; }
        [Display(Name = "Car Model")]
        public string MasinosModelis { get; set; }
        [Display(Name = "Car Type")]
        public CarType MasinosTipas { get; set; }
        [Display(Name = "Status")]
        public CarStatus MasinosBusena { get; set; }
        [Display(Name = "Time Arrived")]
        public DateTime? AtvykimoLaikas { get; set; }
        [Display(Name = "Time Exit")]
        public DateTime? IsvykimoLaikas { get; set; }
    }
}
