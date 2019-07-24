using NUnit.Framework;
using static BusylightRestHost.Action.ActionData;

namespace BusylightRestHost.Tests.Unit.Action
{
    [TestFixture]
    public class ActionDataTest
    {
        [Test]
        public void ShouldCreateActionFromJson()
        {
            //given
            const string json = @"{""action"":""color"",""parameters"":[{""Key"":""color"",""Value"":""#ff0000""}]}";

            //when
            var action = FromJson(json);

            //then
            Assert.AreEqual("color", action.GetAction());
            Assert.AreEqual("#ff0000", action.GetParameters().GetValue("color"));
        }
    }
}