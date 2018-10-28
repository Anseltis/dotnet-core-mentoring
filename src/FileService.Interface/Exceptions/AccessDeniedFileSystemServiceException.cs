using System;
using System.Runtime.Serialization;

namespace ESystems.Mentoring.FileSystem.Interface.Exceptions
{
    [Serializable]
    public sealed class AccessDeniedFileSystemServiceException : FileSystemServiceException
    {
        public AccessDeniedFileSystemServiceException(string path, Exception innerException)
            : base($"Access is denied to '{path}'", innerException)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public AccessDeniedFileSystemServiceException(string path)
            : this(path, null)
        {
        }

        private AccessDeniedFileSystemServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Path = info.GetString(nameof(Path));
        }

        public string Path { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);

            info.AddValue(nameof(Path), Path);
        }
    }
}
