using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Models
{
    public class Process
    {
        public List<string> Labels { get; }
        public List<State> States { get; }

        public Process(List<State> states, List<string> labels)
        {
            States = states;
            Labels = labels;
        }

        public Process(string file)
        {
            var states = new Dictionary<string,State>();
            var labels = new List<string>();

            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                if (line != "")
                {
                    if (!line.Trim().StartsWith("//"))
                    {
                        if (line.Contains("{") && line.Contains("}"))
                        {
                            var labelString = line.Trim().Replace("{", "").Replace("}", "").Replace(" ", "");
                            labels = labelString.Split(",").ToList();
                        }
                        else if (line.Contains("[") && line.Contains("]"))
                        {
                            var stateDefString = line.Trim().Replace("[", "").Replace("]", "").Replace(" ", "");
                            var name = stateDefString.Split(":")[0];
                            states.Add(name, new State(
                                name,
                                new Dictionary<string, State>(),
                                stateDefString.ToUpper().Contains("ISFINAL"),
                                stateDefString.ToUpper().Contains("ISINIT")));
                        }
                        else if (line.Contains("(") && line.Contains(")"))
                        {
                            var transitionString = line.Trim().Replace("(", "").Replace(")", "");
                            var transitionSteps = transitionString.Split(" ");
                            var fromState = transitionSteps[0];
                            var label = transitionSteps[1];
                            var toState = transitionSteps[2];

                            states[fromState].Transitions.Add(label, states[toState]);
                        }
                    }
                }
            }

            States = states.Values.ToList();
            Labels = labels;
        }
    }
}
