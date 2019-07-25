using System.IO;

namespace BusylightRestHost.Utils
{
    public static class Streams
    {
        public static string Read(Stream stream)
        {
            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}