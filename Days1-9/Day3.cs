using System;
using System.IO;
using System.Linq;

namespace AoC_2022
{
    internal class Day3
    {
        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer3A.txt");

            int sumOfPriorities = 0;

            //check each rucksack
            foreach (string rucksack in input) {
                string firstCompartment = rucksack.Substring(0, rucksack.Length/2);
                string secondCompartment = rucksack.Substring(rucksack.Length/2);

                //check which char is duplicated
                foreach (char item in firstCompartment) {
                    if (secondCompartment.Contains(item)) {
                        sumOfPriorities += ObtainPriority(item);
                        break;
                    }
                }
            }

            return sumOfPriorities;
        }

        public static int PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer3B.txt");

            int sumOfPriorities = 0;

            //check the group
            for (int i = 0; i < input.Length; i += 3) {
                
                //check which char is of each group
                foreach (char item in input[i]) {
                    if (input[i+1].Contains(item) && input[i + 2].Contains(item)) {
                        sumOfPriorities += ObtainPriority(item);
                        break;
                    }
                }
            }

            return sumOfPriorities;
        }

        private static int ObtainPriority(char item)
        {
            //IsUpper
            if (Char.IsUpper(item)) 
                return (int) item - (int)'A' + 27;
            //IsLower
            return (int) item - (int)'a' + 1;
        }
    }
}
