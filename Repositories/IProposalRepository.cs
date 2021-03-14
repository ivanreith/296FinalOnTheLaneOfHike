using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Repositories
{
    public interface IProposalRepository
    {
        IQueryable<ProposalModel> proposals { get; }
        void AddProposal(ProposalModel proposal);  // create
        ProposalModel GetProposalById(int ProposalId); //Retrieve a story by topic
        void UpdateProposal(ProposalModel proposal);
        void DeleteProposal(ProposalModel proposal);


    }
}
