using System.Collections.Generic;
using ESystems.Mentoring.FileSystem.Interface.Data;

namespace ESystems.Mentoring.FileSystem.Interface
{
    public interface IFileSystemService
    {
        IEnumerable<FileData> GetFiles(string path);

        IEnumerable<FolderData> GetFolders(string path);
    }
}