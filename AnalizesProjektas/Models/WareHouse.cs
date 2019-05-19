using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class WareHouse
    {
        public int WareHouseId { get; set; }
        public string InternalAddress { get; set; }
        public List<Gate> Gates { get; set; }
    }
}
