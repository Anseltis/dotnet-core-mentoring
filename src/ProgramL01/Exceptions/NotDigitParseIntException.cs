using System;
using System.Runtime.Serialization;

namespace ESystems.Mentoring.ProgramL01.Exceptions
{
    [Serializable]
    public sealed class NotDigitParseIntException : ParseIntException
    {
        public NotDigitParseIntException(char character, Exception innerException)
            : base($"'{character}' is not a digit", innerException)
        {
            Character = Character;
        }

        public NotDigitParseIntException(char character)
            : this(character, null)
        {
        }

        private NotDigitParseIntException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            Character = info.GetChar(nameof(Character));
        }

        public char Character { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);

            info.AddValue(nameof(Character), Character);
        }
    }
}
