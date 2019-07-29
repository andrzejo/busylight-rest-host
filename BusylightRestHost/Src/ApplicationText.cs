namespace BusylightRestHost
{
    public static class ApplicationText
    {
        public const string ServerAddress = "localhost";
        public const int ServerPort = 5748;

        public const string AppHint = "Busylight - Browser Integration Host";
        public const string DocsUrl = "https://github.com/andrzejo/busylight-rest-host";

        public static string getTestPageUrl()
        {
            return $"http://{ServerAddress}:{ServerPort}/Sdk/sdk-test-site.html";
        }

        public static string getAbout()
        {
            return
                $"Kuando™ BUSYLIGHT device to web browser integration app.\n\nFor more information visit documentation page {DocsUrl}." +
                "\n\nNote that this application is Open Source project, NOT OFFICIAL Kuando software.\n\nCopyright (c) Andrzej Oczkowicz 2019";
        }
    }
}