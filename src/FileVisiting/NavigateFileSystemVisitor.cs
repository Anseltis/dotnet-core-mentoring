using System;
using System.Collections.Generic;

using ESystems.Mentoring.FileSystem.Interface.Data;
using ESystems.Mentoring.FileVisiting.Events;

namespace ESystems.Mentoring.FileVisiting
{
    public sealed class NavigateFileSystemVisitor : IFileSystemVisitor
    {
        private readonly IFileSystemVisitor _fileSystemVisitor;

        public NavigateFileSystemVisitor(FileSystemVisitor fileSystemVisitor)
        {
            _fileSystemVisitor = fileSystemVisitor ?? throw new ArgumentNullException(nameof(fileSystemVisitor));
        }

        public event EventHandler<FileVisitEventArgs> Start;

        public event EventHandler<FileVisitEventArgs> Finish;

        public IEnumerable<FileData> Visit(string path)
        {
            OnStart(new FileVisitEventArgs(path));

            try
            {
                foreach (var file in _fileSystemVisitor.Visit(path))
                {
                    yield return file;
                }
            }
            finally
            {
                OnFinish(new FileVisitEventArgs(path));
            }
        }

        private void OnStart(FileVisitEventArgs args) => Start?.Invoke(this, args);

        private void OnFinish(FileVisitEventArgs args) => Finish?.Invoke(this, args);
    }
}
