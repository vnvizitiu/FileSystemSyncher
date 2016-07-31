namespace FileSystemSyncher.Tests
{
    using System;
    using System.Configuration;
    using Commons;
    using FluentAssertions;
    using Mocks;
    using NUnit.Framework;

    [TestFixture]
    public class ConfigurationTests
    {
        [TestCase("C:\\", "C:\\")]
        [TestCase("C:\\", null, ExpectedException = typeof(ArgumentNullException))]
        [TestCase("C:\\", "", ExpectedException = typeof(ArgumentException))]
        [TestCase("RandomPath", "C:\\", ExpectedException = typeof(ConfigurationErrorsException))]
        [TestCase("C:\\RandomPath", "C:\\", ExpectedException = typeof(ConfigurationErrorsException))]
        [TestCase(null, "C:\\", ExpectedException = typeof(ArgumentNullException))]
        [TestCase("", "C:\\", ExpectedException = typeof(ArgumentException))]
        public void CreateConfiguration(string source, string destination)
        {
            IConfigurationProvider configurationProvider = new MockConfigurationProvider(source, destination);
            ConfigurationOptions configurationOptions = configurationProvider.GetOptions();
            configurationOptions.Should().NotBeNull("because we provided to the Mock proper paths");
            configurationOptions.SourceDirectory.Should().NotBeNull("because the source directory path was provided");
            configurationOptions.SourceDirectory.Exists.Should().BeTrue("because the source directory exists");
        }
    }
}
