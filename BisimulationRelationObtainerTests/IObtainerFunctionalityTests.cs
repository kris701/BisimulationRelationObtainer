using BisimulationRelationObtainer.Algorithms;
using BisimulationRelationObtainer.Models;

namespace BisimulationRelationObtainerTests
{
    [TestClass]
    public class IObtainerFunctionalityTests
    {
        public static IEnumerable<object[]> GetTestSets_Naive()
        {
            yield return new object[] {
                new DFAProcess("Examples/Example1/s.txt"),
                new DFAProcess("Examples/Example1/t.txt"),
                "[s0 [IsInit]] [t0 [IsInit]][s1] [t1][ø] [ø][s0s2 [IsFinal]] [t1t2 [IsFinal]][s1s2 [IsFinal]] [t1t2 [IsFinal]][s2 [IsFinal]] [t0t2 [IsFinal]][s0s2 [IsFinal]] [t0t2 [IsFinal]][s2 [IsFinal]] [t1t2 [IsFinal]][s2 [IsFinal]] [t2 [IsFinal]]"
            };
            yield return new object[] {
                new DFAProcess("Examples/Example1/s.txt"),
                new DFAProcess("Examples/Example1/s.txt"),
                "[s0 [IsInit]] [s0 [IsInit]][s1] [s1][ø] [ø][s0s2 [IsFinal]] [s0s2 [IsFinal]][s1s2 [IsFinal]] [s1s2 [IsFinal]][s2 [IsFinal]] [s2 [IsFinal]]"
            };
            yield return new object[] {
                new DFAProcess("Examples/Example1/t.txt"),
                new DFAProcess("Examples/Example1/t.txt"),
                "[t0 [IsInit]] [t0 [IsInit]][t1] [t1][ø] [ø][t1t2 [IsFinal]] [t1t2 [IsFinal]][t0t2 [IsFinal]] [t0t2 [IsFinal]][t2 [IsFinal]] [t2 [IsFinal]]"
            };

            yield return new object[] {
                new DFAProcess("Examples/Example2/s.txt"),
                new DFAProcess("Examples/Example2/t.txt"),
                "[s0 [IsInit]] [t0 [IsInit]][s0s2 [IsFinal]] [t1 [IsFinal]][s1 [IsFinal]] [t1 [IsFinal]][s1s2 [IsFinal]] [t1 [IsFinal]][s2 [IsFinal]] [t1 [IsFinal]]"
            };
            yield return new object[] {
                new DFAProcess("Examples/Example2/s.txt"),
                new DFAProcess("Examples/Example2/s.txt"),
                "[s0 [IsInit]] [s0 [IsInit]][s0s2 [IsFinal]] [s0s2 [IsFinal]][s1 [IsFinal]] [s1 [IsFinal]][s1s2 [IsFinal]] [s1s2 [IsFinal]][s2 [IsFinal]] [s2 [IsFinal]]"
            };
            yield return new object[] {
                new DFAProcess("Examples/Example2/t.txt"),
                new DFAProcess("Examples/Example2/t.txt"),
                "[t0 [IsInit]] [t0 [IsInit]][t1 [IsFinal]] [t1 [IsFinal]]"
            };
        }
        public static IEnumerable<object[]> GetTestSets_HopcroftAndKarp()
        {
            yield return new object[] {
                new DFAProcess("Examples/Example1/s.txt"),
                new DFAProcess("Examples/Example1/t.txt"),
                "[s0 [IsInit]] [t0 [IsInit]][s1] [t1][s0s2 [IsFinal]] [t1t2 [IsFinal]][s1s2 [IsFinal]] [t1t2 [IsFinal]][s2 [IsFinal]] [t0t2 [IsFinal]][s0s2 [IsFinal]] [t0t2 [IsFinal]][s2 [IsFinal]] [t2 [IsFinal]]"
            };
            yield return new object[] {
                new DFAProcess("Examples/Example1/s.txt"),
                new DFAProcess("Examples/Example1/s.txt"),
                ""
            };
            yield return new object[] {
                new DFAProcess("Examples/Example1/t.txt"),
                new DFAProcess("Examples/Example1/t.txt"),
                ""
            };

            yield return new object[] {
                new DFAProcess("Examples/Example2/s.txt"),
                new DFAProcess("Examples/Example2/t.txt"),
                "[s0 [IsInit]] [t0 [IsInit]][s0s2 [IsFinal]] [t1 [IsFinal]][s1 [IsFinal]] [t1 [IsFinal]][s1s2 [IsFinal]] [t1 [IsFinal]][s2 [IsFinal]] [t1 [IsFinal]]"
            };
            yield return new object[] {
                new DFAProcess("Examples/Example2/s.txt"),
                new DFAProcess("Examples/Example2/s.txt"),
                ""
            };
            yield return new object[] {
                new DFAProcess("Examples/Example2/t.txt"),
                new DFAProcess("Examples/Example2/t.txt"),
                ""
            };
        }


        [TestMethod]
        [DynamicData(nameof(GetTestSets_Naive), DynamicDataSourceType.Method)]
        public void Can_SimpleNaive_GiveCorrectRelation(DFAProcess P, DFAProcess Q, string expected)
        {
            // ARRANGE
            IObtainer obtainer = new SimpleNaive();

            // ACT
            var result = obtainer.ObtainRelation(P, Q);
            string resultStr = "";
            foreach (var item in result)
                resultStr += item.ToString();

            // ASSERT
            Assert.AreEqual(expected, resultStr);
        }

        [TestMethod]
        [DynamicData(nameof(GetTestSets_HopcroftAndKarp), DynamicDataSourceType.Method)]
        public void Can_HopcroftAndKarp_GiveCorrectRelation(DFAProcess P, DFAProcess Q, string expected)
        {
            // ARRANGE
            IObtainer obtainer = new HopcroftAndKarp();

            // ACT
            var result = obtainer.ObtainRelation(P, Q);
            string resultStr = "";
            foreach (var item in result)
                resultStr += item.ToString();

            // ASSERT
            Assert.AreEqual(expected, resultStr);
        }
    }
}