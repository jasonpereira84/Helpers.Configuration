using System;
using System.IO;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.Extensions.Configuration;

        public static partial class Configuration
        {
            public static IConfigurationBuilder From<TConfigurationBuilder>(Func<IConfigurationBuilder, IConfigurationBuilder> enrich)
                where TConfigurationBuilder : IConfigurationBuilder, new()
                => enrich(new TConfigurationBuilder() as IConfigurationBuilder);

            public static IConfigurationSection GetSection<TConfigurationBuilder>(this TConfigurationBuilder configurationBuilder, String key)
                where TConfigurationBuilder : IConfigurationBuilder
                => configurationBuilder.Build().GetSection(key);

            public static IConfigurationBuilder FromJsonFile<TConfigurationBuilder>(String path, Boolean optional, Boolean reloadOnChange)
                where TConfigurationBuilder : IConfigurationBuilder, new()
                => From<TConfigurationBuilder>(builder => builder.AddJsonFile(path, optional, reloadOnChange));
            public static IConfigurationBuilder FromJsonFile<TConfigurationBuilder>(String path, Boolean optional)
                where TConfigurationBuilder : IConfigurationBuilder, new()
                => From<TConfigurationBuilder>(builder => builder.AddJsonFile(path, optional));
            public static IConfigurationBuilder FromJsonFile<TConfigurationBuilder>(String path)
                where TConfigurationBuilder : IConfigurationBuilder, new()
                => From<TConfigurationBuilder>(builder => builder.AddJsonFile(path));

            public static IConfigurationBuilder ToConfigurationBuilder(this (String Path, Boolean Optional, Boolean ReloadOnChange) args)
                => FromJsonFile<ConfigurationBuilder>(args.Path, args.Optional, args.ReloadOnChange);
            public static IConfigurationBuilder ToConfigurationBuilder(this (String Path, Boolean Optional) args)
                => FromJsonFile<ConfigurationBuilder>(args.Path, args.Optional);
            public static IConfigurationBuilder ToConfigurationBuilder(this String path)
                => FromJsonFile<ConfigurationBuilder>(path);

            public static IConfigurationBuilder AddCompilationProperties(this IConfigurationBuilder configurationBuilder, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => configurationBuilder.Add(compilationPropertiesConfigurationSource);

            public static IConfigurationBuilder AddCompilationProperties(this IConfigurationBuilder configurationBuilder, CompilationProperties initialData)
                => AddCompilationProperties(configurationBuilder, new CompilationPropertiesConfigurationSource(initialData));

        }
    }
}
