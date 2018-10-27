using System;
using System.Collections.Generic;
using System.Linq;

using ESystems.Mentoring.ProgramL01.Exceptions;

namespace ESystems.Mentoring.ProgramL01.Digitizer
{
    public sealed class ParseIntDigitizer : IParseIntDigitizer
    {
        private const char FirstChar = '0';
        private const char LastChar = '9';

        public IReadOnlyList<int> Digitize(string sample)
        {
            if (sample == null)
            {
                throw new ArgumentNullException(nameof(sample));
            }

            if (sample.Length == 0)
            {
                throw new EmptyStringParseIntException();
            }

            var chars = sample.ToCharArray();

            if (chars.Any(ch => !IsDigit(ch)))
            {
                throw new AggregateException(chars
                    .Where(ch => !IsDigit(ch))
                    .Select(ch => new NotDigitParseIntException(ch)));
            }

            return sample.Select(ch => ch - FirstChar).ToList();
        }

        private static bool IsDigit(char ch) => ch >= FirstChar && ch <= LastChar;
    }
}
