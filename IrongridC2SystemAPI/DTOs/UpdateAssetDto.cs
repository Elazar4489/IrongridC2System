namespace IrongridC2SystemAPI.DTOs
{
    public class UpdateAssetDto
    {
        public int Id { get; set; }
        public int UnitId { get; set; }
        public string AssetSerial { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
