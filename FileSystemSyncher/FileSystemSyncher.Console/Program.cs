using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace FileSystemSyncher.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sourcePath = ConfigurationManager.AppSettings["SourcePath"];
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

                var destinationPath = ConfigurationManager.AppSettings["DestinationPath"];
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

                var sourceDirectory = new DirectoryInfo(sourcePath);
                foreach (var sourceFile in sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
                {
                    var destinationFilePath = sourceFile.FullName.Replace(sourcePath, destinationPath);
                    var destinationFile = new FileInfo(destinationFilePath);

                    if (!destinationFile.Exists || sourceFile.Length != destinationFile.Length ||
                        sourceFile.LastWriteTime != destinationFile.LastWriteTime)
                    {
                        Directory.CreateDirectory(destinationFile.DirectoryName);
                        File.Copy(sourceFile.FullName, destinationFilePath, true);
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}
