using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.Dtos
{
    public class VolunteerOpportunityModelDto
    {
        public string name { get; set; }
        public List<string> Numbers { get; set; }
        public TimeOnly Time { get; set; }
        public string Age { get; set; }
        public DateOnly Date { get; set; }
    }
}
