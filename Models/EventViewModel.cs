using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class EventViewModel
    {

        [Required]
        public int EventID { get; set; }
        [Required]
        public string EventTitle { get; set; }

        [Required]
        public string EventText { get; set; }
        public DateTime EventTime { get; set; }

        public string MemberName { get; set; }
    }
}
