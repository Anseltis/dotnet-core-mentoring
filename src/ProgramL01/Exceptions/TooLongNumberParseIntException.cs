using System;
using System.Runtime.Serialization;

namespace ESystems.Mentoring.ProgramL01.Exceptions
{
    [Serializable]
    public sealed class TooLongNumberParseIntException : Exception
    {
        public TooLongNumberParseIntException(int length, int basis, Exception innerException)
            : base($"{length} is too long for the {basis}-base system", innerException)
        {
            Length = length;
            Basis = basis;
        }

        public TooLongNumberParseIntException(int length, int basis)
            : this(length, basis, null)
        {
        }

        private TooLongNumberParseIntException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Basis = info.GetInt32(nameof(Basis));
            Length = info.GetInt32(nameof(Length));
        }

        public int Length { get; }

        public int Basis { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);

            info.AddValue(nameof(Basis), Basis);
            info.AddValue(nameof(Length), Length);
        }
    }
}
