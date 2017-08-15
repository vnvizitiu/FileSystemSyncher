namespace FileSystemSyncher.Commons
{
    using System;
    using System.IO;

    public abstract class FileProcessingStrategyBase
    {
        public event EventHandler OnFileProcessed;

        public void ProcessFiles(FileInfo sourceFileInfo, FileInfo destinationFileInfo)
        {
            ProcessFilesInternal(sourceFileInfo, destinationFileInfo);
            OnFileProcessed?.Invoke(this, EventArgs.Empty);
        }

        protected abstract void ProcessFilesInternal(FileInfo sourceFileInfo, FileInfo destinationFileInfo);
    }
}