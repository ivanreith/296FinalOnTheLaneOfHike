using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class EventModel
    {
        
        [Key]
        public int EventID { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Title between 1 and 50 chars")]
        public string EventTitle { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "storyText between 1 and 250 chars")]
        public string EventText { get; set; }
        public DateTime EventTime { get; set; }
        public MemberModel Member {get; set;}
        
        public string Slug =>
         EventTitle?.Replace(' ', '-').ToLower() + '-' + EventID.ToString();
    }
}
