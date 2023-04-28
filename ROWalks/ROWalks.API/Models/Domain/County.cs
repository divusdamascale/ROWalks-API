namespace ROWalks.API.Models.Domain
{
    public class County
    {
        public Guid CountyId { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string? CountyImageURL{ get; set; } 
    }
}
