using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JasonPereira84.Helpers.Configuration.Tests
{
    using JasonPereira84.Helpers.Extensions;

    [TestClass]
    public class Test_CompilationProperties
    {
        [TestMethod]
        public void CompilationPropertiesConfigurationSource()
        {
            {
                {
                    var compilationProperties = new CompilationProperties();
                    var source = new CompilationPropertiesConfigurationSource(compilationProperties);

                    Assert.IsNotNull(source.CompilationProperties);
                    Assert.AreSame(
                        expected: compilationProperties,
                        actual: source.CompilationProperties);
                }

                {
                    var compilationProperties = default(CompilationProperties);

                    Assert.ThrowsException<ArgumentNullException>(
                        () => new CompilationPropertiesConfigurationSource(compilationProperties));
                }
            }

            {
                var source = new CompilationPropertiesConfigurationSource(new CompilationProperties());

                Assert.IsNotNull(source.Build(default));
            }

        }

        [TestMethod]
        public void CompilationPropertiesConfigurationProvider()
        {
            {
                {
                    var source = new CompilationPropertiesConfigurationSource(new CompilationProperties());

                    Assert.IsNotNull(new CompilationPropertiesConfigurationProvider(source));
                }

                {
                    var source = default(CompilationPropertiesConfigurationSource);

                    Assert.ThrowsException<ArgumentNullException>(
                        () => new CompilationPropertiesConfigurationProvider(source));
                }
            }

            {
                {
                    var compilationProperties = new CompilationProperties();
                    var source = new CompilationPropertiesConfigurationSource(compilationProperties);
                    var provider = new CompilationPropertiesConfigurationProvider(source);

                    Assert.IsTrue(provider.Count() == 0);

                    provider.Load();
                    var data = provider.ToDictionary(x => x.Key, x => x.Value);

                    Assert.IsTrue(provider.Count() == 3);
                    Assert.IsTrue(data.ContainsKey(nameof(CompilationProperties.GIT_BRANCH)));
                    Assert.IsTrue(data[nameof(CompilationProperties.GIT_BRANCH)].Equals("Unknown"));
                    Assert.IsTrue(data.ContainsKey(nameof(CompilationProperties.GIT_COMMIT)));
                    Assert.IsTrue(data[nameof(CompilationProperties.GIT_COMMIT)].Equals("Unknown"));
                    Assert.IsTrue(data.ContainsKey(nameof(CompilationProperties.BUILD_CONFIGURATION)));
                    Assert.IsTrue(data[nameof(CompilationProperties.BUILD_CONFIGURATION)].Equals("Unknown"));
                }

                {
                    var compilationProperties = new CompilationProperties();
                    var source = new CompilationPropertiesConfigurationSource(compilationProperties);
                    var provider = new CompilationPropertiesConfigurationProvider(source);

                    String _gitBranch()
                        => provider.FirstOrDefault(x => x.Key.Equals(nameof(CompilationProperties.GIT_BRANCH))).Value;

                    provider.Load();

                    Assert.AreEqual(
                        expected: "Unknown",
                        actual: _gitBranch());

                    provider.Set(nameof(CompilationProperties.GIT_BRANCH), "1");
                    Assert.AreEqual(
                        expected: "1",
                        actual: _gitBranch());

                    provider.Load();
                    Assert.AreEqual(
                        expected: "Unknown",
                        actual: _gitBranch());
                }

            }

        }

    }
}
