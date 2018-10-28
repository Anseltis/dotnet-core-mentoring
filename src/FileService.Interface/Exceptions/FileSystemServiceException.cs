using System;
using System.Runtime.Serialization;

namespace ESystems.Mentoring.FileSystem.Interface.Exceptions
{
    public abstract class FileSystemServiceException : Exception
    {
        protected FileSystemServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected FileSystemServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
