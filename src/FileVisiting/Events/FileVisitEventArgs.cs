using System;

namespace ESystems.Mentoring.FileVisiting.Events
{
    public sealed class FileVisitEventArgs
    {
        public FileVisitEventArgs(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public string Path { get; }
    }
}