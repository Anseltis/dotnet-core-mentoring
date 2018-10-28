using System;
using System.Runtime.Serialization;

namespace ESystems.Mentoring.ProgramL01.Exceptions
{
    [Serializable]
    public sealed class WrongBasisParseIntException : ParseIntException
    {
        public WrongBasisParseIntException(int basis, Exception innerException)
            : base($"{basis} is wrong for digital system", innerException)
        {
            Basis = basis;
        }

        public WrongBasisParseIntException(int basis)
            : this(basis, null)
        {
        }

        private WrongBasisParseIntException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Basis = info.GetInt32(nameof(Basis));
        }

        public int Basis { get; }

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
