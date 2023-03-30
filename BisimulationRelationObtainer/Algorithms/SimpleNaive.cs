using BisimulationRelationObtainer.Helpers;
using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Algorithms
{
    public class SimpleNaive : BaseObtainer
    {
        public override List<Pair<State>> ObtainRelation(Process P, Process Q)
        {
            HashSet<Pair<State>> R = new HashSet<Pair<State>>();
            Queue<Pair<State>> todo = new Queue<Pair<State>>();

            if (!DoesLabelsMatch(P,Q))
                throw new Exception("Process labels did not match!");

            State p0 = StateHelper.GetInitState(P.States);
            State q0 = StateHelper.GetInitState(Q.States);

            todo.Enqueue(new Pair<State>(p0,q0));

            while (todo.Count > 0)
            {
                Pair<State> pair = todo.Dequeue();
                if (R.Contains(pair))
                    continue;
                if (pair.Left.IsFinalState != pair.Right.IsFinalState)
                    throw new Exception("Relations are not bisimilar!");
                foreach(var label in P.Labels)
                    todo.Enqueue(new Pair<State>(pair.Left.Transitions[label], pair.Right.Transitions[label]));
                R.Add(pair);
            }

            return R.ToList();
        }
    }
}
