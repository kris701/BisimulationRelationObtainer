using BisimulationRelationObtainer.Helpers;
using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Algorithms
{
    public class HopcroftAndKarp : BaseObtainer
    {
        public override List<Pair<DFAState>> ObtainRelation(DFAProcess P, DFAProcess Q)
        {
            HashSet<Pair<DFAState>> R = new HashSet<Pair<DFAState>>();
            Queue<Pair<DFAState>> todo = new Queue<Pair<DFAState>>();

            if (!DoesLabelsMatch(P,Q))
                throw new Exception("Process labels did not match!");

            DFAState p0 = StateHelper.GetInitState(P.States);
            DFAState q0 = StateHelper.GetInitState(Q.States);

            todo.Enqueue(new Pair<DFAState>(p0,q0));

            while (todo.Count > 0)
            {
                Pair<DFAState> pair = todo.Dequeue();
                if (R.Contains(pair) || UnionAllClosures(P, Q, R).Contains(pair))
                    continue;
                if (pair.Left.IsFinalState != pair.Right.IsFinalState)
                    throw new Exception("Relations are not bisimilar!");
                foreach(var label in P.Labels)
                    todo.Enqueue(new Pair<DFAState>(pair.Left.Transitions[label], pair.Right.Transitions[label]));
                R.Add(pair);
            }

            return R.ToList();
        }

        private HashSet<Pair<DFAState>> GetSymmetricClosure(HashSet<Pair<DFAState>> states)
        {
            var symmetricStates = new HashSet<Pair<DFAState>>();
            foreach (var state in states)
                if (!state.Left.Equals(state.Right))
                    symmetricStates.Add(new Pair<DFAState>(state.Right, state.Left));
            return symmetricStates;
        }

        private HashSet<Pair<DFAState>> GetReflexiveClosure(DFAProcess P, DFAProcess Q)
        {
            var reflectiveStates = new HashSet<Pair<DFAState>>();
            foreach (var state in P.States)
                reflectiveStates.Add(new Pair<DFAState>(state, state));
            foreach (var state in Q.States)
                reflectiveStates.Add(new Pair<DFAState>(state, state));
            return reflectiveStates;
        }

        private HashSet<Pair<DFAState>> GetTransitiveClosure(HashSet<Pair<DFAState>> states, List<string> labels)
        {
            var transitiveStates = new HashSet<Pair<DFAState>>();

            foreach (var pairA in states)
            {
                foreach (var pairB in states)
                {
                    if (!pairA.Equals(pairB))
                        if (CanReach(pairA, pairB, labels, new HashSet<Pair<DFAState>>(){ pairA }, 0))
                            transitiveStates.Add(new Pair<DFAState>(pairA.Left, pairB.Right));
                }
            }

            return transitiveStates;
        }

        private bool CanReach(Pair<DFAState> from, Pair<DFAState> to, List<string> labels, HashSet<Pair<DFAState>> visited, int depth)
        {
            depth++;
            foreach (var label in labels)
            {
                var tempPair = new Pair<DFAState>(from.Left.Transitions[label], from.Right.Transitions[label]);
                if (tempPair.Equals(to))
                    return depth >= 2;
                else
                {
                    if (!visited.Contains(tempPair))
                    {
                        visited.Add(tempPair);
                        if (CanReach(tempPair, to, labels, visited, depth))
                            return true;
                    }
                };
            }
            return false;
        }

        private HashSet<Pair<DFAState>> UnionAllClosures(DFAProcess P, DFAProcess Q, HashSet<Pair<DFAState>> states)
        {
            HashSet<Pair<DFAState>> newStates = new HashSet<Pair<DFAState>>(states);

            foreach (var state in GetSymmetricClosure(newStates))
                newStates.Add(state);
            foreach (var state in GetTransitiveClosure(newStates, P.Labels))
                newStates.Add(state);
            // Need symmetric twice, since we also want the symmetric closure from the transitive closure.
            foreach (var state in GetSymmetricClosure(newStates))
                newStates.Add(state);
            foreach (var state in GetReflexiveClosure(P, Q))
                newStates.Add(state);

            return newStates;
        }
    }
}
