using System.Collections.Generic;

namespace ESystems.Mentoring.ProgramL01.Assembler
{
    public interface IParseIntAssembler
    {
        long Assembly(IReadOnlyList<int> digits);
    }
}