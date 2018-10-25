using System;
using System.Linq;

using ESystems.Mentoring.ProgramL01.Exceptions;

namespace ESystems.Mentoring.ProgramL01
{
    public class ParseIntAssembler
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

        public long Assembly(int[] digits)
        {
            if (digits == null)
            {
                throw new ArgumentNullException(nameof(digits));
            }

            if (digits.Length > Capacity)
            {
                throw new TooLongNumberParseIntException(digits, Basis);
            }

            if (digits.Any(digit => !IsDigit(digit)))
            {
                throw new AggregateException(
                    digits
                        .Where(IsDigit)
                        .Select(digit => new WrongDigitParseIntException(digit, Basis)));
            }

            return digits
                .Aggregate(
                    new ParseIntSeed(),
                    (seed, digit) => seed.Concat(new ParseIntSeed(digit, Basis)),
                    seed => seed.Value);
        }

        private bool IsDigit(int digit) => digit >= 0 && digit < Basis;
    }
}
