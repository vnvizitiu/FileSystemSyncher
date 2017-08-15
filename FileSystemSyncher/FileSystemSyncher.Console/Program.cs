namespace FileSystemSyncher.Console
{
    using System;

    using Commons;

    public static class Program
    {
        public static void Main()
        {
            try
            {
                IConfigurationProvider configurationProvider = new ConfigFileConfigurationProvider();

                long numberOfFileToUpdate = 0;
                long numberOfFileUpdated = 0;

                CopyReportStrategy copyReportStrategy = new CopyReportStrategy();
                copyReportStrategy.OnFileProcessed += (sender, args) => numberOfFileToUpdate++;

                FileCopyStrategy fileCopyStrategy = new FileCopyStrategy();
                fileCopyStrategy.OnFileProcessed += (sender, args) =>
                    {
                        numberOfFileUpdated++;
                        Console.WriteLine(
                            $"[{numberOfFileToUpdate / numberOfFileToUpdate}] ({(numberOfFileUpdated / (double)numberOfFileToUpdate):P})");
                    };

                FileProcessingStrategyBase[] strategies = { copyReportStrategy, fileCopyStrategy };

                FileSystemProcessor fileSystemProcessor = new FileSystemProcessor(configurationProvider, strategies);
                fileSystemProcessor.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}
