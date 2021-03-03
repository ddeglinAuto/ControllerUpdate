using LogginPractice.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogginPractice.Tests
{
    [TestClass]
    [TestCategory("ContactUsPage"), TestCategory("SampleApp2")]
    public class ContactUsFeature : BaseTest
    {
        [TestMethod]
        [Description("")]
        public void TCID2()
        {
            ContactUsPage contactUsPage = new ContactUsPage(Driver);
            contactUsPage.Login();
            Assert.IsTrue(contactUsPage.IsLoaded,
                "The contact us page did not open successfully.");


        }

    }
}
