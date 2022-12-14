using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2022
{
    internal class Day5
    {
        private struct Movement
        {
            public int Amount;
            public int Origin;
            public int Destination;

            public Movement(int amount, int origin, int destination)
            {
                Amount = amount;
                Origin = origin;
                Destination = destination;
            }
        }

        public static string PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer5A.txt");

            //the stacks and the movements
            Stack<char>[] stacks = ObtainOriginStacks(input);
            List<Movement> movements = ObtainMovements(input);

            //apply movements
            foreach (Movement move in movements) {
                for (int i = 0; i < move.Amount; i++) {
                    var origin = stacks[move.Origin].Pop();
                    stacks[move.Destination].Push(origin);
                }
            }

            //obtatin top char in stacks
            string topStacks = ObtainTopSacks(stacks);

            return topStacks;
        }

        public static string PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer5A.txt");

            //the stacks and the movements
            Stack<char>[] stacks = ObtainOriginStacks(input);
            List<Movement> movements = ObtainMovements(input);

            //apply movements
            foreach (Movement move in movements) {
                Stack<char> auxStack = new Stack<char>();

                for (int i = 0; i < move.Amount; i++) {
                    var origin = stacks[move.Origin].Pop();
                    auxStack.Push(origin);
                }

                while (auxStack.Count > 0)
                    stacks[move.Destination].Push(auxStack.Pop());
            }

            //obtatin top char in stacks
            string topStacks = ObtainTopSacks(stacks);

            return topStacks;
        }

        private static string ObtainTopSacks(Stack<char>[] stacks)
        {
            string topStacks = String.Empty;
            for (int i = 0; i < stacks.Length; i++) {
                topStacks += stacks[i].Peek();
            }

            return topStacks;
        }

        //could be more efficient in memory passing movement by ref, to prevent copy all the data
        private static List<Movement> ObtainMovements(string[] input)
        {
            List<Movement> movements = new List<Movement>();

            for (int i = 0; i < input.Length; i++) {
                //lines with movements
                if (input[i].Contains("move")) {

                    //find the three numbers
                    string currentNumber = String.Empty;
                    List<int> threeNumbers = new List<int>();
                    
                    for (int j = 0; j < input[i].Length; j++) {
                        if (Char.IsDigit(input[i][j])) {
                            currentNumber += input[i][j];
                        }
                        else {
                            if (currentNumber != String.Empty) {
                                threeNumbers.Add(Convert.ToInt32(currentNumber));
                                currentNumber = String.Empty;
                            }
                        }
                    }

                    //check just in case
                    if (currentNumber != String.Empty) {
                        threeNumbers.Add(Convert.ToInt32(currentNumber));
                        currentNumber = String.Empty;
                    }

                    movements.Add(new Movement(threeNumbers[0], threeNumbers[1]-1, threeNumbers[2]-1));
                }
            }

            return movements;
        }

        private static Stack<char>[] ObtainOriginStacks(string[] input)
        {
            Stack<char>[] stacks;

            //search for the line where the stack numbers are
            bool numberFound = false;
            int lineWithNumbers = 0;
            int numOfStacks = 0;
            int i = 0;
            while (!numberFound) {
                if (input[i].Any(char.IsDigit)) {
                    numberFound = true;
                    lineWithNumbers = i;
                    int lastNumberInLine = input[i].Length - 2;   //-1 is an empty space, -2 is the last number
                    numOfStacks = (int)Char.GetNumericValue(input[i][lastNumberInLine]);
                }

                i++;
            }

            //initialize stacks
            stacks = new Stack<char>[numOfStacks];
            for (i = 0; i < numOfStacks; i++) {
                stacks[i] = new Stack<char>();
            }

            //loop input from lineWithNumbers to first line
            for (i = lineWithNumbers; i >= 0; i--) {

                for (int j = 0; j < input[i].Length; j++) {
                    if (Char.IsLetter(input[i][j])) {
                        char actuaLetter = input[i][j];
                        int stackID = (int)Char.GetNumericValue(input[lineWithNumbers][j]) - 1;
                        
                        stacks[stackID].Push(actuaLetter);
                    }
                }
            }

            return stacks;
        }
    }
}