using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerIrongridC2System.Models
{
    public class Asset
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string AssetSerial { get; set; } = string.Empty;
        public string AssetType { get; set; } = string.Empty;
        public Unit Unit { get; set; } = new();
        public AssetLiveStatus? AssetLiveStatus { get; set; }
    }
}
