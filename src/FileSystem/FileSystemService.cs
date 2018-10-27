using System.Collections.Generic;
using System.IO;
using System.Linq;

using ESystems.Mentoring.FileSystem.Interface;
using ESystems.Mentoring.FileSystem.Interface.Data;

namespace ESystems.Mentoring.FileSystem
{
    public sealed class FileSystemService : IFileSystemService
    {
        public IEnumerable<FileData> GetFiles(string path)
            => new DirectoryInfo(path).GetFiles()
                .Select(file => new FileData(name: file.Name, fullName: file.FullName));

        public IEnumerable<FolderData> GetFolders(string path)
            => new DirectoryInfo(path).GetDirectories()
                .Select(folder => new FolderData(name: folder.Name, fullName: folder.FullName));
    }
}
