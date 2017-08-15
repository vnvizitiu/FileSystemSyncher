namespace FileSystemSyncher.Commons
{
    using System;
    using System.IO;

    public abstract class FileProcessingStrategyBase
    {
        public event EventHandler OnFileProcessed;

        public abstract void ProcessFiles(FileInfo sourceFileInfo, FileInfo destinationFileInfo);
    }
}