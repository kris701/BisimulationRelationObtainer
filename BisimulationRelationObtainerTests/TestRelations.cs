using BisimulationRelationObtainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisimulationRelationObtainerTests
{
    public static class TestRelations
    {
        public static List<Tuple<Process, Process, string>> GetTestSets_Naive()
        {
            List<Tuple<Process, Process, string>> returnList = new List<Tuple<Process, Process, string>>();

            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example1/s.txt"),
                new Process("Examples/Example1/t.txt"),
                "[s0 [IsInit]] [t0 [IsInit]][s1] [t1][Ø] [Ø][s0s2 [IsFinal]] [t1t2 [IsFinal]][s1s2 [IsFinal]] [t1t2 [IsFinal]][s2 [IsFinal]] [t0t2 [IsFinal]][s0s2 [IsFinal]] [t0t2 [IsFinal]][s2 [IsFinal]] [t1t2 [IsFinal]][s2 [IsFinal]] [t2 [IsFinal]]"));
            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example1/s.txt"),
                new Process("Examples/Example1/s.txt"),
                "[s0 [IsInit]] [s0 [IsInit]][s1] [s1][Ø] [Ø][s0s2 [IsFinal]] [s0s2 [IsFinal]][s1s2 [IsFinal]] [s1s2 [IsFinal]][s2 [IsFinal]] [s2 [IsFinal]]"));
            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example1/t.txt"),
                new Process("Examples/Example1/t.txt"),
                "[t0 [IsInit]] [t0 [IsInit]][t1] [t1][Ø] [Ø][t1t2 [IsFinal]] [t1t2 [IsFinal]][t0t2 [IsFinal]] [t0t2 [IsFinal]][t2 [IsFinal]] [t2 [IsFinal]]"));

            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example2/s.txt"),
                new Process("Examples/Example2/t.txt"),
                "[s0 [IsInit]] [t0 [IsInit]][s0s2 [IsFinal]] [t1 [IsFinal]][s1 [IsFinal]] [t1 [IsFinal]][s1s2 [IsFinal]] [t1 [IsFinal]][s2 [IsFinal]] [t1 [IsFinal]]"));
            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example2/s.txt"),
                new Process("Examples/Example2/s.txt"),
                "[s0 [IsInit]] [s0 [IsInit]][s0s2 [IsFinal]] [s0s2 [IsFinal]][s1 [IsFinal]] [s1 [IsFinal]][s1s2 [IsFinal]] [s1s2 [IsFinal]][s2 [IsFinal]] [s2 [IsFinal]]"));
            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example2/t.txt"),
                new Process("Examples/Example2/t.txt"),
                "[t0 [IsInit]] [t0 [IsInit]][t1 [IsFinal]] [t1 [IsFinal]]"));

            return returnList;
        }

        public static List<Tuple<Process, Process, string>> GetTestSets_HopcroftAndKarp()
        {
            List<Tuple<Process, Process, string>> returnList = new List<Tuple<Process, Process, string>>();

            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example1/s.txt"),
                new Process("Examples/Example1/t.txt"),
                "[s0 [IsInit]] [t0 [IsInit]][s1] [t1][s0s2 [IsFinal]] [t1t2 [IsFinal]][s1s2 [IsFinal]] [t1t2 [IsFinal]][s2 [IsFinal]] [t0t2 [IsFinal]][s0s2 [IsFinal]] [t0t2 [IsFinal]][s2 [IsFinal]] [t2 [IsFinal]]"));
            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example1/s.txt"),
                new Process("Examples/Example1/s.txt"),
                ""));
            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example1/t.txt"),
                new Process("Examples/Example1/t.txt"),
                ""));

            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example2/s.txt"),
                new Process("Examples/Example2/t.txt"),
                "[s0 [IsInit]] [t0 [IsInit]][s0s2 [IsFinal]] [t1 [IsFinal]][s1 [IsFinal]] [t1 [IsFinal]][s1s2 [IsFinal]] [t1 [IsFinal]][s2 [IsFinal]] [t1 [IsFinal]]"));
            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example2/s.txt"),
                new Process("Examples/Example2/s.txt"),
                ""));
            returnList.Add(new Tuple<Process, Process, string>(
                new Process("Examples/Example2/t.txt"),
                new Process("Examples/Example2/t.txt"),
                ""));

            return returnList;
        }
    }
}
