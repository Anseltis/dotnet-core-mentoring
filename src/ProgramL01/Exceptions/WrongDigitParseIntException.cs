﻿using System;
using System.Runtime.Serialization;

namespace ESystem.Mentoring.ProgramL01.Exceptions
{

    [Serializable]
    public sealed class WrongDigitParseIntException : Exception
    {
        public int Digit { get; }
        public int Basis { get; }

        public WrongDigitParseIntException(int digit, int basis, Exception innerException)
            : base($"{digit} is a wrong digit the {basis}-base system", innerException)
        {
            Digit = digit;
            Basis = basis;
        }

        public WrongDigitParseIntException(int digit, int basis) : this(digit, basis, null)
        {
        }

        public WrongDigitParseIntException(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Basis = info.GetInt32(nameof(Basis));
            Digit = info.GetInt32(nameof(Digit));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);

            info.AddValue(nameof(Basis), Basis);
            info.AddValue(nameof(Digit), Digit);
        }
    }
}
