namespace ESystems.Mentoring.ProgramL01.Assembler
{
    public static class ParseIntSeedExtension
    {
        public static ParseIntSeed Concat(this ParseIntSeed left, ParseIntSeed right)
            => new ParseIntSeed(left.Value * right.Basis + right.Value, left.Basis * right.Basis);
    }
}
