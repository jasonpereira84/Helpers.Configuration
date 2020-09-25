using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.Extensions.Configuration;

        using Stack = Stack<String>;
        using Dictionary = Dictionary<String, String>;
        using IDictionary = IDictionary<String, String>;
        using SortedDictionary = SortedDictionary<String, String>;

        public interface IInitialDataConfigurationSource : IConfigurationSource
        {
            IDictionary InitialData { get; }
        }
    }
}
