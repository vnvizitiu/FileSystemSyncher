namespace FileSystemSyncher.Commons
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal sealed class FileSystemEnumerator
    {
        private readonly IEnumerable<string> _whitelist;

        private readonly IEnumerable<string> _blackList;

        private FileSystemEnumerator(IEnumerable<string> whitelist, IEnumerable<string> blackList)
        {
            _whitelist = whitelist;
            _blackList = blackList;
        }

        public static FileSystemEnumerator CreateInstance(IEnumerable<string> whitelist, IEnumerable<string> blackList)
        {
            whitelist = whitelist ?? Enumerable.Empty<string>();
            blackList = blackList ?? Enumerable.Empty<string>();

            return new FileSystemEnumerator(whitelist, blackList);
        }

        internal IEnumerable<FileInfo> EnumerateFileBreathFirst(DirectoryInfo directoryInfo)
        {
            foreach (FileInfo file in directoryInfo.EnumerateFiles())
            {
                if (_whitelist.Any(path => IsFileWhitelisted(path, file)))
                {
                    yield return file;
                }

                if (_blackList.Any(path => IsFileBlacklisted(path, file)))
                {
                    continue;
                }

                yield return file;
            }

            foreach (DirectoryInfo subDir in directoryInfo.EnumerateDirectories()
                .Where(info => !info.Attributes.HasFlag(FileAttributes.System)))
            {
                if (ShouldEnumerateDirectory(subDir))
                {
                    foreach (FileInfo file in EnumerateFileBreathFirst(subDir))
                    {
                        yield return file;
                    }
                }
            }
        }

        private bool IsFileBlacklisted(string path, FileInfo file)
        {
            if (IsFileOrExtensionPresent(path, file))
            {
                return true;
            }

            return false;
        }

        private bool IsFileWhitelisted(string path, FileInfo file)
        {
            if (IsFileOrExtensionPresent(path, file))
            {
                return true;
            }

            return false;
        }

        private bool IsFileOrExtensionPresent(string path, FileInfo file)
        {
            if (path.Equals(file.Name, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            if (path.Equals(file.FullName, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            if (path.Equals(file.Extension, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private bool ShouldEnumerateDirectory(DirectoryInfo subDir)
        {
            bool shouldEnumerate = true;

            if (_blackList.Any(path => path.Equals(subDir.Name, StringComparison.InvariantCultureIgnoreCase) || path.Equals(subDir.FullName, StringComparison.InvariantCultureIgnoreCase)))
            {
                shouldEnumerate = false;
            }

            if (_whitelist.Any(path => path.ToLower().StartsWith(subDir.FullName, StringComparison.InvariantCultureIgnoreCase)))
            {
                shouldEnumerate = true;
            }

            return shouldEnumerate;
        }
    }
}