using System;
using System.Runtime.Serialization;

namespace ESystems.Mentoring.ProgramL01.Exceptions
{
    [Serializable]
    public sealed class EmptyStringParseIntException : Exception
    {
        public EmptyStringParseIntException(Exception innerException)
            : base($"String is empty", innerException)
        {
        }

        public EmptyStringParseIntException()
            : this(null)
        {
        }

        private EmptyStringParseIntException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
