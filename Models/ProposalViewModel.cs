using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class ProposalViewModel
    {
        [Required]
        public int ProposalID { get; set; }
        [Required]
        public string ProposalTitle { get; set; }
        public DateTime ProposalTime { get; set; }
        [Required]
        public string ProposalText { get; set; }

        public string MemberName { get; set; }

    }
}
