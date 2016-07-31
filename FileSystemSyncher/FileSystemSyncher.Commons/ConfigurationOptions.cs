namespace FileSystemSyncher.Commons
{
    using System.Configuration;
    using System.IO;

    public class ConfigurationOptions
    {
        public ConfigurationOptions(string sourceDirectory, string destinationDirectory)
        {
            string sourceFullPath = Path.GetFullPath(sourceDirectory);
            SourceDirectory = new DirectoryInfo(sourceFullPath);
            if (!SourceDirectory.Exists)
                throw new ConfigurationErrorsException("The folder in the source path does not exist");

            DestinationDirectory = new DirectoryInfo(destinationDirectory);
        }

        public DirectoryInfo SourceDirectory { get; }

        public DirectoryInfo DestinationDirectory { get; }
    }
}