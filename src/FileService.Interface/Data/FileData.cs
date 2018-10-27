using System;

namespace ESystems.Mentoring.FileSystem.Interface.Data
{
    public sealed class FileData : IEquatable<FileData>
    {
        private readonly (string Name, string FullName) _tuple;

        public FileData(string name, string fullName)
            => _tuple = (
                Name: name ?? throw new ArgumentNullException(nameof(name)),
                FullName: fullName ?? throw new ArgumentNullException(nameof(fullName)));

        public string Name => _tuple.Name;

        public string FullName => _tuple.FullName;

        public override bool Equals(object obj)
            => obj == this || obj is FileData fileData && _tuple.Equals(fileData._tuple);

        public bool Equals(FileData other)
            => other == this || other != null && _tuple.Equals(other._tuple);

        public override int GetHashCode() => _tuple.GetHashCode();

        public override string ToString() => FullName;
    }
}
