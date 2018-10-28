using System;
using System.Runtime.Serialization;

namespace ESystems.Mentoring.ProgramL01.Exceptions
{
    public abstract class ParseIntException : Exception
    {
        protected ParseIntException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ParseIntException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
