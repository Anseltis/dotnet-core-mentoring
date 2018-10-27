using System;

namespace ESystems.Mentoring.FileVisiting
{
    public static class FileSystemVisitorExtension
    {
        public static TWrap Wrap<T, TWrap>(this T visitor, Func<T, TWrap> wrapper)
            where T : IFileSystemVisitor
            where TWrap : IFileSystemVisitor
            => wrapper(visitor);

        public static T Initialize<T>(this T visitor, Action<T> initializer)
            where T : IFileSystemVisitor
        {
            initializer(visitor);
            return visitor;
        }
    }
}
