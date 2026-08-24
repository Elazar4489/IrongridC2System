using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerIrongridC2System.Models
{
    public class AssetLiveStatus
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetType { get; set; } = string.Empty;
        public string RawValue { get; set; } = string.Empty;
        public string ProcessedStatus { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public DateTime LastUpdate { get; set; }
        public Asset Asset { get; set; } = null!;
    }
}
