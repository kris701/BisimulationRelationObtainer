using BisimulationRelationObtainer.Algorithms;

namespace BisimulationRelationObtainerTests
{
    [TestClass]
    public class IObtainerFunctionalityTests
    {
        [TestMethod]
        public void Can_SimpleNaive_GiveCorrectRelation()
        {
            var data = TestRelations.GetTestSets_Naive();

            foreach (var dataSet in data)
            {
                // ARRANGE
                IObtainer obtainer = new SimpleNaive();

                // ACT
                var result = obtainer.ObtainRelation(dataSet.Item1, dataSet.Item2);
                string resultStr = "";
                foreach (var item in result)
                    resultStr += item.ToString();

                // ASSERT
                Assert.AreEqual(dataSet.Item3, resultStr);
            }
        }

        [TestMethod]
        public void Can_HopcroftAndKarp_GiveCorrectRelation()
        {
            var data = TestRelations.GetTestSets_HopcroftAndKarp();

            foreach (var dataSet in data)
            {
                // ARRANGE
                IObtainer obtainer = new HopcroftAndKarp();

                // ACT
                var result = obtainer.ObtainRelation(dataSet.Item1, dataSet.Item2);
                string resultStr = "";
                foreach (var item in result)
                    resultStr += item.ToString();

                // ASSERT
                Assert.AreEqual(dataSet.Item3, resultStr);
            }
        }
    }
}