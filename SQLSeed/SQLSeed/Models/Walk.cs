using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSeed.Models
{
    internal class Walk
    {
        [Key]
        public Guid WalkdID { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public int LengthInKm { get; set; }
        public string? WalkImageURL { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid CountyId { get; set; }
    }
}
