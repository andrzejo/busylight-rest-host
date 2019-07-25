using NUnit.Framework;

namespace BusylightRestHost.Tests.Unit
{
    [TestFixture]
    public class HttpServerTest
    {
        [Test]
        public void ShouldConvertResourceNameToPath()
        {
            const string resourceName = "Sdk.busylight-lib.js";

            //when
            var path = HttpServer.ResourceNameToPath(resourceName);

            //then
            Assert.AreEqual("Sdk/busylight-lib.js", path);
        }

        [Test]
        public void ShouldConvertResourceNameToPath1()
        {
            const string resourceName = "busylight-lib.js";

            //when
            var path = HttpServer.ResourceNameToPath(resourceName);

            //then
            Assert.AreEqual("busylight-lib.js", path);
        }
    }
}