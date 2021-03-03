using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SampleApp2
{
    [TestClass]
    public class BoundariesUpdateTest : BaseTest
    {
        [TestMethod]
        [Description("Verifies that boundary values are updated")]
        public void BoundariesValuesSaved_When_BoundariesAreUpdatedTest()
        {
            
            HomePage homePage = new HomePage(Driver);
            homePage.Login();
            var result = homePage.UpdateBoundaries();
            //Assert.AreEqual(expectedToastMsg, result, $"Expected toast message: '{expectedToastMsg}' not equal to displayed toast message: '{result}'");
            // StringAssert.IsMatch(expectedToastMsg, result, $"Expected toast message: '{expectedToastMsg}' not equal to displayed toast message: '{result}'");
            foreach (var item in result)
                Assert.IsTrue(item.Key, item.Value);
        }
    }
}
