using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using BusylightRestHost.Utils;

namespace BusylightRestHost
{
    public static class Resources
    {
        private const string ResourcesDir = "Resources";

        public static Dictionary<string, string> AllResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var currentNamespace = CurrentNamespace();
            var result = new Dictionary<string, string>();

            foreach (var resourceName in resourceNames)
            {
                var resourceNamePrefix = currentNamespace + "." + ResourcesDir + ".";
                if (resourceName.StartsWith(resourceNamePrefix))
                {
                    using (var stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        var res = Streams.Read(stream);
                        var name = resourceName.Replace(resourceNamePrefix, "");
                        result.Add(name, res);
                    }
                }
            }

            return result;
        }

        private static string CurrentNamespace()
        {
            return typeof(Resources).Namespace;
        }

        public static string ResourceContent(string name)
        {
            var allResources = AllResources();
            if (allResources.ContainsKey(name))
            {
                return allResources[name];
            }

            throw new MissingManifestResourceException(
                $"Resource '{name}' not found in namespace '{CurrentNamespace()}'.");
        }
    }
}