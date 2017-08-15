namespace FileSystemSyncher.Commons
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public sealed class FileSystemProcessor
    {
        private readonly IEnumerable<FileProcessingStrategyBase> _strategies;

        private readonly IConfigurationProvider _configurationProvider;

        public FileSystemProcessor(IConfigurationProvider configurationProvider, IEnumerable<FileProcessingStrategyBase> strategies)
        {
            _configurationProvider = configurationProvider
                                     ?? throw new ArgumentNullException(nameof(configurationProvider));
            _strategies = strategies ?? Enumerable.Empty<FileProcessingStrategyBase>();
        }

        public void Run()
        {
            ConfigurationOptions configurationOptions = _configurationProvider.GetOptions();
            FileSystemEnumerator fileSystemEnumerator = FileSystemEnumerator.CreateInstance();

            foreach (FileProcessingStrategyBase fileProcessingStrategyBase in _strategies)
            {
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

                        fileProcessingStrategyBase.ProcessFiles(sourceFile, destinationFile);
                    }
                }
            }
        }
    }
}