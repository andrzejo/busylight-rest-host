using System;
using bl_host;
using Busylight;
using NUnit.Framework;

namespace bl_host_unit
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ShouldCreateActionFromJson()
        {
            //given
            String Json = @"{""action"":""color"",""parameters"":[{""Key"":""color"",""Value"":""#ff0000""}]}";

            //when
            var busylightAction = BusylightAction.FromJson(Json);

            //then
            Assert.AreEqual("color", busylightAction.GetAction());
            Assert.AreEqual("#ff0000", busylightAction.GetParam("color"));
            var color = busylightAction.GetColorParam();
            Assert.AreEqual(BusylightColor.Red.RedRgbValue, color.RedRgbValue);
            Assert.AreEqual(BusylightColor.Red.GreenRgbValue, color.GreenRgbValue);
            Assert.AreEqual(BusylightColor.Red.BlueRgbValue, color.BlueRgbValue);
        }
    }
}