using Microsoft.EntityFrameworkCore;
using OnTheLaneOfHike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Repositories
{
    public class ProposalRepository : IProposalRepository
    {
        DataBaseContext context;
        public ProposalRepository(DataBaseContext c)
        {
            context = c;
        }
        public IQueryable<ProposalModel> proposals
        {
            get
            {
                return context.Proposals.Include(proposals => proposals.Member);
            }

        }
        public void AddProposal(ProposalModel proposal)
        {
            proposal.ProposalTime= DateTime.Now;

            context.Proposals.Add(proposal);
            context.SaveChanges();
        }

        public void DeleteProposal(ProposalModel proposal)
        {
            context.Proposals.Remove(proposal);
            context.SaveChanges();
        }

        public ProposalModel GetProposalById(int ProposalId)
        {
            var proposal = (from p in context.Proposals
                          where p.ProposalID == ProposalId
                          select p).FirstOrDefault<ProposalModel>();
            return proposal;
        }

        public void UpdateProposal(ProposalModel proposal)
        {
            context.Proposals.Update(proposal);
            context.SaveChanges();
        }
    }
}
