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
                FileSystemProcessor fileSystemProcessor = new FileSystemProcessor(configurationProvider);
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
