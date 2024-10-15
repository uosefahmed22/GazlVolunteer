using GazlVolunteer.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Core.Models
{
    public class Notifications
    {
        public int Id { get; set; }
        public string NotificationMessage { get; set; }
        public bool IsRead { get; private set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
        public void MarkAsRead()
        {
            IsRead = true;
        }
    }
}
