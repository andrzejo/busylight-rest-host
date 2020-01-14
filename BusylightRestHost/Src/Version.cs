namespace BusylightRestHost
{
    public static class Version
    {
        private static readonly string Ver = Resources.ResourceContent("version").Trim();

        public static string Get()
        {
            return Ver;
        }
    }
}