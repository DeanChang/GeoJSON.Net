using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GeoJSON.Net.Tests
{
    public abstract class TestBase
    {
        private static readonly string AssemblyName = typeof(TestBase).Assembly.GetName().Name;

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = typeof(TestBase).Assembly.CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }


#if (NET45)

        protected string GetExpectedJson([CallerMemberName] string name = null)
        {
            var type = GetType().Name;
            var projectFolder = GetType().Namespace.Substring(AssemblyName.Length + 1);
            var path = Path.Combine(AssemblyDirectory, @".\", projectFolder, type + "_" + name + ".json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("file not found at " + path);
            }

            return File.ReadAllText(path);
        }
#else
        protected string GetExpectedJson(string name = null)
        {
            var type = GetType().Name;
            var projectFolder = GetType().Namespace.Substring(AssemblyName.Length + 1);
            var path = Path.Combine(AssemblyDirectory, @".\", projectFolder, type + "_" + name + ".json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("file not found at " + path);
            }

            return File.ReadAllText(path);
        }
#endif
    }
}