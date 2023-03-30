using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Models
{
    public class State
    {
        public string Name { get; set; }
        public Dictionary<string, State> Transitions { get; set; }
        public bool IsFinalState { get; set; }
        public bool IsInitialState { get; set; }

        public State(string name, Dictionary<string, State> transitions, bool isFinalState = false, bool isInitialState = false)
        {
            Name = name;
            Transitions = transitions;
            IsFinalState = isFinalState;
            IsInitialState = isInitialState;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public override bool Equals(object? obj)
        {
            if (obj is State state)
                return state.Name == Name;
            return false;
        }

        public override string? ToString()
        {
            var output = $"{Name}";
            if (IsFinalState)
                output += " [IsFinal]";
            if (IsInitialState)
                output += " [IsInit]";
            return output;
        }
    }
}
