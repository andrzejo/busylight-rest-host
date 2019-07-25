using System.Runtime.Serialization;

namespace BusylightRestHost.Action
{
    [DataContract]
    public class VersionTo
    {
        [DataMember(Name = "version")] private string _version;

        public VersionTo(string version)
        {
            _version = version;
        }

        public string GetVersion()
        {
            return _version;
        }
    }
}