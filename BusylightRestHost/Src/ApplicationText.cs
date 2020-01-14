namespace BusylightRestHost
{
    public static class ApplicationText
    {
        public const string ServerAddress = "localhost";
        public const int ServerPort = 5748;

        public const string DocsUrl = "https://github.com/thuliumcc/busylight-rest-host";

        public static string GetTestPageUrl()
        {
            return $"http://{ServerAddress}:{ServerPort}/Sdk/sdk-test-site.html";
        }

        public static string GetAbout()
        {
            return
                $"Kuando™ BUSYLIGHT device web browser integration app.\n\nFor more information visit documentation page {DocsUrl}." +
                "\n\nNote that this application is Open Source project, NOT OFFICIAL Kuando software.\n\nCopyright (c) Andrzej Oczkowicz 2020";
        }

        public static string GetAppHint()
        {
            return $"Busylight - Browser Integration Host ({Version.Get()})";
        }

        public static string GetAutostartText()
        {
            return "Busylight Integration Host";
        }
    }
}