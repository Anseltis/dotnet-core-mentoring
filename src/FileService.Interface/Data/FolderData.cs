using System;

namespace ESystems.Mentoring.FileSystem.Interface.Data
{
    public sealed class FolderData : IEquatable<FolderData>
    {
        private readonly (string Name, string FullName) _tuple;

        public FolderData(string name, string fullName)
            => _tuple = (
                Name: name ?? throw new ArgumentNullException(nameof(name)),
                FullName: fullName ?? throw new ArgumentNullException(nameof(fullName)));

        public string Name => _tuple.Name;

        public string FullName => _tuple.FullName;

        public override bool Equals(object obj)
            => obj == this || obj is FolderData folderData && _tuple.Equals(folderData._tuple);

        public bool Equals(FolderData other)
            => other == this || other != null && _tuple.Equals(other._tuple);

        public override int GetHashCode() => _tuple.GetHashCode();

        public override string ToString() => FullName;
    }
}
