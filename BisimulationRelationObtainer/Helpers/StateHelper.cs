using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Helpers
{
    public static class StateHelper
    {
        public static DFAState GetInitState(List<DFAState> states)
        {
            foreach (var state in states)
                if (state.IsInitialState)
                    return state;
            throw new Exception("State list contains no initial state!");
        }
    }
}
