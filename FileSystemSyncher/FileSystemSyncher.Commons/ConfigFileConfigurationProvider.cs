namespace FileSystemSyncher.Commons
{
    using System;
    using System.Configuration;
    using System.Linq;

    public sealed class ConfigFileConfigurationProvider : IConfigurationProvider
    {
        public ConfigurationOptions GetOptions()
        {
            string sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            string destinationPath = ConfigurationManager.AppSettings["DestinationPath"];

            ConfigurationOptions configurationOptions = new ConfigurationOptions(sourcePath, destinationPath);

            configurationOptions.Whitelist = ConfigurationManager.AppSettings["Whitelist"]
                ?.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(path => path.ToLower());
            configurationOptions.BlackList = ConfigurationManager.AppSettings["BlackList"]
                ?.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(path => path.ToLower());

            return configurationOptions;
        }
    }
}