using GazlVolunteer.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.Dtos
{
    public class VolunteerModelDto
    {
        public string CaseName { get; set; }
        public string IdNumber { get; set; }
        public List<string> PhoneNumber { get; set; }
        public AgencyEnum agency { get; set; }
        public CaseEnum Case { get; set; }
    }
}
