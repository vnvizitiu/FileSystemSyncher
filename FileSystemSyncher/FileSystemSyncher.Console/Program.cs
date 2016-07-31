namespace FileSystemSyncher.Console
{
    using System;
    using System.Configuration;
    using System.IO;

    public static class Program
    {
        public static void Main()
        {
            try
            {
                string sourcePath = ConfigurationManager.AppSettings["SourcePath"];
                if (!string.IsNullOrWhiteSpace(sourcePath))
                {
                    sourcePath = Path.GetFullPath(sourcePath);
                    if (!Directory.Exists(sourcePath))
                    {
                        throw new ConfigurationErrorsException("The folder in the source path does not exist");
                    }
                }
                else
                {
                    throw new ConfigurationErrorsException("There was no source path set");
                }

                string destinationPath = ConfigurationManager.AppSettings["DestinationPath"];
                if (!string.IsNullOrWhiteSpace(destinationPath))
                {
                    destinationPath = Path.GetFullPath(destinationPath);
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }
                }
                else
                {
                    throw new ConfigurationErrorsException("There was no destination path set");
                }

                DirectoryInfo sourceDirectory = new DirectoryInfo(sourcePath);
                foreach (FileInfo sourceFile in sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
                {
                    string destinationFilePath = sourceFile.FullName.Replace(sourcePath, destinationPath);
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
