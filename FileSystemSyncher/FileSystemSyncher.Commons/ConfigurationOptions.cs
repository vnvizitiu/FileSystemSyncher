namespace FileSystemSyncher.Commons
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    public sealed class ConfigurationOptions
    {
        public ConfigurationOptions(string sourceDirectory, string destinationDirectory)
        {
            string sourceFullPath = Path.GetFullPath(sourceDirectory).TrimEnd('\\');
            SourceDirectory = new DirectoryInfo(sourceFullPath);
            if (!SourceDirectory.Exists)
            {
                throw new ConfigurationErrorsException("The folder in the source path does not exist");
            }

            string destinationPath = destinationDirectory;
            if (!string.IsNullOrWhiteSpace(destinationDirectory))
            {
                destinationPath = destinationPath.TrimEnd('\\');
            }
            
            DestinationDirectory = new DirectoryInfo(destinationPath);

            Whitelist = Enumerable.Empty<string>();
            BlackList = Enumerable.Empty<string>();
        }

        public DirectoryInfo SourceDirectory { get; }

        public DirectoryInfo DestinationDirectory { get; }

        public IEnumerable<string> Whitelist { get; set; }

        public IEnumerable<string> BlackList { get; set; }
    }
}