using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GazlVolunteer.Core.Enums;
using GazlVolunteer.Core.Models.Auth;

namespace GazlVolunteer.Core.Models
{
    public class VolunteerModel
    {
        public int Id { get; set; }
        public string CaseName { get; set; }
        public string IdNumber { get; set; }
        public List<string> PhoneNumber { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
        public AgencyEnum agency { get; set; }
        public CaseEnum Case { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
    }
}
