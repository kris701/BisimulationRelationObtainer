using BisimulationRelationObtainer.Helpers;
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

        internal bool DoesLabelsMatch(DFAProcess P, DFAProcess Q)
        {
            foreach (var label in P.Labels)
                if (!Q.Labels.Contains(label))
                    return false;
            return true;
        }

        internal Queue<Pair<DFAState>> InitializeTodoQueue(DFAProcess P, DFAProcess Q)
        {
            Queue<Pair<DFAState>> todo = new Queue<Pair<DFAState>>();

            if (!DoesLabelsMatch(P, Q))
                throw new Exception("Process labels did not match!");

            DFAState p0 = StateHelper.GetInitState(P.States);
            DFAState q0 = StateHelper.GetInitState(Q.States);

            todo.Enqueue(new Pair<DFAState>(p0, q0));

            return todo;
        }

        internal HashSet<Pair<DFAState>> GetTransitiveClosure(HashSet<Pair<DFAState>> states)
        {
            HashSet<Pair<DFAState>> newStates = new HashSet<Pair<DFAState>>();
            foreach (var state in states)
                newStates.Add(state);

            int lastSize = -1;
            while (lastSize != newStates.Count)
            {
                lastSize = newStates.Count;
                HashSet<Pair<DFAState>> tempStates = new HashSet<Pair<DFAState>>();
                foreach (var stateA in newStates)
                {
                    foreach (var stateB in newStates)
                    {
                        if (stateA.Right.Name == stateB.Left.Name)
                            tempStates.Add(new Pair<DFAState>(
                                new DFAState(new Set(stateA.Left.Name), new Dictionary<string, DFAState>()),
                                new DFAState(new Set(stateB.Right.Name), new Dictionary<string, DFAState>())
                                ));
                    }
                }

                foreach (var state in tempStates)
                    newStates.Add(state);
            }

            return newStates;
        }

        internal HashSet<Pair<DFAState>> GetSymmetricClosure(HashSet<Pair<DFAState>> states)
        {
            var newStates = new HashSet<Pair<DFAState>>();
            foreach (var state in states)
                newStates.Add(state);

            foreach (var state in states)
                if (!state.Left.Equals(state.Right))
                    newStates.Add(new Pair<DFAState>(state.Right, state.Left));
            return newStates;
        }

        internal HashSet<Pair<DFAState>> GetReflexiveClosure(HashSet<Pair<DFAState>> states, DFAProcess P, DFAProcess Q)
        {
            var newStates = new HashSet<Pair<DFAState>>();
            foreach (var state in states)
                newStates.Add(state);

            foreach (var state in P.States.Values)
                newStates.Add(new Pair<DFAState>(state, state));
            foreach (var state in Q.States.Values)
                newStates.Add(new Pair<DFAState>(state, state));
            return newStates;
        }
    }
}
