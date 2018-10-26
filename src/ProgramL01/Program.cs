using System;

using ESystems.Mentoring.ProgramL01.Assembler;
using ESystems.Mentoring.ProgramL01.Digitizer;

namespace ESystems.Mentoring.ProgramL01
{
    internal sealed class Program
    {
        internal static void Main()
        {
            var converter = new ParseIntConverter(new ParseIntDigitizer(), new ParseIntAssembler(10));
            Console.WriteLine(converter.ToInt("12458"));
        }
    }
}
