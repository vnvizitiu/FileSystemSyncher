namespace FileSystemSyncher.Console
{
    using System;
    using System.IO;
    using Commons;

    public static class Program
    {
        public static void Main()
        {
            try
            {
                IConfigurationProvider configurationProvider = new ConfigFileConfigurationProvider();
                ConfigurationOptions configurationOptions = configurationProvider.GetOptions();

                if (!configurationOptions.DestinationDirectory.Exists)
                {
                    configurationOptions.DestinationDirectory.Create();
                }

                foreach (FileInfo sourceFile in configurationOptions.SourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
                {
                    string destinationFilePath = sourceFile.FullName.Replace(configurationOptions.SourceDirectory.FullName, configurationOptions.DestinationDirectory.FullName);
                    FileInfo destinationFile = new FileInfo(destinationFilePath);

                    if (!destinationFile.Exists || sourceFile.Length != destinationFile.Length ||
                        sourceFile.LastWriteTime != destinationFile.LastWriteTime)
                    {
                        if (destinationFile.DirectoryName != null)
                        {
                            Directory.CreateDirectory(destinationFile.DirectoryName);
                            File.Copy(sourceFile.FullName, destinationFilePath, true);
                            Console.WriteLine(destinationFilePath);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
