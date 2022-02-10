using System;
using System.Collections;
using System.Collections.Generic;

namespace JasonPereira84.Helpers
{
    using Microsoft.Extensions.Configuration;

    public class CompilationPropertiesConfigurationSource : IConfigurationSource
    {
        public CompilationProperties CompilationProperties { get; private set; }

        public CompilationPropertiesConfigurationSource(CompilationProperties compilationProperties)
        {
            CompilationProperties = compilationProperties ?? throw new ArgumentNullException(nameof(compilationProperties));
            //No need to validate each pair as CompilationProperties already ensures non-null values
        }

        public IConfigurationProvider Build(IConfigurationBuilder configurationBuilder)//Yup, the configurationBuilder is not used in this implemenation
            => new CompilationPropertiesConfigurationProvider(this);
    }

    public class CompilationPropertiesConfigurationProvider : ConfigurationProvider, IEnumerable<KeyValuePair<String, String>>
    {
        private readonly CompilationPropertiesConfigurationSource _source;

        public CompilationPropertiesConfigurationProvider(CompilationPropertiesConfigurationSource source)
            : base()
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public IEnumerator<KeyValuePair<String, String>> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override void Load()
        {
            //Done this way so that these configuration values will never change, they are set at build time and that is that
            Extensions.Misc.AddIfNewOrUpdate(Data, _source.CompilationProperties);

            base.Load();
        }
    }

}
