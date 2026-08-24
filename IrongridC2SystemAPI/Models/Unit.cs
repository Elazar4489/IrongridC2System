using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerIrongridC2System.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public List<Asset> Assets { get; set; } = new();
    }
}
