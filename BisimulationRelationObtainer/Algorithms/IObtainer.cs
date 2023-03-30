﻿using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Algorithms
{
    public interface IObtainer
    {
        public List<Pair<State>> ObtainRelation(Process P, Process Q); 
    }
}
