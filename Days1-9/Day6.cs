using System;
using System.IO;
using System.Linq;

namespace AoC_2022
{
    internal class Day6
    {
        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer6A.txt");
            
            int markerLength = 4;

            return ProcessInput(input, markerLength);
        }

        public static int PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer6B.txt");

            int markerLength = 14;

            return ProcessInput(input, markerLength);
        }

        private static int ProcessInput(string[] input, int markerLength)
        {
            string marker = input[0].Substring(0, markerLength);

            //check the line
            int i = markerLength;
            while (!IsValidMarker(marker))
            {
                marker = marker.Substring(1) + input[0][i];

                i++;
            }

            return i;
        }

        private static bool IsValidMarker(string marker)
        {
            string lettersUsed = "" + marker[0];

            for (int i = 1; i < marker.Length; i++) {
                char actualLetter = marker[i];

                if (lettersUsed.Contains(actualLetter))
                    return false;

                lettersUsed += actualLetter;
            }

            return true;
        }
    }
}