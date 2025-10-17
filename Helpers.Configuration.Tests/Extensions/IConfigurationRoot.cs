using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JasonPereira84.Helpers.Configuration.Tests
{
    namespace Extensions
    {
        using JasonPereira84.Helpers.Extensions;

        using Microsoft.Extensions.Configuration;

        [TestClass]
        public class Test_IConfigurationRoot
        {
            [TestMethod]
            public void AsDictionary()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "D1_O1:P1", "D1_O1_1"},
                        { "D1_O1:P2:P2_1", "D1_O1_2"},
                        { "D1_O1:P2:P2_2:P2_2_1", "D1_O1_3"},
                        { "D1_O2:P1", "D1_O2_1"},
                        { "D1_O2:P2:P2_1", "D1_O2_2"},
                        { "D1_O2:P2:P2_2:P2_2_1", "D1_O2_3"},
                    };
                    
                    var configurationRoot = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build();

                    var dictionary = configurationRoot.AsDictionary();
                    Assert.IsTrue(dictionary.ContainsKey(nameof(SomeConfigurationProvider)));
                    Assert.IsNotNull(dictionary[nameof(SomeConfigurationProvider)]);
                    {
                        var list = dictionary[nameof(SomeConfigurationProvider)];
                        foreach (var pair in data)
#pragma warning disable MSTEST0037
                            Assert.IsTrue(list.Contains($"{pair.Key}={pair.Value}"));
#pragma warning restore MSTEST0037
                    }

                }

                {
                    var data1 = new Dictionary<String, String>
                    {
                        { "D1_O1:P1", "D1_O1_1"},
                        { "D1_O1:P2:P2_1", "D1_O1_2"},
                        { "D1_O1:P2:P2_2:P2_2_1", "D1_O1_3"},
                        { "D1_O2:P1", "D1_O2_1"},
                        { "D1_O2:P2:P2_1", "D1_O2_2"},
                        { "D1_O2:P2:P2_2:P2_2_1", "D1_O2_3"},
                    };
                    var data2 = new Dictionary<String, String>
                    {
                        { "D2_O1:P1", "D2_O1_1"},
                        { "D2_O1:P2:P2_1", "D2_O1_2"},
                        { "D2_O1:P2:P2_2:P2_2_1", "D2_O1_3"},
                        { "D2_O2:P1", "D2_O2_1"},
                        { "D2_O2:P2:P2_1", "D2_O2_2"},
                        { "D2_O2:P2:P2_2:P2_2_1", "D2_O2_3"},
                    };
                    var configurationRoot = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data1))
                        .Add(new OtherConfigurationSource(data2))
                        .Build();

                    var dictionary = configurationRoot.AsDictionary();
                    Assert.IsTrue(dictionary.ContainsKey(nameof(SomeConfigurationProvider)));
                    Assert.IsNotNull(dictionary[nameof(SomeConfigurationProvider)]);
                    {
                        var list = dictionary[nameof(SomeConfigurationProvider)];
#pragma warning disable MSTEST0037
                        foreach (var pair in data1)
                            Assert.IsTrue(list.Contains($"{pair.Key}={pair.Value}"));
#pragma warning restore MSTEST0037
                    }
                    Assert.IsTrue(dictionary.ContainsKey(nameof(OtherConfigurationProvider)));
                    Assert.IsNotNull(dictionary[nameof(OtherConfigurationProvider)]);
                    {
                        var list = dictionary[nameof(OtherConfigurationProvider)];
#pragma warning disable MSTEST0037
                        foreach (var pair in data2)
                            Assert.IsTrue(list.Contains($"{pair.Key}={pair.Value}"));
#pragma warning restore MSTEST0037
                    }

                }

                {
                    var data1 = new Dictionary<String, String>
                    {
                        { "D1_O1:P1", "D1_O1_1"},
                        { "D1_O1:P2:P2_1", "D1_O1_2"},
                        { "D1_O1:P2:P2_2:P2_2_1", "D1_O1_3"},
                        { "D1_O2:P1", "D1_O2_1"},
                        { "D1_O2:P2:P2_1", "D1_O2_2"},
                        { "D1_O2:P2:P2_2:P2_2_1", "D1_O2_3"},

                        { "key", "old-value"},
                    };
                    var data2 = new Dictionary<String, String>
                    {
                        { "D2_O1:P1", "D2_O1_1"},
                        { "D2_O1:P2:P2_1", "D2_O1_2"},
                        { "D2_O1:P2:P2_2:P2_2_1", "D2_O1_3"},
                        { "D2_O2:P1", "D2_O2_1"},
                        { "D2_O2:P2:P2_1", "D2_O2_2"},
                        { "D2_O2:P2:P2_2:P2_2_1", "D2_O2_3"},

                        { "key", "new-value"},
                    };
                    var configurationRoot = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data1))
                        .Add(new OtherConfigurationSource(data2))
                        .Build();

                    var dictionary = configurationRoot.AsDictionary();
                    Assert.IsTrue(dictionary.ContainsKey(nameof(SomeConfigurationProvider)));
                    Assert.IsNotNull(dictionary[nameof(SomeConfigurationProvider)]);
                    {
                        var list = dictionary[nameof(SomeConfigurationProvider)];
#pragma warning disable MSTEST0037
                        foreach (var pair in data1.Where(x => !x.Key.Equals("key")))
                            Assert.IsTrue(list.Contains($"{pair.Key}={pair.Value}"));
#pragma warning restore MSTEST0037
                    }
                    Assert.IsTrue(dictionary.ContainsKey(nameof(OtherConfigurationProvider)));
                    Assert.IsNotNull(dictionary[nameof(OtherConfigurationProvider)]);
                    {
                        var list = dictionary[nameof(OtherConfigurationProvider)];
#pragma warning disable MSTEST0037
                        foreach (var pair in data2)
                            Assert.IsTrue(list.Contains($"{pair.Key}={pair.Value}"));
#pragma warning restore MSTEST0037
                    }

                }

            }

        }
    }
}
