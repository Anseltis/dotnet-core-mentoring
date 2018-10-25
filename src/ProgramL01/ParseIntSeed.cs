namespace ESystems.Mentoring.ProgramL01
{
    public class ParseIntSeed
    {
        public ParseIntSeed()
        {
            Value = 0;
            Basis = 1;
        }

        public ParseIntSeed(long value, long basis)
        {
            Value = value;
            Basis = basis;
        }

        public long Value { get; }

        public long Basis { get; }
    }
}
