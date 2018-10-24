using ESystem.Mentoring.ProgramL01.Exceptions;
using System;
using System.Linq;

namespace ESystems.Mentoring.ProgramL01
{
    class ParseIntAssembler
    {
        public int Basis { get; }
        public int Capacity { get; }

        public ParseIntAssembler(int basis)
        {
            if (basis < 2)
            {
                throw new WrongBasisParseIntException(Basis);
            }

            Basis = basis;
            Capacity = (int)Math.Floor(Math.Log(long.MaxValue) / Math.Log(Basis));
        }

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
                .Aggregate(new ParseIntSeed(), (seed, digit) => seed.Concat(new ParseIntSeed(digit, Basis)))
                .Value;
        }

        private bool IsDigit(int digit) => digit >= 0 && digit < Basis;
    }
}
