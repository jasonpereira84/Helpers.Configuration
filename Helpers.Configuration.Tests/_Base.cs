using System;
using System.Collections.Generic;

namespace JasonPereira84.Helpers.Configuration.Tests
{
    using JasonPereira84.Helpers.Extensions;

    using Microsoft.Extensions.Configuration;

    internal class SomeConfigurationSource : IConfigurationSource
    {
        public IDictionary<String, String> Data { get; private set; }

        public SomeConfigurationSource(IDictionary<String, String> data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IConfigurationProvider Build(IConfigurationBuilder configurationBuilder)//Yup, the configurationBuilder is not used in this implemenation
            => new SomeConfigurationProvider(this);
    }

    internal class SomeConfigurationProvider : ConfigurationProvider
    {
        private readonly SomeConfigurationSource _source;

        public SomeConfigurationProvider(SomeConfigurationSource source)
            : base()
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public override void Load()
        {
            Misc.AddIfNewOrUpdate(Data, _source.Data);

            base.Load();
        }
    }

    internal class OtherConfigurationSource : IConfigurationSource
    {
        public IDictionary<String, String> Data { get; private set; }

        public OtherConfigurationSource(IDictionary<String, String> data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IConfigurationProvider Build(IConfigurationBuilder configurationBuilder)//Yup, the configurationBuilder is not used in this implemenation
            => new OtherConfigurationProvider(this);
    }

    internal class OtherConfigurationProvider : ConfigurationProvider
    {
        private readonly OtherConfigurationSource _source;

        public OtherConfigurationProvider(OtherConfigurationSource source)
            : base()
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public override void Load()
        {
            Misc.AddIfNewOrUpdate(Data, _source.Data);

            base.Load();
        }
    }

    internal class SomeClass : IEquatable<SomeClass>
    {
        public Int32 Value { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as SomeClass);
        }

        public bool Equals(SomeClass other)
        {
            if (other == null)
                return false;

            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            {
                hash.Add(Value);
            }
            return hash.ToHashCode();
        }

        public static bool operator ==(SomeClass left, SomeClass right)
        {
            return EqualityComparer<SomeClass>.Default.Equals(left, right);
        }

        public static bool operator !=(SomeClass left, SomeClass right)
        {
            return !(left == right);
        }

    }

}
