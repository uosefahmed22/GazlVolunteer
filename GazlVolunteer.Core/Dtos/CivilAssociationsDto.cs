using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.Dtos
{
    public class CivilAssociationsDto
    {
        public string Name { get; set; }
        public List<string> Contacts { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
