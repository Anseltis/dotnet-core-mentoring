using System;
using System.Collections.Generic;
using System.Linq;

using ESystems.Mentoring.ProgramL01.Exceptions;

namespace ESystems.Mentoring.ProgramL01.Assembler
{
    public sealed class ParseIntAssembler : IParseIntAssembler
    {
        public ParseIntAssembler(int basis)
        {
            if (basis < 2)
            {
                throw new WrongBasisParseIntException(Basis);
            }

            Basis = basis;
            Capacity = (int)Math.Floor(Math.Log(long.MaxValue) / Math.Log(Basis));
        }

        public int Basis { get; }

        public int Capacity { get; }

        public long Assembly(IReadOnlyList<int> digits)
        {
            if (digits == null)
            {
                throw new ArgumentNullException(nameof(digits));
            }

            if (digits.Count > Capacity)
            {
                throw new TooLongNumberParseIntException(digits.Count, Basis);
            }

            if (digits.Any(digit => !IsValidDigit(digit)))
            {
                throw new AggregateException(digits
                        .Where(digit => !IsValidDigit(digit))
                        .Select(digit => new WrongDigitParseIntException(digit, Basis)));
            }

            return digits
                .Aggregate(
                    new ParseIntSeed(),
                    (seed, digit) => seed.Concat(new ParseIntSeed(digit, Basis)),
                    seed => seed.Value);
        }

        private bool IsValidDigit(int digit) => digit >= 0 && digit < Basis;
    }
}
