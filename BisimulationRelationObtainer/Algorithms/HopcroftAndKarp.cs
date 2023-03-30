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
                if (R.Contains(pair) || UnionAllClosures(P, Q, R).Contains(pair))
                    continue;
                if (pair.Left.IsFinalState != pair.Right.IsFinalState)
                    throw new Exception("Relations are not bisimilar!");
                foreach(var label in P.Labels)
                    todo.Enqueue(new Pair<State>(pair.Left.Transitions[label], pair.Right.Transitions[label]));
                R.Add(pair);
            }

            return R.ToList();
        }

        private HashSet<Pair<State>> GetSymmetricClosure(HashSet<Pair<State>> states)
        {
            var symmetricStates = new HashSet<Pair<State>>();
            foreach (var state in states)
                if (!state.Left.Equals(state.Right))
                    symmetricStates.Add(new Pair<State>(state.Right, state.Left));
            return symmetricStates;
        }

        private HashSet<Pair<State>> GetReflexiveClosure(Process P, Process Q)
        {
            var reflectiveStates = new HashSet<Pair<State>>();
            foreach (var state in P.States)
                reflectiveStates.Add(new Pair<State>(state, state));
            foreach (var state in Q.States)
                reflectiveStates.Add(new Pair<State>(state, state));
            return reflectiveStates;
        }

        private HashSet<Pair<State>> GetTransitiveClosure(HashSet<Pair<State>> states, List<string> labels)
        {
            var transitiveStates = new HashSet<Pair<State>>();

            foreach (var pairA in states)
            {
                foreach (var pairB in states)
                {
                    if (!pairA.Equals(pairB))
                        if (CanReach(pairA, pairB, labels, new HashSet<Pair<State>>(){ pairA }, 0))
                            transitiveStates.Add(new Pair<State>(pairA.Left, pairB.Right));
                }
            }

            return transitiveStates;
        }

        private bool CanReach(Pair<State> from, Pair<State> to, List<string> labels, HashSet<Pair<State>> visited, int depth)
        {
            depth++;
            foreach (var label in labels)
            {
                var tempPair = new Pair<State>(from.Left.Transitions[label], from.Right.Transitions[label]);
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

        private HashSet<Pair<State>> UnionAllClosures(Process P, Process Q, HashSet<Pair<State>> states)
        {
            HashSet<Pair<State>> newStates = new HashSet<Pair<State>>(states);

            foreach (var state in GetSymmetricClosure(newStates))
                newStates.Add(state);
            foreach (var state in GetTransitiveClosure(newStates, P.Labels))
                newStates.Add(state);
            foreach (var state in GetSymmetricClosure(newStates))
                newStates.Add(state);
            foreach (var state in GetReflexiveClosure(P, Q))
                newStates.Add(state);

            return newStates;
        }
    }
}
