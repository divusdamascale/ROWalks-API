namespace ROWalks.API.Models.DTO
{
    public class WalkDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        public CountyDto County { get; set; }
        public DifficultyDto Difficulty {  get; set; }
    }
}
