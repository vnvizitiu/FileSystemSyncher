namespace FileSystemSyncher.Commons
{
    using System;
    using System.IO;

    public sealed class CopyReportStrategy : FileProcessingStrategyBase
    {
        protected override void ProcessFilesInternal(FileInfo sourceFileInfo, FileInfo destinationFileInfo)
        {
            if (!destinationFileInfo.Exists)
            {
                Console.WriteLine($"{destinationFileInfo.FullName} [+](new)");
            }
            else
            {
                Console.WriteLine($"{destinationFileInfo.FullName} [!](update)");
            }
        }
    }
}