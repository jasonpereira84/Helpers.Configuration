using System;

namespace JasonPereira84.Helpers
{
    namespace Extensions
    {
        using Microsoft.Extensions.Configuration;

        public static partial class Configuration
        {
            #region CompilationProperties
            public static IConfigurationBuilder AddCompilationProperties(this IConfigurationBuilder configurationBuilder, CompilationPropertiesConfigurationSource compilationPropertiesConfigurationSource)
                => configurationBuilder.Add(compilationPropertiesConfigurationSource);

            public static IConfigurationBuilder AddCompilationProperties(this IConfigurationBuilder configurationBuilder, CompilationProperties compilationProperties)
                => AddCompilationProperties(configurationBuilder, new CompilationPropertiesConfigurationSource(compilationProperties));
            #endregion CompilationProperties
        }
    }
}
