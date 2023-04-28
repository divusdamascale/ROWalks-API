using System.ComponentModel.DataAnnotations;

namespace ROWalks.API.Models.Domain
{
    public class Walk
    {
        [Key]
        public Guid WalkdID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid CountyId { get; set; }


        //Navigation Properties
        public Difficulty Difficulty { get; set; }
        public County County { get; set; }
    }
}
