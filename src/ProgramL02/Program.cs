using ESystems.Mentoring.FileSystem;
using ESystems.Mentoring.FileVisiting;
using ESystems.Mentoring.FileVisiting.Events;
using System;

namespace ESystems.Mentoring.ProgramL02
{
    internal sealed class Program
    {
        private static void Main()
        {
            var fileSystemVisitor = new FileSystemVisitor(new FileSystemService(), file => file.Name.EndsWith(".json"))
                .Wrap(visitor => new NavigateFileSystemVisitor(visitor))
                .Initialize(visitor => visitor.Start += OnFileSystemVisitorOnStart)
                .Initialize(visitor => visitor.Finish += OnFileSystemVisitorOnFinish)
                .Wrap(visitor => new FilterFileSystemVisitor(visitor))
                .Initialize(visitor => visitor.Exclude += FileSystemVisitorOnExclude);

            foreach (var result in fileSystemVisitor.Visit(@"D:\JSS\node_modules")) Console.WriteLine(result);
        }

        private static void FileSystemVisitorOnExclude(object sender, FileExcludeEventArgs e)
        {
            if (e.File.FullName.Contains("Copy"))
            {
                e.Action = FileExcludeAction.Exclude;
            }

            if (e.File.FullName.Contains("buffer"))
            {
                e.Action = FileExcludeAction.Stop;
            }
        }

        private static void OnFileSystemVisitorOnFinish(object sender, FileVisitEventArgs eventArgs)
        {
            Console.WriteLine($"finished - {eventArgs.Path}");
        }

        private static void OnFileSystemVisitorOnStart(object sender, FileVisitEventArgs eventArgs)
        {
            Console.WriteLine($"on start - {eventArgs.Path}");
        }

    }
}
