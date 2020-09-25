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

        using Newtonsoft.Json;

        using UserSecretsIdAttribute = Microsoft.Extensions.Configuration.UserSecrets.UserSecretsIdAttribute;

        using Stack = Stack<String>;
        using Dictionary = Dictionary<String, String>;
        using IDictionary = IDictionary<String, String>;
        using SortedDictionary = SortedDictionary<String, String>;

        namespace Json
        {
            public static partial class UserSecrets
            {
                public const String SecretsFileName = "secrets.json";

                public static String GetPath(String userSecretsId)
                {
                    if (userSecretsId.EvaluateSanity(out String userSecretsId_SANE).IsFalse())
                        throw new ArgumentException($"Value cannot be {userSecretsId_SANE}", nameof(userSecretsId));

                    var badCharIndex = userSecretsId.IndexOfAny(Path.GetInvalidFileNameChars());
                    if (badCharIndex != -1)
                        throw new InvalidOperationException($"Invalid character '{userSecretsId[badCharIndex]}' found in the user secrets ID at index '{badCharIndex}'");

                    const String userSecretsFallbackDir = "DOTNET_USER_SECRETS_FALLBACK_DIR";

                    // For backwards compat, this checks env vars first before using Env.GetFolderPath
                    var root = Environment.GetEnvironmentVariable("APPDATA")                             // On Windows it goes to %APPDATA%\Microsoft\UserSecrets\
                               ?? Environment.GetEnvironmentVariable("HOME")                             // On Mac/Linux it goes to ~/.microsoft/usersecrets/
                               ?? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                               ?? Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
                               ?? Environment.GetEnvironmentVariable(userSecretsFallbackDir);            // this fallback is an escape hatch if everything else fails

                    if (root.IsNullOrEmptyOrWhiteSpace())
                        throw new InvalidOperationException($"Could not determine an appropriate location for storing user secrets. Set the {userSecretsFallbackDir} environment variable to a folder where user secrets should be stored.");

                    return Environment
                        .GetEnvironmentVariable("APPDATA")
                        .IsNotNullOrEmptyOrWhiteSpace()
                            ? Path.Combine(root, "Microsoft", "UserSecrets", userSecretsId, SecretsFileName)
                            : Path.Combine(root, ".microsoft", "usersecrets", userSecretsId, SecretsFileName);
                }

                public static IDictionary GetProperties(this String path, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                {
                    var path_EVAL = path.EvaluateSanity();

                    if (optional.IsFalse())
                    {
                        if (path_EVAL.IsSane.IsFalse())
                            throw new ArgumentException($"File path cannot be '{path_EVAL.Value}' if NOT-OPTIONAL", nameof(path));

                        if (File.Exists(path_EVAL.Value).IsFalse())
                            throw new ArgumentException($"The file '{path_EVAL.Value}' was NOT-FOUND and was NOT-OPTIONAL", nameof(path));
                    }

                    if (path_EVAL.IsSane && File.Exists(path_EVAL.Value))
                        return Misc.Parse(path_EVAL.Value, delimiter, dateParseHandling);

                    return new SortedDictionary();
                }

                public static IDictionary GetProperties(this UserSecretsIdAttribute userSecretsIdAttribute, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                {
                    if (optional.IsFalse() && userSecretsIdAttribute.IsNull())
                        throw new ArgumentNullException(nameof(userSecretsIdAttribute), "Value cannot be NULL if NOT-OPTIONAL");

                    return GetProperties(GetPath(userSecretsIdAttribute?.UserSecretsId), delimiter, dateParseHandling, optional);
                }

                public static IDictionary GetProperties(this Assembly assembly, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                {
                    if (optional.IsFalse() && assembly.IsNull())
                        throw new ArgumentNullException(nameof(assembly), "Value cannot be NULL if NOT-OPTIONAL");

                    return GetProperties(assembly?.GetCustomAttribute<UserSecretsIdAttribute>(), delimiter, dateParseHandling, optional);
                }

                public static IDictionary GetProperties(String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                    => GetProperties(Assembly.GetCallingAssembly(), delimiter, dateParseHandling, optional);
            }
        }

        public class UserSecretsConfigurationProvider : ConfigurationProvider
        {
            private readonly IDictionary _source;

            public UserSecretsConfigurationProvider(IDictionary source)
            {
                _source = source ?? throw new ArgumentNullException(nameof(source));
            }

            public override void Load()
            {
                foreach (var pair in _source)
                    Data.Add(pair.Key, pair.Value);

                base.Load();
            }
        }

        public class UserSecretsConfigurationSource : IInitialDataConfigurationSource
        {
            public IDictionary InitialData { get; private set; }

            public UserSecretsConfigurationSource(IDictionary initialData)
            {
                InitialData = initialData ?? throw new ArgumentNullException(nameof(initialData));
            }

            public UserSecretsConfigurationSource(String path, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                : this(initialData: Json.UserSecrets.GetProperties(path,delimiter, dateParseHandling, optional))
            { }

            public UserSecretsConfigurationSource(UserSecretsIdAttribute userSecretsIdAttribute, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                : this(initialData: Json.UserSecrets.GetProperties(userSecretsIdAttribute, delimiter, dateParseHandling, optional))
            { }

            public UserSecretsConfigurationSource(Assembly assembly, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                : this(initialData: Json.UserSecrets.GetProperties(assembly, delimiter, dateParseHandling, optional))
            { }

            public IConfigurationProvider Build(IConfigurationBuilder builder) 
                => new ElasticBeanstalkConfigurationProvider(InitialData);
        }

        public static partial class Configuration
        {
            public static IConfigurationBuilder AddUserSecrets(this IConfigurationBuilder builder, UserSecretsConfigurationSource userSecretsConfigurationSource)
                => builder.Add(userSecretsConfigurationSource);

            public static IConfigurationBuilder AddUserSecrets(this IConfigurationBuilder builder, IDictionary initialData)
                => AddUserSecrets(builder, new UserSecretsConfigurationSource(initialData));

            public static IConfigurationBuilder AddUserSecrets(this IConfigurationBuilder builder, String path, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                => AddUserSecrets(builder, new UserSecretsConfigurationSource(path, delimiter, dateParseHandling, optional));

            public static IConfigurationBuilder AddUserSecrets(this IConfigurationBuilder builder, UserSecretsIdAttribute userSecretsIdAttribute, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                => AddUserSecrets(builder, new UserSecretsConfigurationSource(userSecretsIdAttribute, delimiter, dateParseHandling, optional));

            public static IConfigurationBuilder AddUserSecrets(this IConfigurationBuilder builder, Assembly assembly, String delimiter = ":", DateParseHandling dateParseHandling = DateParseHandling.None, Boolean optional = true)
                => AddUserSecrets(builder, new UserSecretsConfigurationSource(assembly, delimiter, dateParseHandling, optional));
        }
    }
}
