using BisimulationRelationObtainer.Algorithms;
using BisimulationRelationObtainer.Models;
using System;
using CommandLine;
using CommandLine.Text;

namespace BisimulationRelationObtainer
{
    internal class Program
    {
        private static ConsoleColor InitStateColor = ConsoleColor.Green;
        private static ConsoleColor FinalStateColor = ConsoleColor.Red;
        private static ConsoleColor BothStateColor = ConsoleColor.Yellow;

        public class Options
        {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            [Option('p', "pprocess", Required = true, HelpText = "Path to the P process file")]
            public string PProcess { get; set; }
            [Option('q', "qprocess", Required = true, HelpText = "Path to the q process file")]
            public string QProcess { get; set; }
            [Option('o', "obtainer", Required = true, HelpText = "What obtainer to use. [Naive, HopcroftKarp]", Default = "Naive")]
            public string Obtainer { get; set; }

            [Usage(ApplicationAlias = "BisimulationRelationObtainer")]
            public static IEnumerable<Example> Examples
            {
                get
                {
                    return new List<Example>() {
                        new Example("Find the bisimulation between two process files with a naive algorithm", new Options { PProcess = "file1.dfa", QProcess = "file2.dfa", Obtainer = "Naive" })
                      };
                }
            }
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

            var P = new DFAProcess(opts.PProcess);
            var Q = new DFAProcess(opts.QProcess); 

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

            PrintColorHelp();

            Console.WriteLine($"The relation is {R.Count} items large, using the {opts.Obtainer} obtainer.");
            int counter = 1;
            foreach (var item in R)
            {
                Console.Write("{0,-3}:", counter);
                Console.Write("(");

                SetConsoleColor(item.Left);
                Console.Write($"{{{item.Left.Name}}}");
                Console.ResetColor();
                Console.Write(", ");
                SetConsoleColor(item.Right);
                Console.Write($"{{{item.Right.Name}}}");
                Console.ResetColor();

                Console.WriteLine(")");
                counter++;
            }
        }

        static void SetConsoleColor(DFAState state) {
            if (state.IsFinalState && state.IsInitialState)
                Console.ForegroundColor = BothStateColor;
            else if (state.IsFinalState)
                Console.ForegroundColor = FinalStateColor;
            else if (state.IsInitialState)
                Console.ForegroundColor = InitStateColor;
            else
                Console.ResetColor();
        }
        
        static void PrintColorHelp()
        {
            Console.ResetColor();
            Console.ForegroundColor = InitStateColor;
            Console.Write("Initial States\t");
            Console.ForegroundColor = FinalStateColor;
            Console.Write("Final States\t");
            Console.ForegroundColor = BothStateColor;
            Console.WriteLine("Both initial and final States\t");
            Console.ResetColor();
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var error in errs)
                Console.WriteLine(error);
        }

    }
}