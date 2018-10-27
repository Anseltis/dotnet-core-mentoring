using System;
using ESystems.Mentoring.FileSystem.Interface.Data;

namespace ESystems.Mentoring.FileVisiting
{
    public class FileExcludeEventArgs
    {
        public FileExcludeEventArgs(FileData file, FileExcludeAction action)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
            Action = action;
        }

        public FileData File { get; }

        public FileExcludeAction Action { get; set; }
    }
}