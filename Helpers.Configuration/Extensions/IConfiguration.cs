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
            #region class
            public static T GetObject<T>(this IConfiguration configuration, String key)
                where T : class, new()
                => GetObject<T>(configuration.GetSection(key));

            public static Boolean TryGetObject<T>(this IConfiguration configuration, String key, out T t)
                where T : class, new()
                => TryGetObject<T>(configuration.GetSection(key), out t);

            public static T[] GetObjects<T>(this IConfiguration configuration, String key, Func<IConfigurationSection, T> selector)
                where T : class, new()
                => GetObjects<T>(configuration.GetSection(key), selector);

            public static Boolean TryGetObjects<T>(this IConfiguration configuration, String key, Func<IConfigurationSection, T> selector, out T[] ts)
                where T : class, new()
                => TryGetObjects<T>(configuration.GetSection(key), selector, out ts);

            public static T[] GetObjects<T>(this IConfiguration configuration, String key)
                where T : class, new()
                => GetObjects<T>(configuration.GetSection(key));

            public static Boolean TryGetObjects<T>(this IConfiguration configuration, String key, out T[] ts)
                where T : class, new()
                => TryGetObjects<T>(configuration.GetSection(key), out ts);
            #endregion class

            #region struct
            public static T GetValue<T>(this IConfiguration configuration, String key)
                where T : struct
                => GetValue<T>(configuration.GetSection(key));

            public static Boolean TryGetValue<T>(this IConfiguration configuration, String key, out T t)
                where T : struct
                => TryGetValue<T>(configuration.GetSection(key), out t);

            public static T[] GetValues<T>(this IConfiguration configuration, String key, Func<IConfigurationSection, T> selector)
                where T : struct
                => GetValues(configuration.GetSection(key), selector);

            public static Boolean TryGetValues<T>(this IConfiguration configuration, String key, Func<IConfigurationSection, T> selector, out T[] ts)
                where T : struct
                => TryGetValues(configuration.GetSection(key), selector, out ts);

            public static T[] GetValues<T>(this IConfiguration configuration, String key)
                where T : struct
                => GetValues<T>(configuration.GetSection(key));

            public static Boolean TryGetValues<T>(this IConfiguration configuration, String key, out T[] ts)
                where T : struct
                => TryGetValues<T>(configuration.GetSection(key), out ts);
            #endregion struct

            #region String
            public static String GetString(this IConfiguration configuration, String key)
                => GetString(configuration.GetSection(key));

            public static Boolean TryGetString(this IConfiguration configuration, String key, out String @string)
                => TryGetString(configuration.GetSection(key), out @string);

            public static String[] GetStrings(this IConfiguration configuration, String key, Func<IConfigurationSection, String> selector)
                => GetStrings(configuration.GetSection(key), selector);

            public static Boolean TryGetStrings(this IConfiguration configuration, String key, Func<IConfigurationSection, String> selector, out String[] strings)
                => TryGetStrings(configuration.GetSection(key), selector, out strings);

            public static String[] GetStrings(this IConfiguration configuration, String key)
                => GetStrings(configuration.GetSection(key));

            public static Boolean TryGetStrings(this IConfiguration configuration, String key, out String[] strings)
                => TryGetStrings(configuration.GetSection(key), out strings);
            #endregion String

        }
    }
}
