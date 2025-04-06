using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace CasCap.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var svc = new DITestService();
            Assert.IsTrue(svc.GetIntValues().Count > 0);
        }
    }
}