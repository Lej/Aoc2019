namespace Aoc2019.Intcode
{
    public enum OpCode
    {
        Addition = 1,
        Multiplication = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        AdjustRelativeBase = 9,
        Halt = 99
    }
}
