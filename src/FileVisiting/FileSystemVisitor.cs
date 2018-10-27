using System;
using System.Collections.Generic;
using System.Linq;

using ESystems.Mentoring.FileSystem.Interface;
using ESystems.Mentoring.FileSystem.Interface.Data;

namespace ESystems.Mentoring.FileVisiting
{
    public sealed class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly Func<FileData, bool> _predicate;

        public FileSystemVisitor(IFileSystemService fileSystemService)
            : this(fileSystemService, file => true)
        {
        }

        public FileSystemVisitor(IFileSystemService fileSystemService, Func<FileData, bool> predicate)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public IEnumerable<FileData> Visit(string path)
            => _fileSystemService.GetFolders(path)
                .SelectMany(folder => _fileSystemService.GetFiles(folder.FullName))
                .Where(_predicate);
    }
}
