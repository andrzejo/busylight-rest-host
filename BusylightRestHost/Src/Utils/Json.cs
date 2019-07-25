using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BusylightRestHost.Utils
{
    public static class Json
    {
        public static string Serialize(Type type, object obj)
        {
            var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(type);
            serializer.WriteObject(stream, obj);
            return Streams.Read(stream);
        }
    }
}