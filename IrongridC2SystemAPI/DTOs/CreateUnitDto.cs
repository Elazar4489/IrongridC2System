namespace IrongridC2SystemAPI.DTOs
{
    public class CreateUnitDto
    {
        public int Id { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
    }
}
