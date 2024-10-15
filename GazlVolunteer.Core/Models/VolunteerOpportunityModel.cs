using GazlVolunteer.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.Models
{
    public class VolunteerOpportunityModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public List<string> Numbers { get; set; }
        public TimeOnly Time { get; set; }
        public string Age { get; set; }
        public DateOnly Date { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
    }
}
