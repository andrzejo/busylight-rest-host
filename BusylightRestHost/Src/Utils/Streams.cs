using System.IO;

namespace BusylightRestHost.Utils
{
    public static class Streams
    {
        public static string Read(Stream stream)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}