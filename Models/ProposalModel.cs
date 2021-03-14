using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class ProposalModel
    {
        [Key]
        public int ProposalID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Title between 1 and 50 chars")]
        public string ProposalTitle { get; set; }

       

        [Required]
        [MaxLength(250, ErrorMessage = "Text between 1 and 250 chars")]
        public string ProposalText { get; set; }

        // changfe due to identity  =>  stuff public string Name { get; set; }
        public MemberModel Member { get; set; }

        public DateTime ProposalTime { get; set; }
       
       

        public string Slug =>
         ProposalTitle?.Replace(' ', '-').ToLower() + '-' + ProposalTime.ToString();


    }
}
