namespace IrongridC2SystemAPI.DTOs
{
    public class ReportUnitDto
    {
        public int AssetId { get; set; }
        public string AssetType { get; set; } = string.Empty;
        public string AssetSerial { get; set; } = string.Empty;
        public string ProcessedStatus { get; set; } = string.Empty;
        public bool IsVerified { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
