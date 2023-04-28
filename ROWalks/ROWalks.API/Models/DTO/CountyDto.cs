namespace ROWalks.API.Models.DTO
{
    public class CountyDto
    {
        public Guid CountyId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string? CountyImageURL  { get; set; }

    }
}
