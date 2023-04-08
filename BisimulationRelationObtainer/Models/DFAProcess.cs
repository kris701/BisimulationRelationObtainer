using BisimulationRelationObtainer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Models
{
    public class DFAProcess
    {
        public List<string> Labels { get; }
        public List<DFAState> States { get; }

        public DFAProcess(List<DFAState> states, List<string> labels)
        {
            States = states;
            Labels = labels;
        }

        public DFAProcess(string file)
        {
            var parsed = ProcessHelper.ParseProcessFile(file);
            ProcessHelper.IsProcessValid(parsed);
            Labels = parsed.Labels;
            States = parsed.States;
        }
    }
}
