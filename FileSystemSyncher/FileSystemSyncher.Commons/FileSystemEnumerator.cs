namespace FileSystemSyncher.Commons
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal sealed class FileSystemEnumerator
    {
        internal static FileSystemEnumerator CreateInstance()
        {
            return new FileSystemEnumerator();
        }

        internal IEnumerable<FileInfo> EnumerateFileBreathFirst(DirectoryInfo directoryInfo)
        {
            foreach (FileInfo file in directoryInfo.EnumerateFiles())
            {
                yield return file;
            }

            foreach (DirectoryInfo subDir in directoryInfo.EnumerateDirectories()
                .Where(info => !info.Attributes.HasFlag(FileAttributes.System)))
            {
                foreach (FileInfo file in EnumerateFileBreathFirst(subDir))
                {
                    yield return file;
                }
            }
        }
    }
}