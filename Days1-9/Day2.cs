using System;
using System.IO;

namespace AoC_2022
{
    internal class Day2
    {
        private enum Shape
        {
            Rock = 1,
            Paper = 2,
            Scissors = 3
        }
        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer2A.txt");

            int score = 0;

            //check every match
            foreach (string match in input) {

                Shape opponentChoose = FromCharToShape(match[0]);
                Shape myChoose = FromCharToShape(match[2]);

                score += (int) myChoose;
                score += CheckWinner(opponentChoose, myChoose);
            }

            return score;
        }

        public static int PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer2B.txt");

            int score = 0;

            //check every match
            foreach (string match in input) {
                Shape opponentChoose = FromCharToShape(match[0]);
                Shape myChoose = ChooseByStrategy(opponentChoose, match[2]);

                score += (int)myChoose;
                score += CheckWinner(opponentChoose, myChoose);
            }

            return score;
        }

        private static Shape ChooseByStrategy(Shape opponentChoose, char strategy)
        {
            //loose
            if (strategy.Equals('X')) {
                if (opponentChoose == Shape.Rock)
                    return Shape.Scissors;
                if (opponentChoose == Shape.Paper)
                    return Shape.Rock;
                //opponentChoose == Shape.Scissors
                return Shape.Paper;
            }
            //draw
            if (strategy.Equals('Y')) {
                if (opponentChoose == Shape.Rock)
                    return Shape.Rock;
                if (opponentChoose == Shape.Paper)
                    return Shape.Paper;
                //opponentChoose == Shape.Scissors
                return Shape.Scissors;
            }
            //win ,strategy.Equals('Z')
            if (opponentChoose == Shape.Rock)
                return Shape.Paper;
            if (opponentChoose == Shape.Paper)
                return Shape.Scissors;
            //opponentChoose == Shape.Scissors
            return Shape.Rock;
        }

        private static int CheckWinner(Shape opponentChoose, Shape myChoose)
        {
            //draw
            if ((opponentChoose == Shape.Rock && myChoose == Shape.Rock)
                || (opponentChoose == Shape.Paper && myChoose == Shape.Paper)
                || (opponentChoose == Shape.Scissors && myChoose == Shape.Scissors)
                )
                return 3;
            //loose
            if ((opponentChoose == Shape.Rock && myChoose == Shape.Scissors)
                || (opponentChoose == Shape.Paper && myChoose == Shape.Rock)
                || (opponentChoose == Shape.Scissors && myChoose == Shape.Paper)
                )
                return 0;
            //win
            return 6;
        }

        private static Shape FromCharToShape(char playerInput)
        {
            if (playerInput.Equals('A') || playerInput.Equals('X'))
                return Shape.Rock;
            if (playerInput.Equals('B') || playerInput.Equals('Y'))
                return Shape.Paper;
            if (playerInput.Equals('C') || playerInput.Equals('Z'))
                return Shape.Scissors;

            throw new ArgumentException("Unexpected char: It should be A B C X Y or Z");
        }
    }
}
