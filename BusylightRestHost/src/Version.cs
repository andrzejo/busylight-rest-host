namespace BusylightRestHost
{
    public class Version
    {
        private static readonly string Ver = Resources.ResourceContent("version");

        public static string Get()
        {
            return Ver;
        }
    }
}