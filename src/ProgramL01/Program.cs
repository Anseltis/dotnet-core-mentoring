using System;

namespace ESystems.Mentoring.ProgramL01
{
    internal class Program
    {
        internal static void Main()
        {
            var assembler = new ParseIntAssembler(10);
            Console.WriteLine(assembler.Assembly(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
        }
    }
}
