using System;
using System.Threading.Tasks;

namespace BusylightRestHost
{
    public class BusylightController
    {
        // private readonly SDK busylight;


        public BusylightController()
        {
            //busylight = new SDK();
        }

        public string RunAction(BusylightAction action)
        {
            switch (action.GetAction())
            {
                case "version":
                    return @"{""version"":""1.0""}";
                case "color":
                    //busylight.Light(action.GetColorParam());
                    break;
                case "alert":
                    //busylight.Alert(action.GetColorParam(), );
                    break;
                case "blink":
                    //busylight.Light(action.GetColorParam());
                    break;
                case "off":
                    //busylight.Light(BusylightColor.Off);
                    break;
                default:
                    throw new ArgumentException(@"Invalid action " + action.GetAction());
            }

            return "";
        }
    }
}