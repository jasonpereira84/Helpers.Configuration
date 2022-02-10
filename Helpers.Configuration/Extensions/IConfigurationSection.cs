using System;
using System.Collections.Generic;
using System.Linq;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.Extensions.Configuration;

        using Newtonsoft.Json;

        public static partial class Configuration
        {
            #region class
            public static T GetObject<T>(this IConfigurationSection configurationSection)
                where T : class, new()
            {
                Boolean _tryGet(out T t)
                {
                    t = ConfigurationBinder.Get<T>(configurationSection);
                    return t != null;
                }

                return _tryGet(out T t) ? t
                    : configurationSection.Value.IsNullOrEmptyOrWhiteSpace() ? default(T)
                        : JsonConvert.DeserializeObject<T>(configurationSection.Value);
            }

            public static Boolean TryGetObject<T>(this IConfigurationSection configurationSection, out T t)
                where T : class, new()
            {
                try
                {
                    t = GetObject<T>(configurationSection);
                    return t != null;
                }
                catch
                {
                    t = default(T);
                    return false;
                }
            }

            public static T[] GetObjects<T>(this IConfigurationSection configurationSection, Func<IConfigurationSection, T> selector)
                where T : class, new()
                => configurationSection.GetChildren()?.Select(selector)?.ToArray() ?? default(T[]);

            public static Boolean TryGetObjects<T>(this IConfigurationSection configurationSection, Func<IConfigurationSection, T> selector, out T[] ts)
                where T : class, new()
            {
                try
                {
                    ts = GetObjects<T>(configurationSection, selector);
                    return ts != null;
                }
                catch
                {
                    ts = default(T[]);
                    return false;
                }
            }

            public static T[] GetObjects<T>(this IConfigurationSection configurationSection)
                where T : class, new()
                => GetObjects<T>(configurationSection, cs => GetObject<T>(cs));

            public static Boolean TryGetObjects<T>(this IConfigurationSection configurationSection, out T[] ts)
                where T : class, new()
            {
                try
                {
                    ts = GetObjects<T>(configurationSection);
                    return ts != null;
                }
                catch
                {
                    ts = default(T[]);
                    return false;
                }
            }
            #endregion class

            #region struct
            public static T GetValue<T>(this IConfigurationSection configurationSection)
                where T : struct
            {
                Boolean _tryGet(out T t)
                {
                    t = ConfigurationBinder.Get<T>(configurationSection);
                    return true;
                }

                return _tryGet(out T t) ? t
                    : configurationSection.Value.IsNullOrEmptyOrWhiteSpace() ? default(T)
                        : JsonConvert.DeserializeObject<T>(configurationSection.Value);
            }

            public static Boolean TryGetValue<T>(this IConfigurationSection configurationSection, out T t)
                where T : struct
            {
                try
                {
                    t = GetValue<T>(configurationSection);
                    return true;
                }
                catch
                {
                    t = default(T);
                    return false;
                }
            }

            public static T[] GetValues<T>(this IConfigurationSection configurationSection, Func<IConfigurationSection, T> selector)
                where T : struct
                => configurationSection.GetChildren()?.Select(selector)?.ToArray() ?? default(T[]);

            public static Boolean TryGetValues<T>(this IConfigurationSection configurationSection, Func<IConfigurationSection, T> selector, out T[] ts)
                where T : struct
            {
                try
                {
                    ts = GetValues<T>(configurationSection, selector);
                    return ts != null;
                }
                catch
                {
                    ts = default(T[]);
                    return false;
                }
            }

            public static T[] GetValues<T>(this IConfigurationSection configurationSection)
                where T : struct
                => GetValues<T>(configurationSection, cs => GetValue<T>(cs));

            public static Boolean TryGetValues<T>(this IConfigurationSection configurationSection, out T[] ts)
                where T : struct
            {
                try
                {
                    ts = GetValues<T>(configurationSection);
                    return ts != null;
                }
                catch
                {
                    ts = default(T[]);
                    return false;
                }
            }
            #endregion struct

            #region String
            public static String GetString(this IConfigurationSection configurationSection)
            {
                Boolean _tryGet(out String s)
                {
                    s = ConfigurationBinder.Get<String>(configurationSection);
                    return true;
                }

                return _tryGet(out String s) ? s
                    : configurationSection.Value;
            }

            public static Boolean TryGetString(this IConfigurationSection configurationSection, out String @string)
            {
                try
                {
                    @string = GetString(configurationSection);
                    return true;
                }
                catch
                {
                    @string = default(String);
                    return false;
                }
            }
     
            public static String[] GetStrings(this IConfigurationSection configurationSection, Func<IConfigurationSection, String> selector)
                => configurationSection.GetChildren()?.Select(selector)?.ToArray() ?? default(String[]);

            public static Boolean TryGetStrings(this IConfigurationSection configurationSection, Func<IConfigurationSection, String> selector, out String[] strings)
            {
                try
                {
                    strings = GetStrings(configurationSection, selector);
                    return strings != null;
                }
                catch
                {
                    strings = default(String[]);
                    return false;
                }
            }

            public static String[] GetStrings(this IConfigurationSection configurationSection)
                => GetStrings(configurationSection, cs => GetString(cs));

            public static Boolean TryGetStrings(this IConfigurationSection configurationSection, out String[] strings)
            {
                try
                {
                    strings = GetStrings(configurationSection);
                    return strings != null;
                }
                catch
                {
                    strings = default(String[]);
                    return false;
                }
            }
            #endregion String

        }
    }
}
