using System;
using System.Linq;

namespace BusylightRestHost.Utils
{
    public class AppArguments
    {
        public static bool forceMock()
        {
            var arguments = Environment.GetCommandLineArgs();
            return arguments.Any(argument => argument == "--force-mock");
        }
    }
}