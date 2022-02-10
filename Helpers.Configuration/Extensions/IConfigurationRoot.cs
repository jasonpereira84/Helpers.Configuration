using System;
using System.Collections.Generic;
using System.Linq;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.Extensions.Configuration;

        public static partial class Configuration
        {
            public static Dictionary<String, List<String>> AsDictionary(this IConfigurationRoot configurationRoot)
            {
                var providers = configurationRoot.Providers.Reverse();

                Boolean _hasValueAndProvider(String key, out (String Value, String Provider) pair)
                {
                    foreach (var provider in providers)
                        if (provider.TryGet(key, out var value))
                        {
                            pair = (value, provider.ToString());
                            return true;
                        }

                    pair = (default, default);
                    return false;
                }

                String _format(String value)
                {
                    if (value.IsNull())
                        return "NULL";

                    return value.Trim()
                        .Replace("\\", "/")
                        .Replace("\"", "`");
                }

                Dictionary<String, List<String>> _recurseChildren(Dictionary<String, List<String>> dictionary, IEnumerable<IConfigurationSection> children)
                {
                    foreach (var child in children)
                    {
                        if(_hasValueAndProvider(child.Path, out (String Value, String Provider) pair))
                        {
                            if (!dictionary.ContainsKey(pair.Provider))
                                dictionary.Add(pair.Provider, new List<String>());

                            dictionary[pair.Provider].Add(
                                $"{child.Path}={_format(pair.Value)}");
                        }

                        _recurseChildren(dictionary, child.GetChildren());
                    }
                    return dictionary;
                }

                return _recurseChildren(new Dictionary<String, List<String>>(), configurationRoot.GetChildren());
            }

        }
    }
}
