using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSeed.Models
{
    internal class County
    {
        public Guid CountyId { get; set; }
        public int Code { get; set; }
        public string? Name { get; set; }
        public string? CountyImageURL { get; set; }
    }
}
