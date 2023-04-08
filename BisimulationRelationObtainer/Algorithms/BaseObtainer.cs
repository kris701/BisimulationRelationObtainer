using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Algorithms
{
    public abstract class BaseObtainer : IObtainer
    {
        public abstract List<Pair<DFAState>> ObtainRelation(DFAProcess P, DFAProcess Q);

        public bool DoesLabelsMatch(DFAProcess P, DFAProcess Q)
        {
            foreach (var label in P.Labels)
                if (!Q.Labels.Contains(label))
                    return false;
            return true;
        }
    }
}
