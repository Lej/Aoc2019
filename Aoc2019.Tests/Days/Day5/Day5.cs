using System;
using System.Collections.Generic;
using System.Linq;
using Aoc2019.Tests.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aoc2019.Tests.Days.Day5
{
    [TestClass]
    public class Day5
    {
        [TestMethod]
        public void SolvePart1()
        {
            var input = new Queue<int>();
            input.Enqueue(1);
            var output = new Queue<int>();

            var text = EmbeddedResourceUtils.ReadToEnd("Day5.txt");
            var program = new Program(text);
            program.Execute(input, output);

            output.Reverse().Skip(1).ForEach(x => Assert.AreEqual(0, x));
            var code = output.Reverse().Take(1).Single();
            Console.WriteLine(code);
            Assert.AreEqual(13547311, code);
        }

        [TestMethod]
        public void SolvePart2()
        {
            var input = new Queue<int>();
            input.Enqueue(5);
            var output = new Queue<int>();

            var text = EmbeddedResourceUtils.ReadToEnd("Day5.txt");
            var program = new Program(text);
            program.Execute(input, output);

            Assert.AreEqual(1, output.Count);
            var code = output.Dequeue();
            Console.WriteLine(code);
            Assert.AreEqual(236453, code);
        }

        [DataTestMethod]
        [DataRow("1,0,0,0,99", new[] { 2, 0, 0, 0, 99 })]
        [DataRow("2,3,0,3,99", new[] { 2, 3, 0, 6, 99 })]
        [DataRow("2,4,4,5,99,0", new[] { 2, 4, 4, 5, 99, 9801 })]
        [DataRow("1,1,1,4,99,5,6,0,99", new[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [DataRow("1002,4,3,4,33", new[] { 1002, 4, 3, 4, 99 })]
        [DataRow("1101,100,-1,4,0", new[] { 1101, 100, -1, 4, 99 })]
        public void ProgramTest(string input, int[] output)
        {
            var program = new Program(input);
            program.Execute(null, null);
            var memory = program.Memory;
            Assert.That.SequenceEquals(output, memory.ToList());
        }

        [DataTestMethod]
        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", new[] { 1 }, new[] { 0 })] // 1 != 8 => 0
        [DataRow("3,9,8,9,10,9,4,9,99,-1,8", new[] { 8 }, new[] { 1 })] // 8 == 8 => 1

        [DataRow("3,9,7,9,10,9,4,9,99,-1,8", new[] { 1 }, new[] { 1 })] // 1 < 8 => 1
        [DataRow("3,9,7,9,10,9,4,9,99,-1,8", new[] { 8 }, new[] { 0 })] // 8 < 8 => 0
        [DataRow("3,9,7,9,10,9,4,9,99,-1,8", new[] { 10 }, new[] { 0 })] // 10 < 8 => 0

        [DataRow("3,3,1108,-1,8,3,4,3,99", new[] { 1 }, new[] { 0 })] // 1 != 8 => 0
        [DataRow("3,3,1108,-1,8,3,4,3,99", new[] { 8 }, new[] { 1 })] // 8 == 8 => 1

        [DataRow("3,3,1107,-1,8,3,4,3,99", new[] { 1 }, new[] { 1 })] // 1 < 8 => 1
        [DataRow("3,3,1107,-1,8,3,4,3,99", new[] { 8 }, new[] { 0 })] // 8 < 8 => 0
        [DataRow("3,3,1107,-1,8,3,4,3,99", new[] { 10 }, new[] { 0 })] // 10 < 8 => 0

        [DataRow("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", new[] { 0 }, new[] { 0 })] // Jump test: 0 -> 0
        [DataRow("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", new[] { 1 }, new[] { 1 })] // Jump test: 1 -> 1
        [DataRow("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", new[] { 2 }, new[] { 1 })] // Jump test: 1 -> 1

        [DataRow("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", new[] { 0 }, new[] { 0 })] // Jump test: 0 -> 0
        [DataRow("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", new[] { 1 }, new[] { 1 })] // Jump test: 1 -> 1
        [DataRow("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", new[] { 2 }, new[] { 1 })] // Jump test: 1 -> 1

        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", new[] { 7 }, new[] { 999 })] // 7 < 8 -> 999
        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", new[] { 8 }, new[] { 1000 })] // 8 == 8 -> 1000
        [DataRow("3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99", new[] { 9 }, new[] { 1001 })] // 9 > 8 -> 1001
        public void ProgramOutputTest(string programText, IEnumerable<int> actualInput, IEnumerable<int> expectedOutput)
        {
            var program = new Program(programText);
            var input = new Queue<int>();
            actualInput.ForEach(x => input.Enqueue(x));
            var output = new Queue<int>();
            program.Execute(input, output);
            Assert.That.SequenceEquals(expectedOutput, output);
        }

        

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

            public void Execute(Queue<int> input, Queue<int> output)
            {
                _input = input;
                _output = output;

                while(true)
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
                            } else
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
                            } else
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
            Halt = 99
        }

        public enum Mode
        {
            Position = 0,
            Immediate = 1
        }

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
}
