using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        public static partial class Configuration
        {
            public static String GetEnvironment(this IDictionary<String, String> dictionary, String defaultValue = "Production")
                => dictionary.ValueOrDefault("ASPNETCORE_ENVIRONMENT", defaultValue.SanitizeTo("Production"));
        }
    }
}
