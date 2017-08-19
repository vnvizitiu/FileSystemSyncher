namespace FileSystemSyncher.Commons
{
    using System;
    using System.IO;

    public sealed class FileCopyStrategy : FileProcessingStrategyBase
    {
        protected override void ProcessFilesInternal(FileInfo sourceFileInfo, FileInfo destinationFileInfo)
        {
            if (string.IsNullOrWhiteSpace(destinationFileInfo.DirectoryName))
            {
                throw new ArgumentException("The destination file must be inside a directory", nameof(destinationFileInfo.DirectoryName));
            }

            Directory.CreateDirectory(destinationFileInfo.DirectoryName);
            File.Copy(sourceFileInfo.FullName, destinationFileInfo.FullName, true);
        }
    }
}