namespace FileSystemSyncher.Tests.Mocks
{
    using Commons;

    public class MockConfigurationProvider : IConfigurationProvider
    {
        private readonly string _source;
        private readonly string _destination;

        public MockConfigurationProvider(string source, string destination)
        {
            _source = source;
            _destination = destination;
        }

        public ConfigurationOptions GetOptions()
        {
            return new ConfigurationOptions(_source, _destination);
        }
    }
}