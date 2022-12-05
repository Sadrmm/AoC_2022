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
