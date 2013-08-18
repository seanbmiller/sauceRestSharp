using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SauceRESTClient;

namespace SauceRestTest
{
    [TestClass]
    public class Test
    {        
        private string username = "";
        private string accessKey = "";
        private string testId = "";

        [TestMethod]
        public void Pass()
        {
            var sauce = new SauceREST(username,accessKey);
            sauce.jobPassed(testId);
            var result = sauce.getJobInfo(testId);
            Assert.IsTrue(result.Contains("\"passed\": true"));
        }

        [TestMethod]
        public void Fail()
        {
            var sauce = new SauceREST(username, accessKey);
            sauce.jobFailed(testId);
            var result = sauce.getJobInfo(testId);
            Assert.IsTrue(result.Contains("\"passed\": false"));
        }
    }
}
