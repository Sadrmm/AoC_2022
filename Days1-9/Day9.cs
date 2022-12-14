using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_2022
{
    internal class Day9
    {
        private enum Direction
        {
            UP = 0,
            DOWN = 1,
            LEFT = 2,
            RIGHT = 3
        }

        private class Movement
        {
            private Direction _direction;
            private int _distance;

            public Direction Direction => _direction;
            public int Distance => _distance;

            public Movement(Direction direction, int distance)
            {
                _direction = direction;
                _distance = distance;
            }
        }

        private struct Vector2
        {
            private int _x;
            private int _y;

            public int X => _x;
            public int Y => _y;

            public Vector2(int x, int y)
            {
                this._x = x;
                this._y = y;
            }

            public void Move(Direction direction)
            {
                switch (direction)
                {
                    case Direction.UP:
                        _y++;
                        break;
                    case Direction.DOWN:
                        _y--;
                        break;
                    case Direction.LEFT:
                        _x--;
                        break;
                    case Direction.RIGHT:
                        _x++;
                        break;
                    default:
                        throw new Exception($"{direction} not managed");
                }
            }
        }

        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer9A.txt");

            //save movements
            Movement[] movements = new Movement[input.Length];

            for (int i = 0; i < input.Length; i++) {
                movements[i] = GetMovement(input[i]);
            }

            // initial positions
            Vector2 headCoord = new Vector2(0,0);
            Vector2 tailCoord = new Vector2(0,0);
            List<Vector2> tailVisits = new List<Vector2>();

            //move tail and save visits
            tailVisits.Add(tailCoord);

            for (int i = 0;  i < movements.Length; i++) {

                Movement actualMovement = movements[i];
                for (int j = 0; j < actualMovement.Distance; j++) {
                    headCoord.Move(actualMovement.Direction);
                    FollowHead(headCoord, ref tailCoord);

                    if (!tailVisits.Contains(tailCoord))
                        tailVisits.Add(tailCoord);
                }
            }

            return tailVisits.Count;
        }

        public static int PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer9A.txt");

            //save movements
            Movement[] movements = new Movement[input.Length];

            for (int i = 0; i < input.Length; i++) {
                movements[i] = GetMovement(input[i]);
            }

            // initial positions
            Vector2 headCoord = new Vector2(0, 0);
            
            int tails = 9;
            Vector2[] tailsCoord = new Vector2[tails];

            List<Vector2> lastTailVisits = new List<Vector2>();

            //move tail and save visits
            lastTailVisits.Add(tailsCoord[tailsCoord.Length - 1]);   //add the last one

            for (int i = 0;  i < movements.Length; i++) {

                Movement actualMovement = movements[i];
                for (int j = 0; j < actualMovement.Distance; j++) {
                    headCoord.Move(actualMovement.Direction);

                    //move rope
                    FollowHead(headCoord, ref tailsCoord[0]);
                    for (int k = 0; k < tails-1; k++) {
                        FollowHead(tailsCoord[k], ref tailsCoord[k+1]);
                    }

                    if (!lastTailVisits.Contains(tailsCoord[tailsCoord.Length - 1]))
                        lastTailVisits.Add(tailsCoord[tailsCoord.Length - 1]);
                }
            }

            return lastTailVisits.Count;
        }

        private static void FollowHead(Vector2 headCoord, ref Vector2 tailCoord)
        {
            int horizontalDistance = headCoord.X - tailCoord.X;
            int verticalDistance = headCoord.Y - tailCoord.Y;

            // distance between them is 1 or 0, not move
            if (Math.Abs(verticalDistance) <= 1 && Math.Abs(horizontalDistance) <= 1)
                return;

            //same horizontal but not close
            if (horizontalDistance == 0 && Math.Abs(verticalDistance) > 1) {
                if (verticalDistance > 0)
                    tailCoord.Move(Direction.UP);
                else
                    tailCoord.Move(Direction.DOWN);
            }
            //same vertical but not close 
            else if (verticalDistance == 0 && Math.Abs(horizontalDistance) > 1) {
                if (horizontalDistance > 0)
                    tailCoord.Move(Direction.RIGHT);
                else
                    tailCoord.Move(Direction.LEFT);

            }
            //diagonally
            else {
                if (horizontalDistance > 0)
                    tailCoord.Move(Direction.RIGHT);
                else
                    tailCoord.Move(Direction.LEFT);

                //now move the other axis
                if (verticalDistance > 0)
                    tailCoord.Move(Direction.UP);
                else
                    tailCoord.Move(Direction.DOWN);
            }
        }

        private static Movement GetMovement(string line)
        {
            string[] movementLine = line.Split();

            Direction direction;
            int distance;

            switch (movementLine[0])
            {
                case "U":
                    direction = Direction.UP;
                    break;
                case "D":
                    direction = Direction.DOWN;
                    break;
                case "L":
                    direction = Direction.LEFT;
                    break;
                case "R":
                    direction = Direction.RIGHT;
                    break;
                default:
                    throw new Exception("first argument not expected");
            }

            distance = Convert.ToInt32(movementLine[1]);

            return new Movement(direction, distance);
        }
    }
}