using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalizesProjektas.Models
{
    public class SendingProduct
    {
        public int SendingProductId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public int Weight { get; set; }
        public ProductType Type { get; set; }

    }
}
