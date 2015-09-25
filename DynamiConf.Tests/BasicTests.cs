using System;
using DynamiConf.Providers;
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
            var config = new DynamiConfiguration()
                .MergeWith.JsonString("{ \"key\": \"value\"}")
                .GetConfiguration();

            Assert.IsNotNull(config.key);
        }
    }
}
