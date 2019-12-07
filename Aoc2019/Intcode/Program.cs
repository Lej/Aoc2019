using System;
using System.Linq;
using System.Threading.Tasks;

namespace Aoc2019.Intcode
{
    public class Program
    {
        private static int NextProgramId = 0;

        public int ProgramId { get; } = NextProgramId++;
        public int[] Memory { get; private set; }
        public Stream<int> Input { get; set; } = new Stream<int>();
        public Stream<int> Output { get; set; } = new Stream<int>();

        private int _instructionPointer = 0;

        public Program(string input)
        {
            Memory = input.Split(",").Select(int.Parse).ToArray();
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
                var instruction = new Instruction(Memory[_instructionPointer]);
                var stop = ExecuteInstruction(instruction);
                if (stop) break;
            }
        }

        private bool ExecuteInstruction(Instruction instruction)
        {
            switch (instruction.OpCode)
            {
                case OpCode.Addition:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        Write(2, p0 + p1);
                        _instructionPointer += 4;
                        return false;
                    }
                case OpCode.Multiplication:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        Write(2, p0 * p1);
                        _instructionPointer += 4;
                        return false;
                    }
                case OpCode.Input:
                    {
                        Write(0, Input.Read());
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
                        Write(2, p0 < p1 ? 1 : 0);
                        _instructionPointer += 4;
                        return false;
                    }
                case OpCode.Equals:
                    {
                        var p0 = Read(instruction, 0);
                        var p1 = Read(instruction, 1);
                        Write(2, p0 == p1 ? 1 : 0);
                        _instructionPointer += 4;
                        return false;
                    }
                case OpCode.Halt:
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        private int Read(Instruction instruction, int parameterIndex)
        {
            var index = _instructionPointer + parameterIndex + 1;
            var immediate = Memory[index];
            switch (instruction.GetMode(parameterIndex))
            {
                case Mode.Position:
                    var value = Memory[immediate];
                    return value;
                case Mode.Immediate:
                    return immediate;
                default:
                    throw new NotImplementedException();
            }
        }

        private void Write(int parameterIndex, int value)
        {
            var index = _instructionPointer + parameterIndex + 1;
            var position = Memory[index];
            Memory[position] = value;
        }

        public override string ToString()
        {
            return $"ProgramId={ProgramId}";
        }
    }
}
