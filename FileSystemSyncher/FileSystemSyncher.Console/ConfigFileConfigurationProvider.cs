namespace FileSystemSyncher.Console
{
    using System.Configuration;
    using Commons;

    public class ConfigFileConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationOptions GetOptions()
        {
            string sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            string destinationPath = ConfigurationManager.AppSettings["DestinationPath"];

            return new ConfigurationOptions(sourcePath, destinationPath);
        }
    }
}