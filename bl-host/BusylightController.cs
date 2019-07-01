using System;
using System.Threading.Tasks;
using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Constants;
using Unosquare.Labs.EmbedIO.Modules;

namespace bl_host
{
    public class BusylightController : WebApiController
    {
        // private readonly SDK busylight;


        public BusylightController(IHttpContext context) : base(context)
        {
            //busylight = new SDK();
        }


        [WebApiHandler(HttpVerbs.Post, "/action/")]
        public async Task<bool> RunAction()
        {
            var content = await HttpContext.RequestBodyAsync();
            var action = BusylightAction.FromJson(content);
            var result = RunAction(action);
            return await Ok(result);
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