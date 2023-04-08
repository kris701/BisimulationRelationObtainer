using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Helpers
{
    public static class ProcessHelper
    {
        public static Process ParseProcessFile(string file)
        {
            var states = new Dictionary<string, State>();
            var labels = new List<string>();

            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (!line.Trim().StartsWith("//"))
                    {
                        var toLowerLine = line.ToLower().Trim();
                        if (toLowerLine.Contains("{") && toLowerLine.Contains("}"))
                        {
                            var labelString = toLowerLine.Replace("{", "").Replace("}", "").Replace(" ", "");
                            labels = labelString.Split(",").ToList();
                        }
                        else if (toLowerLine.Contains("[") && toLowerLine.Contains("]"))
                        {
                            var stateDefString = toLowerLine.Replace("[", "").Replace("]", "").Replace(" ", "");
                            var name = stateDefString.Split(":")[0];
                            states.Add(name, new State(
                                name,
                                new Dictionary<string, State>(),
                                stateDefString.ToUpper().Contains("ISFINAL"),
                                stateDefString.ToUpper().Contains("ISINIT")));
                        }
                        else if (toLowerLine.Contains("(") && toLowerLine.Contains(")"))
                        {
                            var transitionString = toLowerLine.Replace("(", "").Replace(")", "");
                            var transitionSteps = transitionString.Split(" ");
                            var fromState = transitionSteps[0];
                            var label = transitionSteps[1];
                            var toState = transitionSteps[2];

                            states[fromState].Transitions.Add(label, states[toState]);
                        }
                    }
                }
            }

            return new Process(states.Values.ToList(), labels);
        }

        public static void IsProcessValid(Process process)
        {
            // Label Transition Check
            foreach (var state in process.States)
                foreach (var key in state.Transitions.Keys)
                    if (!process.Labels.Contains(key))
                        throw new Exception("Transitions contain labels that was not defined!");

            // DFA Transition Check
            foreach (var state in process.States)
                if (process.Labels.Count != state.Transitions.Keys.Count)
                    throw new Exception("All states must have all labels as transitions!");

            // Transition Jump Check
            foreach (var state in process.States)
                foreach (var key in state.Transitions.Keys)
                    if (!process.States.Contains(state.Transitions[key]))
                        throw new Exception("A transition is missing a target state!");

        }
    }
}
