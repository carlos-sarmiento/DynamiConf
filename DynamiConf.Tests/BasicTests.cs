using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamiConf.Tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void GetEmptyConfiguration()
        {
            var config = new DynamiConfiguration().GetConfiguration();
            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void GetConfigurationFromJsonString()
        {

        }
    }
}
