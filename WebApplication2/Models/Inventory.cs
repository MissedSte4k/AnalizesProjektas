using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class Inventory
    {
        public int ID{ get;set;}
        
        [Required]
        public String name{get;set;}

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int amount{get;set;}

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int weight{get;set;}

        public ProductType type { get; set; }

        public async Task<List<Inventory>> getInventories(WebApplication2Context _context)
        {
            return await _context.Inventory.ToListAsync();
        }
        public void saveInventory(WebApplication2Context _context, Inventory inventory)
        {
            _context.Add(inventory);
        }
        public void deleteInventory(WebApplication2Context _context, Inventory inventory)
        {
            _context.Inventory.Remove(inventory);
        }
        public void updateInventory(WebApplication2Context _context, Inventory inventory)
        {
            _context.Update(inventory);
        }
    }
}
