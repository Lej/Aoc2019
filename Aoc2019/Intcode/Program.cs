using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Aoc2019.Intcode
{
    public class Program
    {
        private static int NextProgramId = 0;

        public int ProgramId { get; } = NextProgramId++;
        public Dictionary<BigInteger, BigInteger> Memory { get; private set; }
        public Stream<BigInteger> Input { get; set; } = new Stream<BigInteger>();
        public Stream<BigInteger> Output { get; set; } = new Stream<BigInteger>();

        private BigInteger _instructionPointer = 0;
        private BigInteger _relativeBase = 0;

        public Program(string input)
        {
            Memory = input.Split(",")
                .Select((x, i) => new
                {
                    Index = new BigInteger(i),
                    Value = BigInteger.Parse(x)
                })
                .ToDictionary(x => x.Index, x => x.Value);
        }

        public Program Pipe(Program destination)
        {
            destination.Input = Output;
            return destination;
        }

        public async Task ExecuteAsync()
        {
            await Task.Run(Execute);
        }

        public void Execute()
        {
            while (true)
            {
                var value = Memory.GetValueOrDefault(_instructionPointer, BigInteger.Zero);
                var instruction = new Instruction(value);
                var stop = Execute(instruction);
                if (stop) break;
            }
        }

        private bool Execute(Instruction instruction)
        {
            switch (instruction.OpCode)
            {
                case OpCode.Addition:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        Write(instruction, 2, p0 + p1);
                        _instructionPointer += 4;
                        return false;
                    }
                case OpCode.Multiplication:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        Write(instruction, 2, p0 * p1);
                        _instructionPointer += 4;
                        return false;
                    }
                case OpCode.Input:
                    {
                        Write(instruction, 0, Input.Read());
                        _instructionPointer += 2;
                        return false;
                    }
                case OpCode.Output:
                    {
                        var p0 = Read(instruction, 0);
                        Output.Write(p0);
                        _instructionPointer += 2;
                        return false;
                    }
                case OpCode.JumpIfTrue:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        if (p0 != 0)
                        {
                            _instructionPointer = p1;
                        }
                        else
                        {
                            _instructionPointer += 3;
                        }
                        return false;
                    }
                case OpCode.JumpIfFalse:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        if (p0 == 0)
                        {
                            _instructionPointer = p1;
                        }
                        else
                        {
                            _instructionPointer += 3;
                        }
                        return false;
                    }
                case OpCode.LessThan:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        Write(instruction, 2, p0 < p1 ? 1 : 0);
                        _instructionPointer += 4;
                        return false;
                    }
                case OpCode.Equals:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        Write(instruction, 2, p0 == p1 ? 1 : 0);
                        _instructionPointer += 4;
                        return false;
                    }
                case OpCode.AdjustRelativeBase:
                    {
                        var p0 = Read(instruction, 0);
                        _relativeBase += p0;
                        _instructionPointer += 2;
                        return false;
                    }
                case OpCode.Halt:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        private BigInteger Read(Instruction instruction, int parameterIndex)
        {
            var index = _instructionPointer + parameterIndex + 1;
            var immediate = Memory.GetValueOrDefault(index, BigInteger.Zero);
            switch (instruction.GetMode(parameterIndex))
            {
                case Mode.Position:
                    {
                        var value = Memory.GetValueOrDefault(immediate, BigInteger.Zero);
                        return value;
                    }
                case Mode.Immediate:
                    {
                        return immediate;
                    }
                case Mode.Relative:
                    {
                        var value = Memory.GetValueOrDefault(_relativeBase + immediate, BigInteger.Zero);
                        return value;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        private void Write(Instruction instruction, int parameterIndex, BigInteger value)
        {
            var mode = instruction.GetMode(parameterIndex);
            var index = _instructionPointer + parameterIndex + 1;
            var position = Memory.GetValueOrDefault(index, BigInteger.Zero);
            switch (mode) {
                case Mode.Position:
                    Memory[position] = value;
                    break;
                case Mode.Relative:
                    Memory[_relativeBase + position] = value;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return $"ProgramId={ProgramId}";
        }
    }
}
