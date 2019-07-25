using BusylightRestHost.Action;
using BusylightRestHost.Action.List;
using NUnit.Framework;

namespace BusylightRestHost.Tests.Unit.Action
{
    [TestFixture]
    public class VersionActionTest
    {
        [Test]
        public void ShouldReturnVersion()
        {
            //given
            IAction action = new VersionAction("dev-version");

            //when
            var json = action.Execute();

            //then
            Assert.AreEqual("{\"version\":\"dev-version\"}", json);
        }
    }
}