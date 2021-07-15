using System;
using System.Net;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.Extensions.Configuration;

        using Set = KeyValuePair<Int32, String>;
        using Pair = KeyValuePair<String, String>;
        using Dictionary = Dictionary<String, String>;
        using IDictionary = IDictionary<String, String>;

        namespace AWS
        {
            public static partial class ElasticBeanstalk
            {
                public const String ConfigurationFilePath = @"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration";

                public static IEnumerable<IConfigurationSection> GetConfigurationSections(Boolean optional = true, Boolean reloadOnChange = false)
                    => (ConfigurationFilePath, optional, reloadOnChange)
                            .ToConfigurationBuilder()
                            .GetSection("iis:env")
                            .GetChildren();
                public static void GetConfigurationSections(out IEnumerable<IConfigurationSection> configurationSections, Boolean optional = true, Boolean reloadOnChange = false)
                    => configurationSections = GetConfigurationSections();

                public static IDictionary GetProperties(Boolean optional = true, Boolean reloadOnChange = false)
                    => new Dictionary()
                        .AddIfNewOrUpdate(
                            GetConfigurationSections(optional, reloadOnChange)
                            .Select(configSection => configSection.Value.Split(new[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries))
                            .Where(parts => (parts.Length >= 2) && !String.IsNullOrWhiteSpace(parts[0]))
                            .Select(parts => (Key: parts[0], Value: WebUtility.HtmlDecode(parts[1].Sanitize()))));
                public static void GetProperties(out IDictionary properties, Boolean optional = true, Boolean reloadOnChange = false)
                    => properties = GetProperties();

            }
        }

        public class ElasticBeanstalkConfigurationProvider : ConfigurationProvider
        {
            private readonly IDictionary _source;

            public ElasticBeanstalkConfigurationProvider(IDictionary source)
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

        public class ElasticBeanstalkConfigurationSource : IInitialDataConfigurationSource
        {
            public IDictionary InitialData { get; private set; }

            public ElasticBeanstalkConfigurationSource(IDictionary initialData)
            {
                InitialData = initialData ?? throw new ArgumentNullException(nameof(initialData));
            }

            public ElasticBeanstalkConfigurationSource(Boolean optional = true, Boolean reloadOnChange = false)
                : this(initialData: AWS.ElasticBeanstalk.GetProperties(optional, reloadOnChange))
            { }

            public IConfigurationProvider Build(IConfigurationBuilder builder) 
                => new ElasticBeanstalkConfigurationProvider(InitialData);
        }

        public static partial class Configuration
        {
            public static IConfigurationBuilder AddElasticBeanstalk(this IConfigurationBuilder builder, ElasticBeanstalkConfigurationSource elasticBeanstalkConfigurationSource)
                => builder.Add(elasticBeanstalkConfigurationSource);

            public static IConfigurationBuilder AddElasticBeanstalk(this IConfigurationBuilder builder, IDictionary initialData)
                => AddElasticBeanstalk(builder, new ElasticBeanstalkConfigurationSource(initialData));

            public static IConfigurationBuilder AddElasticBeanstalk(this IConfigurationBuilder builder, Boolean optional = true, Boolean reloadOnChange = false)
                => AddElasticBeanstalk(builder, new ElasticBeanstalkConfigurationSource(optional, reloadOnChange));
        }
    }
}
