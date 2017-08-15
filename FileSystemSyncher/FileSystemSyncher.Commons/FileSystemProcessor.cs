namespace FileSystemSyncher.Commons
{
    using System;
    using System.IO;

    public sealed class FileSystemProcessor
    {
        private readonly IConfigurationProvider _configurationProvider;

        public FileSystemProcessor(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider
                                     ?? throw new ArgumentNullException(nameof(configurationProvider));
        }

        public void Run()
        {
            ConfigurationOptions configurationOptions = _configurationProvider.GetOptions();
            FileSystemEnumerator fileSystemEnumerator = FileSystemEnumerator.CreateInstance();

            foreach (FileInfo sourceFile in fileSystemEnumerator.EnumerateFileBreathFirst(
                configurationOptions.SourceDirectory))
            {
                string destinationFilePath = sourceFile.FullName.Replace(
                    configurationOptions.SourceDirectory.FullName,
                    configurationOptions.DestinationDirectory.FullName);

                FileInfo destinationFile = new FileInfo(destinationFilePath);

                if (!destinationFile.Exists || sourceFile.Length != destinationFile.Length
                    || sourceFile.LastWriteTime != destinationFile.LastWriteTime)
                {
                    if (destinationFile.DirectoryName != null)
                    {
                        Directory.CreateDirectory(destinationFile.DirectoryName);
                        File.Copy(sourceFile.FullName, destinationFilePath, true);
                        System.Console.WriteLine(destinationFilePath);
                    }
                }
            }
        }
    }
}