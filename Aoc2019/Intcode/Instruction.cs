using Aoc2019.Extensions;
using System.Linq;

namespace Aoc2019.Intcode
{
    public class Instruction
    {
        public OpCode OpCode { get; }

        private Mode[] _modes;

        public Instruction(int instruction)
        {
            var digits = instruction.ToDigits();
            var opCode = (OpCode)digits.TakeLast(2).ToInt();
            OpCode = opCode;

            var modes = digits.Reverse().Skip(2).Select(x => (Mode)x).ToArray();
            _modes = modes;
        }

        public Mode GetMode(int parameterIndex)
        {
            if (parameterIndex < _modes.Length)
            {
                return _modes[parameterIndex];
            }
            return Mode.Position;
        }

        public override string ToString()
        {
            return $"{OpCode}({string.Join(", ", _modes)})";
        }
    }
}
