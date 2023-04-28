using System.ComponentModel.DataAnnotations;

namespace ROWalks.API.Models.DTO
{
    public class UpdateCountyDto
    {
        [Required]
        public int Code { get; set; }
        [Required]
        [MinLength(4,ErrorMessage = "Name has to be a minimum of 4 characters")]
        [MaxLength(50,ErrorMessage = "Name has to be a maximum of 50 characters")]
        public string Name { get; set; }
        public string? CountyImageURL { get; set; }
    }
}
