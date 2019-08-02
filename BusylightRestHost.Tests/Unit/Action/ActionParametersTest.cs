using System.Collections.Generic;
using Busylight;
using BusylightRestHost.Action;
using NUnit.Framework;

namespace BusylightRestHost.Tests.Unit.Action
{
    [TestFixture]
    public class ActionParametersTest
    {
        [Test]
        public void ShouldGetColorFromParameters()
        {
            //given
            var parameters = new Dictionary<string, string>
            {
                {"color", "#ff0000"}
            };
            var actionParameters = new ActionParameters(parameters);

            //when
            var color = actionParameters.GetColor();

            //then
            Assert.AreEqual(BusylightColor.Red.RedRgbValue, color.RedRgbValue);
            Assert.AreEqual(BusylightColor.Red.GreenRgbValue, color.GreenRgbValue);
            Assert.AreEqual(BusylightColor.Red.BlueRgbValue, color.BlueRgbValue);
        }

        [Test]
        public void ShouldGetSoundFromParameters()
        {
            //given
            var parameters = new Dictionary<string, string>
            {
                {"sound", "OpenOffice"}
            };
            var actionParameters = new ActionParameters(parameters);

            //when
            var sound = actionParameters.GetSound();

            //then
            Assert.AreEqual(BusylightSoundClip.OpenOffice, sound);
        }

        [Test]
        public void ShouldGetVolumeFromParameters()
        {
            //given
            var parameters = new Dictionary<string, string>
            {
                {"volume", "Max"}
            };
            var actionParameters = new ActionParameters(parameters);

            //when
            var volume = actionParameters.GetVolume();

            //then
            Assert.AreEqual(BusylightVolume.Max, volume);
        }
    }
}