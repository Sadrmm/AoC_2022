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

        public static string PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer10.txt");

            ///////DEBUG INPUT/////
            //input = new string[]
            //{
            //    "addx 15",
            //    "addx -11",
            //    "addx 6",
            //    "addx -3",
            //    "addx 5",
            //    "addx -1",
            //    "addx -8",
            //    "addx 13",
            //    "addx 4",
            //    "noop",
            //    "addx -1",
            //    "addx 5",
            //    "addx -1",
            //    "addx 5",
            //    "addx -1",
            //    "addx 5",
            //    "addx -1",
            //    "addx 5",
            //    "addx -1",
            //    "addx -35",
            //    "addx 1",
            //    "addx 24",
            //    "addx -19",
            //    "addx 1",
            //    "addx 16",
            //    "addx -11",
            //    "noop",
            //    "noop",
            //    "addx 21",
            //    "addx -15",
            //    "noop",
            //    "noop",
            //    "addx -3",
            //    "addx 9",
            //    "addx 1",
            //    "addx -3",
            //    "addx 8",
            //    "addx 1",
            //    "addx 5",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "addx -36",
            //    "noop",
            //    "addx 1",
            //    "addx 7",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "addx 2",
            //    "addx 6",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "addx 1",
            //    "noop",
            //    "noop",
            //    "addx 7",
            //    "addx 1",
            //    "noop",
            //    "addx -13",
            //    "addx 13",
            //    "addx 7",
            //    "noop",
            //    "addx 1",
            //    "addx -33",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "addx 2",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "addx 8",
            //    "noop",
            //    "addx -1",
            //    "addx 2",
            //    "addx 1",
            //    "noop",
            //    "addx 17",
            //    "addx -9",
            //    "addx 1",
            //    "addx 1",
            //    "addx -3",
            //    "addx 11",
            //    "noop",
            //    "noop",
            //    "addx 1",
            //    "noop",
            //    "addx 1",
            //    "noop",
            //    "noop",
            //    "addx -13",
            //    "addx -19",
            //    "addx 1",
            //    "addx 3",
            //    "addx 26",
            //    "addx -30",
            //    "addx 12",
            //    "addx -1",
            //    "addx 3",
            //    "addx 1",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "addx -9",
            //    "addx 18",
            //    "addx 1",
            //    "addx 2",
            //    "noop",
            //    "noop",
            //    "addx 9",
            //    "noop",
            //    "noop",
            //    "noop",
            //    "addx -1",
            //    "addx 2",
            //    "addx -37",
            //    "addx 1",
            //    "addx 3",
            //    "noop",
            //    "addx 15",
            //    "addx -21",
            //    "addx 22",
            //    "addx -6",
            //    "addx 1",
            //    "noop",
            //    "addx 2",
            //    "addx 1",
            //    "noop",
            //    "addx -10",
            //    "noop",
            //    "noop",
            //    "addx 20",
            //    "addx 1",
            //    "addx 2",
            //    "addx 2",
            //    "addx -6",
            //    "addx -11",
            //    "noop",
            //    "noop",
            //    "noop",
            //};
            ///////////////////////
            
            // get all instructions
            Instruction[] instructions = new Instruction[input.Length];
            for (int i = 0; i < input.Length; i++) {
                string[] instructionLine = input[i].Split();

                instructions[i] = ParseToInstruction(instructionLine);
            }

            // do cycle
            int cycle = 1;
            int registerX = 1;
            string drawCRT = String.Empty;

            int wide = 40;
            int high = 6;

            string[] linesCRT = new string[high];
            int nextLine = 0;

            bool[] cyclesComproved = new bool[wide * high];

            //first time no need to check if it is outside range
            CheckSpriteInside(registerX, cycle, wide, high, ref linesCRT, ref nextLine, ref drawCRT);

            Instruction lastInstruction = instructions[0];
            cycle += lastInstruction.TimeToComplete;
            registerX += lastInstruction.Value;

            for (int i = 1; i < instructions.Length; i++) {
                lastInstruction = instructions[i - 1];
                bool screenFull = false;
                int cycleToComprove = cycle-1 -1;
                if (!cyclesComproved[cycleToComprove]) { 
                    screenFull = CheckSpriteInside(registerX - lastInstruction.Value, cycleToComprove, wide, high, ref linesCRT, ref nextLine, ref drawCRT);
                    cyclesComproved[cycleToComprove] = true;
                    if (screenFull)
                        break;
                }

                cycleToComprove = cycle-1;
                screenFull = CheckSpriteInside(registerX, cycleToComprove, wide, high, ref linesCRT, ref nextLine, ref drawCRT);
                cyclesComproved[cycleToComprove] = true;
                if (screenFull)
                    break;

                // apply instructions
                Instruction actualInstruction = instructions[i];
                cycle += actualInstruction.TimeToComplete;
                registerX += actualInstruction.Value;
            }

            return drawCRT;
        }

        private static bool CheckSpriteInside(int registerX, int cycle, int wide, int high, ref string[] linesCRT, ref int nextLine, ref string drawCRT)
        {
            bool inside;
            bool screenFull = false;

            // check if sprite is inside register
            int valueToCompare = cycle % wide;
            if (registerX == valueToCompare
                || registerX + 1 == valueToCompare
                || registerX - 1 == valueToCompare)
                inside = true;
            else
                inside = false;

            //write
            if (inside) {
                linesCRT[nextLine] += "#";
            }
            else {
                linesCRT[nextLine] += ".";
            }

            //check new line or end write
            if (linesCRT[nextLine].Length >= wide) {
                drawCRT += linesCRT[nextLine] + "\n";
                nextLine++;

                if (nextLine >= high)
                    screenFull = true;
            }

            return screenFull;
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