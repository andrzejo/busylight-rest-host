using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.Serialization.Json;

namespace BusylightRestHost.Action
{
    [DataContract]
    public class ActionData
    {
        [DataMember(Name = "action")] private string _action = "";

        [DataMember(Name = "parameters")]
        private Dictionary<string, string> _parameters = new Dictionary<string, string>();

        public static ActionData FromJson(string actionDataJson)
        {
            var stream = new MemoryStream(Encoding.Unicode.GetBytes(actionDataJson)) {Position = 0};
            var serializer = new DataContractJsonSerializer(typeof(ActionData));
            return (ActionData) serializer.ReadObject(stream);
        }

        public string GetAction()
        {
            return _action;
        }

        public ActionParameters GetParameters()
        {
            return new ActionParameters(_parameters);
        }
    }
}