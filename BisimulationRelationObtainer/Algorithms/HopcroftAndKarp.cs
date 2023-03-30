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
            HashSet<Pair<State>> todo = new HashSet<Pair<State>>();

            if (!DoesLabelsMatch(P,Q))
                throw new Exception("Process labels did not match!");

            State p0 = StateHelper.GetInitState(P.States);
            State q0 = StateHelper.GetInitState(Q.States);

            todo.Add(new Pair<State>(p0,q0));

            while (todo.Count > 0)
            {
                Pair<State> pair = todo.First();
                todo.Remove(pair);
                if (GetClosures(R, P, Q).Contains(pair))
                    continue;
                if (pair.Left.IsFinalState != pair.Right.IsFinalState)
                    throw new Exception("Relations are not bisimilar!");
                foreach(var label in P.Labels)
                    todo.Add(new Pair<State>(pair.Left.Transitions[label], pair.Right.Transitions[label]));
                R.Add(pair);
            }

            return R.ToList();
        }

        private HashSet<Pair<State>> GetClosures(HashSet<Pair<State>> states, Process P, Process Q)
        {
            HashSet<Pair<State>> newStates = new HashSet<Pair<State>>();

            // Symmetric
            HashSet<Pair<State>> symetricStates = SymmetricClosure(states);

            // Reflective
            HashSet<Pair<State>> reflectiveStates = ReflexiveClosure(P, Q);

            // Transitive
            HashSet<Pair<State>> transitiveStates = TransitiveClosure(P, Q);

            // Combine it all
            foreach (var state in states)
                newStates.Add(state);
            foreach (var state in symetricStates)
                newStates.Add(state);
            foreach (var state in reflectiveStates)
                newStates.Add(state);
            foreach (var state in transitiveStates)
                newStates.Add(state);

            return newStates;
        }
        private HashSet<Pair<State>> SymmetricClosure(HashSet<Pair<State>> states)
        {
            HashSet<Pair<State>> newStates = new HashSet<Pair<State>>();
            foreach (var state in states)
                newStates.Add(new Pair<State>(state.Right, state.Left));
            return newStates;
        }

        private HashSet<Pair<State>> ReflexiveClosure(Process P, Process Q)
        {
            HashSet<Pair<State>> newStates = new HashSet<Pair<State>>();
            foreach (var state in P.States)
                newStates.Add(new Pair<State>(state, state));
            foreach (var state in Q.States)
                newStates.Add(new Pair<State>(state, state));
            return newStates;
        }

        private HashSet<Pair<State>> TransitiveClosure(Process P, Process Q)
        {
            HashSet<Pair<State>> newStates = new HashSet<Pair<State>>();
            foreach (var state in P.States)
                newStates.Add(new Pair<State>(state, state));
            foreach (var state in Q.States)
                newStates.Add(new Pair<State>(state, state));
            return newStates;
        }
    }
}
