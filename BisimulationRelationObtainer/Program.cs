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

            //IObtainer obtainer = new SimpleNaive();
            IObtainer obtainer = new HopcroftAndKarp();

            var R = obtainer.ObtainRelation(P, Q);
            Console.WriteLine($"Items: {R.Count}");
            int counter = 1;
            foreach (var item in R)
            {
                Console.WriteLine($"{counter}: {item}");
                counter++;
            }
        }
    }
}