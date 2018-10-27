using System.Collections.Generic;

using ESystems.Mentoring.FileSystem.Interface.Data;

namespace ESystems.Mentoring.FileVisiting
{
    public interface IFileSystemVisitor
    {
        IEnumerable<FileData> Visit(string path);
    }
}
