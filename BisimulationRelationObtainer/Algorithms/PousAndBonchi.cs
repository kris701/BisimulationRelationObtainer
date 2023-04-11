using BisimulationRelationObtainer.Helpers;
using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Algorithms
{
    public class PousAndBonchi : BaseObtainer
    {
        public override List<Pair<DFAState>> ObtainRelation(DFAProcess P, DFAProcess Q)
        {
            HashSet<Pair<DFAState>> R = new HashSet<Pair<DFAState>>();
            Queue<Pair<DFAState>> todo = InitializeTodoQueue(P, Q);

            while (todo.Count > 0)
            {
                Pair<DFAState> pair = todo.Dequeue();
                if (ContextualUnion(todo, R).Contains(pair))
                    continue;
                if (pair.Left.IsFinalState != pair.Right.IsFinalState)
                    throw new Exception("Relations are not bisimilar!");
                foreach(var label in P.Labels)
                    todo.Enqueue(new Pair<DFAState>(pair.Left.Transitions[label], pair.Right.Transitions[label]));
                R.Add(pair);
            }

            return R.ToList();
        }

        private HashSet<Pair<DFAState>> GetInitializingUnion(Queue<Pair<DFAState>> todo, HashSet<Pair<DFAState>> R)
        {
            HashSet<Pair<DFAState>> newStates = new HashSet<Pair<DFAState>>();
            foreach (var state in todo)
                newStates.Add(state);
            foreach (var state in R)
                newStates.Add(state);
            return newStates;
        }

        private HashSet<Pair<DFAState>> GetFullUnionOfStates(HashSet<Pair<DFAState>> states)
        {
            HashSet<Pair<DFAState>> newStates = new HashSet<Pair<DFAState>>();
            int skip = 1;
            foreach (var stateA in states)
            {
                foreach (var stateB in states.Skip(skip))
                {
                    newStates.Add(new Pair<DFAState>(
                        new DFAState(new Set(stateA.Left.Name, stateB.Left.Name), new Dictionary<string, DFAState>()),
                        new DFAState(new Set(stateA.Right.Name, stateB.Right.Name), new Dictionary<string, DFAState>())
                        ));
                }
                skip++;
            }

            foreach (var state in states)
                newStates.Add(state);

            return newStates;
        }

        private HashSet<Pair<DFAState>> ContextualUnion(Queue<Pair<DFAState>> todo, HashSet<Pair<DFAState>> R)
        {
            HashSet<Pair<DFAState>> initialStates = GetInitializingUnion(todo, R);
            HashSet<Pair<DFAState>> unionStates = GetFullUnionOfStates(initialStates);
            HashSet<Pair<DFAState>> symmetricStates = GetSymmetricClosure(unionStates);
            HashSet<Pair<DFAState>> returnStates = GetTransitiveClosure(symmetricStates);

            return returnStates;
        }
    }
}
