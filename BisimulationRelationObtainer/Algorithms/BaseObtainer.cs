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

        internal static bool DoesLabelsMatch(DFAProcess P, DFAProcess Q)
        {
            foreach (var label in P.Labels)
                if (!Q.Labels.Contains(label))
                    return false;
            return true;
        }

        internal static DFAState GetInitState(Dictionary<Set<string>, DFAState> states)
        {
            foreach (var state in states.Values)
                if (state.IsInitialState)
                    return state;
            throw new Exception("State list contains no initial state!");
        }

        internal static Queue<Pair<DFAState>> InitializeTodoQueue(DFAProcess P, DFAProcess Q)
        {
            Queue<Pair<DFAState>> todo = new Queue<Pair<DFAState>>();

            if (!DoesLabelsMatch(P, Q))
                throw new Exception("Process labels did not match!");

            DFAState p0 = GetInitState(P.States);
            DFAState q0 = GetInitState(Q.States);

            todo.Enqueue(new Pair<DFAState>(p0, q0));

            return todo;
        }

        internal static HashSet<Pair<DFAState>> GetTransitiveClosure(HashSet<Pair<DFAState>> states)
        {
            HashSet<Pair<DFAState>> newStates = new HashSet<Pair<DFAState>>(states);

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
                                new DFAState(new Set<string>(stateA.Left.Name)),
                                new DFAState(new Set<string>(stateB.Right.Name))
                                ));
                    }
                }

                foreach (var state in tempStates)
                    newStates.Add(state);
            }

            return newStates;
        }

        internal static HashSet<Pair<DFAState>> GetSymmetricClosure(HashSet<Pair<DFAState>> states)
        {
            var newStates = new HashSet<Pair<DFAState>>(states);

            foreach (var state in states)
                if (!state.Left.Equals(state.Right))
                    newStates.Add(new Pair<DFAState>(state.Right, state.Left));
            return newStates;
        }

        internal static HashSet<Pair<DFAState>> GetReflexiveClosure(HashSet<Pair<DFAState>> states, DFAProcess P, DFAProcess Q)
        {
            foreach (var state in P.States.Values)
                states.Add(new Pair<DFAState>(state, state));
            foreach (var state in Q.States.Values)
                states.Add(new Pair<DFAState>(state, state));
            return states;
        }
    }
}
