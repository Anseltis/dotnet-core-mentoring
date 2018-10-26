using System;
using ESystems.Mentoring.ProgramL01.Assembler;
using ESystems.Mentoring.ProgramL01.Digitizer;
using ESystems.Mentoring.ProgramL01.Exceptions;

namespace ESystems.Mentoring.ProgramL01
{
    public sealed class ParseIntConverter
    {
        private readonly IParseIntAssembler _assembler;

        private readonly IParseIntDigitizer _digitizer;

        public ParseIntConverter(IParseIntDigitizer digitizer, IParseIntAssembler assembler)
        {
            _digitizer = digitizer ?? throw new ArgumentNullException(nameof(digitizer));
            _assembler = assembler ?? throw new ArgumentNullException(nameof(assembler));
        }

        public long ToInt(string sample)
        {
            try
            {
                var digits = _digitizer.Digitize(sample);
                return _assembler.Assembly(digits);
            }
            catch (ParseIntException exception)
            {
                throw new InvalidCastException($"Wrong parsing of {sample}", exception);
            }
        }
    }
}
