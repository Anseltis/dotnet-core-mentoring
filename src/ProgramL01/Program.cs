using System;

namespace ESystems.Mentoring.ProgramL01
{
    class Program
    {
        static void Main()
        {
            var assembler = new ParseIntAssembler(10);
            Console.WriteLine(assembler.Assembly(new[] { 1, 2, 3, 4, 5 }));
        }
    }
}
