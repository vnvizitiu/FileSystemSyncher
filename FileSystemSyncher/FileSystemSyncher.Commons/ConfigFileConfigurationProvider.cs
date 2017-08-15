namespace FileSystemSyncher.Commons
{
    using System.Configuration;

    public sealed class ConfigFileConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationOptions GetOptions()
        {
            string sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            string destinationPath = ConfigurationManager.AppSettings["DestinationPath"];

            return new ConfigurationOptions(sourcePath, destinationPath);
        }
    }
}