namespace FileSystemSyncher.Commons
{
    using System;
    using System.IO;

    public abstract class FileProcessingStrategyBase
    {
        public event EventHandler OnFileProcessed;

        public void ProcessFiles(FileInfo sourceFileInfo, FileInfo destinationFileInfo)
        {
            sourceFileInfo = sourceFileInfo ?? throw new ArgumentNullException(nameof(sourceFileInfo));
            destinationFileInfo = destinationFileInfo ?? throw new ArgumentNullException(nameof(destinationFileInfo));

            ProcessFilesInternal(sourceFileInfo, destinationFileInfo);
            OnFileProcessed?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void ProcessFilesInternal(FileInfo sourceFileInfo, FileInfo destinationFileInfo);
    }
}