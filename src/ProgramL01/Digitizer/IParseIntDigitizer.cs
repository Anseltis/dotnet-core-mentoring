using System.Collections.Generic;

namespace ESystems.Mentoring.ProgramL01.Digitizer
{
    public interface IParseIntDigitizer
    {
        IReadOnlyList<int> Digitize(string sample);
    }
}