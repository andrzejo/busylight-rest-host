using BusylightRestHost.Action;
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
            IAction action = new VersionAction(null, null);

            //when
            var json = action.Execute();

            //then
            Assert.AreEqual("{\"version\":\"dev\"}", json);
        }
    }
}