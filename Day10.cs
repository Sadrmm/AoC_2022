using System;
using System.ComponentModel;
using System.IO;

namespace AoC_2022
{
    internal class Day10
    {
        private class Instruction
        {
            private int _timeToComplete;
            private int _value;

            public int TimeToComplete => _timeToComplete;
            public int Value => _value;

            public Instruction(int timeToComplete)
            {
                _timeToComplete = timeToComplete;
                _value = 0;
            }
            public Instruction(int timeToComplete, int value)
            {
                _timeToComplete = timeToComplete;
                _value = value;
            }

            public override string ToString()
            {
                return $"Time: {TimeToComplete}, Value: {Value}";
            }
        }

        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer10.txt");
            
            // get all instructions
            Instruction[] instructions = new Instruction[input.Length];
            for (int i = 0; i < input.Length; i++) {
                string[] instructionLine = input[i].Split();

                instructions[i] = ParseToInstruction(instructionLine);
            }

            // do cycle loop and get sum
            int cycleToDetect = 20;
            int lastCycleToDetect = 220;
            int differenceBetweenCycles = 40;

            int cycle = 1;
            int registerX = 1;
            int signalsSum = 0;

            Instruction lastInstruction = instructions[0];
            cycle += lastInstruction.TimeToComplete;
            registerX += lastInstruction.Value;
            for (int i = 1; i < instructions.Length; i++) {
                lastInstruction = instructions[i-1];

                //check if we are in a cycle to sum
                if (cycle == cycleToDetect) {
                    signalsSum += registerX * cycleToDetect;

                    cycleToDetect += differenceBetweenCycles;
                    if (cycleToDetect > lastCycleToDetect)
                        break;
                }
                else if (cycle > cycleToDetect) {
                    signalsSum += (registerX - lastInstruction.Value) * cycleToDetect;

                    cycleToDetect += differenceBetweenCycles;
                    if (cycleToDetect > lastCycleToDetect)
                        break;
                }
                
                // apply instructions
                Instruction actualInstruction = instructions[i];

                cycle += actualInstruction.TimeToComplete;
                registerX += actualInstruction.Value;
            }

            return signalsSum;
        }

        private static Instruction ParseToInstruction(string[] instructionLine)
        {
            //noop
            if (instructionLine.Length == 1)
                return new Instruction(1);

            //addx
            return new Instruction(2, Convert.ToInt32(instructionLine[1]));
        }
    }
}