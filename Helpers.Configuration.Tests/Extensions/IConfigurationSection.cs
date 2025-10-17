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
        public class Test_IConfigurationSection
        {
            #region class
            [TestMethod]
            public void GetObject()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key:Value", "1"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.AreEqual(
                        expected: new SomeClass { Value = 1 },
                        actual: configurationSection.GetObject<SomeClass>());
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: default(SomeClass),
                            actual: configurationSection.GetObject<SomeClass>());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: default(SomeClass),
                            actual: configurationSection.GetObject<SomeClass>());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: default(SomeClass),
                            actual: configurationSection.GetObject<SomeClass>());
                    }

                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:Value", default(String)}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: new SomeClass(),
                            actual: configurationSection.GetObject<SomeClass>());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:Value", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.Throws<InvalidOperationException>(
                            () => configurationSection.GetObject<SomeClass>());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:Value", "x"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.Throws<InvalidOperationException>(
                            () => configurationSection.GetObject<SomeClass>());
                    }
                }

            }

            [TestMethod]
            public void TryGetObject()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key:Value", "1"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(configurationSection.TryGetObject(out SomeClass someClass));
                    Assert.AreEqual(
                        expected: new SomeClass { Value = 1 },
                        actual: someClass);
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetObject(out SomeClass someClass));
                        Assert.AreEqual(
                            expected: default(SomeClass),
                            actual: someClass);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetObject(out SomeClass someClass));
                        Assert.AreEqual(
                            expected: default(SomeClass),
                            actual: someClass);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetObject(out SomeClass someClass));
                        Assert.AreEqual(
                            expected: default(SomeClass),
                            actual: someClass);
                    }

                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:Value", default(String)}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetObject(out SomeClass someClass));
                        Assert.AreEqual(
                            expected: new SomeClass(),
                            actual: someClass);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:Value", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetObject(out SomeClass someClass));
                        Assert.AreEqual(
                            expected: default(SomeClass),
                            actual: someClass);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:Value", "x"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetObject(out SomeClass someClass));
                        Assert.AreEqual(
                            expected: default(SomeClass),
                            actual: someClass);
                    }
                }

            }

            [TestMethod]
            public void GetObjects()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key:0:Value", "1"},
                        { "key:1:Value", "2"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(
                        new[] { 
                            new SomeClass { Value = 1 }, 
                            new SomeClass { Value = 2 } }.SequenceEqual(configurationSection.GetObjects<SomeClass>()));
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new SomeClass[0].SequenceEqual(configurationSection.GetObjects<SomeClass>()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new SomeClass[0].SequenceEqual(configurationSection.GetObjects<SomeClass>()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new SomeClass[0].SequenceEqual(configurationSection.GetObjects<SomeClass>()));
                    }

                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0:Value", default(String)},
                            { "key:1:Value", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new[] {
                                new SomeClass { Value = default(Int32) },
                                new SomeClass { Value = 2 } }.SequenceEqual(configurationSection.GetObjects<SomeClass>()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0:Value", ""},
                            { "key:1:Value", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.Throws<InvalidOperationException>(
                            () => configurationSection.GetObjects<SomeClass>());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0:Value", "x"},
                            { "key:1:Value", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.Throws<InvalidOperationException>(
                            () => configurationSection.GetObjects<SomeClass>());
                    }

                }

            }

            [TestMethod]
            public void TryGetObjects()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key:0:Value", "1"},
                        { "key:1:Value", "2"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(configurationSection.TryGetObjects(out SomeClass[] someClasses));
                    Assert.IsTrue(
                        new[] {
                            new SomeClass { Value = 1 },
                            new SomeClass { Value = 2 } }.SequenceEqual(someClasses));
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetObjects(out SomeClass[] someClasses));
                        Assert.IsTrue(
                            new SomeClass[0].SequenceEqual(someClasses));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetObjects(out SomeClass[] someClasses));
                        Assert.IsTrue(
                            new SomeClass[0].SequenceEqual(someClasses));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetObjects(out SomeClass[] someClasses));
                        Assert.IsTrue(
                            new SomeClass[0].SequenceEqual(someClasses));
                    }

                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0:Value", default(String)},
                            { "key:1:Value", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetObjects(out SomeClass[] someClasses));
                        Assert.IsTrue(
                            new[] {
                                new SomeClass { Value = default(Int32) },
                                new SomeClass { Value = 2 } }.SequenceEqual(someClasses));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0:Value", ""},
                            { "key:1:Value", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetObjects(out SomeClass[] someClasses));
                        Assert.IsNull(someClasses);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0:Value", "x"},
                            { "key:1:Value", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetObjects(out SomeClass[] someClasses));
                        Assert.IsNull(someClasses);
                    }

                }

            }
            #endregion class

            #region struct
            [TestMethod]
            public void GetValue()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key", "1"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.AreEqual(
                        expected: 1,
                        actual: configurationSection.GetValue<Int32>());
                }

                {
                    var data = new Dictionary<String, String>();

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.AreEqual(
                        expected: default(Int32),
                        actual: configurationSection.GetValue<Int32>());
                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: default(Int32),
                            actual: configurationSection.GetValue<Int32>());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.Throws<InvalidOperationException>(
                            () => configurationSection.GetValue<Int32>());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", "x"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.Throws<InvalidOperationException>(
                            () => configurationSection.GetValue<Int32>());
                    }

                }

            }

            [TestMethod]
            public void TryGetValue()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key", "1"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(configurationSection.TryGetValue(out Int32 int32));
                    Assert.AreEqual(
                        expected: 1,
                        actual: int32);
                }

                {
                    var data = new Dictionary<String, String>();

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(configurationSection.TryGetValue(out Int32 int32));
                    Assert.AreEqual(
                        expected: default(Int32),
                        actual: int32);
                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetValue(out Int32 int32));
                        Assert.AreEqual(
                            expected: default(Int32),
                            actual: int32);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetValue(out Int32 int32));
                        Assert.AreEqual(
                            expected: default(Int32),
                            actual: int32);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", "x"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetValue(out Int32 int32));
                        Assert.AreEqual(
                            expected: default(Int32),
                            actual: int32);
                    }

                }

            }

            [TestMethod]
            public void GetValues()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key:0", "1"},
                        { "key:1", "2"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(
                        new[] { 1, 2 }.SequenceEqual(configurationSection.GetValues<Int32>()));
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new Int32[0].SequenceEqual(configurationSection.GetValues<Int32>()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new Int32[0].SequenceEqual(configurationSection.GetValues<Int32>()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new Int32[0].SequenceEqual(configurationSection.GetValues<Int32>()));
                    }

                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", default(String)},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new[] { default(Int32), 2 }.SequenceEqual(configurationSection.GetValues<Int32>()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", ""},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.Throws<InvalidOperationException>(
                            () => configurationSection.GetValues<Int32>());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", "x"},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.Throws<InvalidOperationException>(
                            () => configurationSection.GetValues<Int32>());
                    }

                }

            }

            [TestMethod]
            public void TryGetValues()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key:0", "1"},
                        { "key:1", "2"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(configurationSection.TryGetValues(out Int32[] ints));
                    Assert.IsTrue(
                        new[] { 1, 2 }.SequenceEqual(ints));
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetValues(out Int32[] ints));
                        Assert.IsTrue(
                            new Int32[0].SequenceEqual(ints));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetValues(out Int32[] ints));
                        Assert.IsTrue(
                            new Int32[0].SequenceEqual(ints));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetValues(out Int32[] ints));
                        Assert.IsTrue(
                            new Int32[0].SequenceEqual(ints));
                    }

                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", default(String)},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetValues(out Int32[] ints));
                        Assert.IsTrue(
                            new[] { default(Int32), 2 }.SequenceEqual(ints));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", ""},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetValues(out Int32[] ints));
                        Assert.IsNull(ints);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", "x"},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsFalse(configurationSection.TryGetValues(out Int32[] ints));
                        Assert.IsNull(ints);
                    }

                }

            }
            #endregion struct

            #region String
            [TestMethod]
            public void GetString()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key", "1"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.AreEqual(
                        expected: "1",
                        actual: configurationSection.GetString());
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: default(String),
                            actual: configurationSection.GetString());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: default(String),
                            actual: configurationSection.GetString());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: "",
                            actual: configurationSection.GetString());
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", " "}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.AreEqual(
                            expected: " ",
                            actual: configurationSection.GetString());
                    }

                }

            }

            [TestMethod]
            public void TryGetString()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key", "1"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(configurationSection.TryGetString(out String @string));
                    Assert.AreEqual(
                        expected: "1",
                        actual: @string);
                }

                {
                    var data = new Dictionary<String, String>();

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(configurationSection.TryGetString(out String @string));
                    Assert.AreEqual(
                        expected: default(String),
                        actual: @string);
                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetString(out String @string));
                        Assert.AreEqual(
                            expected: default(String),
                            actual: @string);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetString(out String @string));
                        Assert.AreEqual(
                            expected: "",
                            actual: @string);
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", " "}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetString(out String @string));
                        Assert.AreEqual(
                            expected: " ",
                            actual: @string);
                    }

                }

            }

            [TestMethod]
            public void GetStrings()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key:0", "1"},
                        { "key:1", "2"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(
                        new[] { "1", "2" }.SequenceEqual(configurationSection.GetStrings()));
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new String[0].SequenceEqual(configurationSection.GetStrings()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new String[0].SequenceEqual(configurationSection.GetStrings()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new String[0].SequenceEqual(configurationSection.GetStrings()));
                    }

                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", default(String)},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new[] { default(String), "2" }.SequenceEqual(configurationSection.GetStrings()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", ""},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new[] { "", "2" }.SequenceEqual(configurationSection.GetStrings()));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", " "},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(
                            new[] { " ", "2" }.SequenceEqual(configurationSection.GetStrings()));
                    }

                }

            }

            [TestMethod]
            public void TryGetStrings()
            {
                {
                    var data = new Dictionary<String, String>
                    {
                        { "key:0", "1"},
                        { "key:1", "2"}
                    };

                    var configuration = new ConfigurationBuilder()
                        .Add(new SomeConfigurationSource(data))
                        .Build() as IConfiguration;
                    var configurationSection = configuration.GetSection("key");

                    Assert.IsTrue(configurationSection.TryGetStrings(out String[] strings));
                    Assert.IsTrue(
                        new[] { "1", "2" }.SequenceEqual(strings));
                }

                {
                    {
                        var data = new Dictionary<String, String>();

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetStrings(out String[] strings));
                        Assert.IsTrue(
                            new String[0].SequenceEqual(strings));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", default(String)},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetStrings(out String[] strings));
                        Assert.IsTrue(
                            new String[0].SequenceEqual(strings));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key", ""},
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetStrings(out String[] strings));
                        Assert.IsTrue(
                            new String[0].SequenceEqual(strings));
                    }

                }

                {
                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", default(String)},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetStrings(out String[] strings));
                        Assert.IsTrue(
                            new[] { default(String), "2" }.SequenceEqual(strings));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", ""},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetStrings(out String[] strings));
                        Assert.IsTrue(
                            new[] { "", "2" }.SequenceEqual(strings));
                    }

                    {
                        var data = new Dictionary<String, String>
                        {
                            { "key:0", " "},
                            { "key:1", "2"}
                        };

                        var configuration = new ConfigurationBuilder()
                            .Add(new SomeConfigurationSource(data))
                            .Build() as IConfiguration;
                        var configurationSection = configuration.GetSection("key");

                        Assert.IsTrue(configurationSection.TryGetStrings(out String[] strings));
                        Assert.IsTrue(
                            new[] { " ", "2" }.SequenceEqual(strings));
                    }

                }

            }
            #endregion String

        }
    }
}
