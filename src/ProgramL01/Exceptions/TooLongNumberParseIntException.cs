using System;
using System.Runtime.Serialization;

namespace ESystem.Mentoring.ProgramL01.Exceptions
{

    [Serializable]
    public sealed class TooLongNumberParseIntException : Exception
    {
        public int[] Digits { get; }
        public int Basis { get; }

        public TooLongNumberParseIntException(int[] digits, int basis, Exception innerException)
            : base($"{digits?.Length} is too long for the {basis}-base system", innerException)
        {
            Digits = digits;
            Basis = basis;
        }

        public TooLongNumberParseIntException(int[] digits, int basis) : this(digits, basis, null)
        {
        }

        public TooLongNumberParseIntException(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Basis = info.GetInt32(nameof(Basis));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);

            info.AddValue(nameof(Basis), Basis);
        }
    }
}
