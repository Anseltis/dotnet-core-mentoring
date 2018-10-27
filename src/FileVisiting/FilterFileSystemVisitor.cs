using System;
using System.Collections.Generic;

using ESystems.Mentoring.FileSystem.Interface.Data;

namespace ESystems.Mentoring.FileVisiting
{
    public sealed class FilterFileSystemVisitor : IFileSystemVisitor
    {
        private readonly IFileSystemVisitor _fileSystemVisitor;

        public FilterFileSystemVisitor(IFileSystemVisitor fileSystemVisitor)
        {
            _fileSystemVisitor = fileSystemVisitor ?? throw new ArgumentNullException(nameof(fileSystemVisitor));
        }

        public event EventHandler<FileExcludeEventArgs> Exclude;

        public IEnumerable<FileData> Visit(string path)
        {
            foreach (var file in _fileSystemVisitor.Visit(path))
            {
                var fileExcludeEventArgs = new FileExcludeEventArgs(file, FileExcludeAction.Include);
                OnExclude(fileExcludeEventArgs);

                switch (fileExcludeEventArgs.Action)
                {
                    case FileExcludeAction.Exclude:
                        break;
                    case FileExcludeAction.Stop:
                        yield return file;
                        yield break;
                    case FileExcludeAction.Include:
                        yield return file;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private void OnExclude(FileExcludeEventArgs e)
        {
            Exclude?.Invoke(this, e);
        }
    }
}
