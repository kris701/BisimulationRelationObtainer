using BisimulationRelationObtainer.Algorithms;
using BisimulationRelationObtainer.Models;
using System;
using CommandLine;

namespace BisimulationRelationObtainer
{
    internal class Program
    {
        public class Options
        {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            [Option('p', "pprocess", Required = true, HelpText = "Path to the P process file")]
            public string PProcess { get; set; }
            [Option('q', "qprocess", Required = true, HelpText = "Path to the q process file")]
            public string QProcess { get; set; }
            [Option('o', "obtainer", Required = true, HelpText = "What obtainer to use. [Naive, HopcroftKarp]", Default = "Naive")]
            public string Obtainer { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        }

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed(RunOptions)
              .WithNotParsed(HandleParseError);
        }

        static void RunOptions(Options opts)
        {
            if (!File.Exists(opts.PProcess))
                throw new FileNotFoundException("The P process file was not found!");
            if (!File.Exists(opts.QProcess))
                throw new FileNotFoundException("The Q process file was not found!");

            var P = new Process(opts.PProcess);
            var Q = new Process(opts.QProcess); 

            IObtainer obtainer;
            switch (opts.Obtainer.ToUpper())
            {
                case "NAIVE": obtainer = new SimpleNaive(); break;
                case "HOPCROFTKARP": obtainer = new HopcroftAndKarp(); break;
                default:
                    throw new Exception("Obtainer not found!");
            }

            Console.WriteLine("Finding relation set...");
            var R = obtainer.ObtainRelation(P, Q);
            Console.WriteLine("Done!");

            Console.WriteLine($"Items: {R.Count}");
            int counter = 1;
            foreach (var item in R)
            {
                Console.WriteLine($"{counter}: {item}");
                counter++;
            }
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var error in errs)
                Console.WriteLine(error);
        }

    }
}