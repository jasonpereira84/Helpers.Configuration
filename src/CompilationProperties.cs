using System;
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
            public static CompilationProperties GetCompilationProperties(this IConfiguration configuration, CompilationProperties defaultValue)
            {
                String _getCompilationProperty(String key, String defaultValue2)
                    => GetValue<String>(configuration, key, out (Boolean IsSuccess, String Value) retVal).IsSuccess
                        ? retVal.Value
                        : defaultValue2;

                return new CompilationProperties
                {
                    GIT_BRANCH = _getCompilationProperty(nameof(CompilationProperties.GIT_BRANCH), defaultValue.GIT_BRANCH),
                    GIT_COMMIT = _getCompilationProperty(nameof(CompilationProperties.GIT_COMMIT), defaultValue.GIT_COMMIT),
                    BUILD_CONFIGURATION = _getCompilationProperty(nameof(CompilationProperties.BUILD_CONFIGURATION), defaultValue.BUILD_CONFIGURATION),
                };
            }

            public static CompilationProperties GetCompilationProperties(this IConfiguration configuration, String defaultValue)
                => GetCompilationProperties(configuration, new CompilationProperties
                {
                    GIT_BRANCH = defaultValue,
                    GIT_COMMIT = defaultValue,
                    BUILD_CONFIGURATION = defaultValue
                });

            public static CompilationProperties GetCompilationProperties(this IConfiguration configuration)
                => GetCompilationProperties(configuration, "Unknown");

        }
    }
}
