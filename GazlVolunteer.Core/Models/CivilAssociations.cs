using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.Models
{
    public class CivilAssociations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Contacts { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
