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
        using Microsoft.Extensions.Configuration;

        public static partial class Configuration
        {
            public static (String Value, String Provider) GetValueAndProvider(this IConfigurationRoot configurationRoot, String key)
            {
                foreach (var provider in configurationRoot.Providers.Reverse())
                    if (provider.TryGet(key, out var value))
                        return (value, provider.ToString());

                return (null, null);
            }

            public static Dictionary<String, String> ToDictionary(this IConfigurationRoot configurationRoot)
            {
                String _sanitize(String value)
                {
                    if (String.IsNullOrWhiteSpace(value))
                        return value;

                    return value.Trim()
                        .Replace("\\", "/")
                        .Replace("\"", "`");
                }

                Dictionary<String, List<String>> _recurseChildren(Dictionary<String, List<String>> dictionary, IEnumerable<IConfigurationSection> children)
                {
                    foreach (var child in children)
                    {
                        var valueAndProvider = configurationRoot.GetValueAndProvider(child.Path);

                        if (valueAndProvider.Provider != null)
                        {
                            if (!dictionary.ContainsKey(valueAndProvider.Provider))
                                dictionary.Add(valueAndProvider.Provider, new List<String>());

                            dictionary[valueAndProvider.Provider].Add(
                                $"{child.Path}={_sanitize(valueAndProvider.Value)}");
                        }

                        _recurseChildren(dictionary, child.GetChildren());
                    }
                    return dictionary;
                }
                var providerSets = _recurseChildren(new Dictionary<String, List<String>>(), configurationRoot.GetChildren());

                (IEnumerable<String> PathNodes, String Value) _split(String @string)
                {
                    var delimiterIndex = @string.IndexOf('=');
                    return (@string.Substring(0, delimiterIndex).Split(new Char[] { ':' }, StringSplitOptions.RemoveEmptyEntries), @string.Substring(delimiterIndex));
                }

                (IEnumerable<String> PathNodes, String Value) _extract(String pathNode, (IEnumerable<String> PathNodes, String Value) pathNodeHierarchy)
                {
                    if (pathNodeHierarchy.PathNodes.Count().GreaterThan(1) && pathNodeHierarchy.PathNodes.First().Matches(pathNode))
                        return (pathNodeHierarchy.PathNodes.Skip(1), pathNodeHierarchy.Value);

                    return pathNodeHierarchy;
                }

                var retVal = new Dictionary<String, String>();
                foreach (var providerSet in providerSets)
                {
                    var pathNodeHierarchies = providerSet.Value
                        .Select(@string =>
                        {
                            var parts = _split(@string);
                            return new
                            {
                                PathNodeCount = parts.PathNodes.Count(),
                                Tuple = parts
                            };
                        })
                        .OrderBy(a => a.PathNodeCount);


                }
                return retVal;
            }

            public static T GetValue<T>(this IConfiguration configuration, String key, out T value)
                => value = ConfigurationBinder.GetValue<T>(configuration, key);

            public static (Boolean IsSuccess, T Value) GetValue<T>(this IConfiguration configuration, String key, out (Boolean IsSuccess, T Value) result)
            {
                try
                {
                    return result = (true, GetValue(configuration, key, out T value));
                }
                catch { return result = (false, default(T)); }
            }

            public static IConfigurationSection GetSection(this IConfiguration configuration, String key, out IConfigurationSection configurationSection)
                => configurationSection = configuration.GetSection(key);

            public static T Get<T>(this IConfigurationSection configurationSection, out T instance)
                where T : class, new()
                => instance = ConfigurationBinder.Get<T>(configurationSection);

            public static T GetObject<T>(this IConfigurationSection configurationSection, T defaultValue = default(T))
                where T : class, new()
                => Get(configurationSection, out T obj) != null ? obj
                    : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(configurationSection.Value) ?? defaultValue;
            public static T GetObject<T>(this IConfigurationSection configurationSection, out T obj, T defaultValue = default(T))
                where T : class, new()
                => obj = GetObject(configurationSection, defaultValue);

            public static T GetObject<T>(this IConfiguration configuration, String key, T defaultValue = default(T))
                where T : class, new()
                => GetSection(configuration, key, out IConfigurationSection configurationSection) == null
                    ? defaultValue
                    : GetObject(configurationSection, defaultValue);
            public static T GetObject<T>(this IConfiguration configuration, String key, out T obj, T defaultValue = default(T))
                where T : class, new()
                => obj = GetObject(configuration, key, defaultValue);


            public static IEnumerable<T> GetObjects<T>(this IConfiguration configuration, String key, Func<IConfigurationSection, T> selector)
                where T : class, new()
                => configuration.GetSection(key).GetChildren().Select(selector);
            public static IEnumerable<T> GetObjects<T>(this IConfiguration configuration, String key, Func<IConfigurationSection, T> selector, out IEnumerable<T> objs)
                where T : class, new()
                => objs = GetObjects<T>(configuration, key, selector);

            public static IEnumerable<T> GetObjects<T>(this IConfiguration configuration, String key)
                where T : class, new()
                => GetObjects<T>(configuration, key, child => child.Get<T>());
            public static IEnumerable<T> GetObjects<T>(this IConfiguration configuration, String key, out IEnumerable<T> objs)
                where T : class, new()
                => objs = GetObjects<T>(configuration, key);
        }
    }
}
