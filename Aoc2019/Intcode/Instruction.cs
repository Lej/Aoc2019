using Aoc2019.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Aoc2019.Intcode
{
    public class Instruction
    {
        public OpCode OpCode { get; }

        private Dictionary<BigInteger, Mode> _modes;

        public Instruction(BigInteger value)
        {
            var digits = value.ToDigits();
            var opCode = (OpCode)digits.TakeLast(2).ToInt();
            OpCode = opCode;

            var modes = digits.Reverse().Skip(2)
                .Select((x, i) => new
                { 
                    Index = new BigInteger(i),
                    Mode = (Mode)x
                })
                .ToDictionary(x => x.Index, x => x.Mode);
            _modes = modes;
        }

        public Mode GetMode(BigInteger parameterIndex)
        {
            return _modes.GetValueOrDefault(parameterIndex, Mode.Position);
        }

        public override string ToString()
        {
            return $"{OpCode}({string.Join(", ", _modes)})";
        }
    }
}
