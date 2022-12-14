using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_2022
{
    internal class Day4
    {
        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer4A.txt");

            int counter = 0;

            foreach (string pair in input) {
                string[] elfs = pair.Split(',');

                int[] firstElfSections = FillSectionsString(elfs[0]);
                int[] secondElfSections = FillSectionsString(elfs[1]);

                IEnumerable<int> intersection = firstElfSections.Intersect(secondElfSections);

                if (intersection.Count() == firstElfSections.Count() || intersection.Count() == secondElfSections.Count())
                    counter++;
	        }

            return counter;
        }

        public static int PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer4B.txt");

            int counter = 0;

            foreach (string pair in input)
            {
                string[] elfs = pair.Split(',');

                int[] firstElfSections = FillSectionsString(elfs[0]);
                int[] secondElfSections = FillSectionsString(elfs[1]);

                IEnumerable<int> intersection = firstElfSections.Intersect(secondElfSections);

                if (intersection.Count() > 0)
                    counter++;
            }

            return counter;
        }

        private static int[] FillSectionsString(string range)
        {
            string[] firstAndLast = range.Split('-');
            int first = Convert.ToInt32(firstAndLast[0]);
            int last = Convert.ToInt32(firstAndLast[1]);

            int[] sectionNumbers = new int[last-first + 1];
            for (int i = 0; i < sectionNumbers.Length; i++) {
                sectionNumbers[i] = first + i;
            }

            return sectionNumbers;
        }
    }
}