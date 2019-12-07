using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2019.Intcode
{
    public class Program
    {
        public IReadOnlyList<int> Memory => Array.AsReadOnly(_memory);

        private readonly int[] _memory;
        private int _instructionPointer = 0;
        private Queue<int> _input;
        private Queue<int> _output;

        public Program(string input)
        {
            _memory = input.Split(",").Select(int.Parse).ToArray();
        }

        public Program(int[] memory)
        {
            _memory = memory;
        }

        public void SetMemory(int index, int value)
        {
            _memory[index] = value;
        }

        public int GetMemory(int index)
        {
            return _memory[index];
        }

        public void Execute(Queue<int> input = null, Queue<int> output = null)
        {
            _input = input;
            _output = output;

            while (true)
            {
                var instruction = new Instruction(_memory[_instructionPointer]);
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
                        Write(0, _input.Dequeue());
                        _instructionPointer += 2;
                        return false;
                    }
                case OpCode.Output:
                    {
                        var p0 = Read(instruction, 0);
                        _output.Enqueue(p0);
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
            var immediate = _memory[index];
            switch (instruction.GetMode(parameterIndex))
            {
                case Mode.Position:
                    var value = _memory[immediate];
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
            var position = _memory[index];
            _memory[position] = value;
        }
    }
}
