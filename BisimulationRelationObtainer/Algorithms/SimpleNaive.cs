﻿using BisimulationRelationObtainer.Helpers;
using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainer.Algorithms
{
    public class SimpleNaive : BaseObtainer
    {
        public override List<Pair<DFAState>> ObtainRelation(DFAProcess P, DFAProcess Q)
        {
            HashSet<Pair<DFAState>> R = new HashSet<Pair<DFAState>>();
            Queue<Pair<DFAState>> todo = InitializeTodoQueue(P, Q);

            while (todo.Count > 0)
            {
                Pair<DFAState> pair = todo.Dequeue();
                if (R.Contains(pair))
                    continue;
                if (pair.Left.IsFinalState != pair.Right.IsFinalState)
                    throw new Exception("Relations are not bisimilar!");
                foreach(var label in P.Labels)
                    todo.Enqueue(new Pair<DFAState>(pair.Left.Transitions[label], pair.Right.Transitions[label]));
                R.Add(pair);
            }

            return R.ToList();
        }
    }
}
