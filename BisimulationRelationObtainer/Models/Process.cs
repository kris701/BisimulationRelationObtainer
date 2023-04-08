using BisimulationRelationObtainer.Helpers;
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
            var parsed = ProcessHelper.ParseProcessFile(file);
            ProcessHelper.IsProcessValid(parsed);
            Labels = parsed.Labels;
            States = parsed.States;
        }
    }
}
