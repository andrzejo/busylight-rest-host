using System.Runtime.Serialization;

namespace BusylightRestHost.Action
{
    [DataContract]
    public class VersionTo
    {
        [DataMember(Name = "version")] private string _version = Version.Get();

        public string GetVersion()
        {
            return _version;
        }
    }
}