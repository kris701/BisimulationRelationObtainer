using BisimulationRelationObtainer.Algorithms;
using BisimulationRelationObtainer.Models;
using System;

namespace BisimulationRelationObtainer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var P = new Process("Examples/s.txt");
            var Q = new Process("Examples/t.txt");

            IObtainer obtainer = new HopcroftAndKarp();

            var R = obtainer.ObtainRelation(P, Q);
            Console.WriteLine($"Items: {R.Count}");
            foreach(var item in R)
                Console.WriteLine(item);
        }
    }
}