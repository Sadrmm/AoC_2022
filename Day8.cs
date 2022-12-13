using System;
using System.IO;

namespace AoC_2022
{
    internal class Day8
    {

        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer8A.txt");

            //get all trees heights
            int rows = input.Length;
            int cols = input.GetLength(0);
            int[,] trees = new int[rows, cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    string row = input[i];
                    trees[i, j] = (int)Char.GetNumericValue(row[j]);
                }
            }

            //get visible trees
            int visibleTrees = 0;

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    //we are on a border
                    if (i == 0 || i == rows-1 || j == 0 || j == cols-1) {
                        visibleTrees++;
                        continue;
                    }

                    //inside trees
                    if (CheckVisible(i, j, trees))
                        visibleTrees++;
                }
            }

            return visibleTrees;
        }

        public static int PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer8A.txt");

            //get all trees heights
            int rows = input.Length;
            int cols = input.GetLength(0);
            int[,] trees = new int[rows, cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    string row = input[i];
                    trees[i, j] = (int)Char.GetNumericValue(row[j]);
                }
            }

            //obtain highest scenic score
            int maxScenicScore = 0;
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    //we are on a border, so 0 multiply by something is 0
                    if (i == 0 || i == rows-1 || j == 0 || j == cols-1) {
                        continue;
                    }

                    //inside trees
                    int scenicScore = CheckScenicScore(i, j, trees);
                    if (scenicScore > maxScenicScore)
                        maxScenicScore = scenicScore;
                }
            }

            return maxScenicScore;
        }

        private static int CheckScenicScore(int i, int j, int[,] trees)
        {
            int topDistance, bottomDistance, leftDistance, rightDistance;
            CheckTop(i, j, trees[i, j], trees, out topDistance);
            CheckBottom(i, j, trees[i, j], trees, out bottomDistance);
            CheckLeft(i, j, trees[i, j], trees, out leftDistance);
            CheckRight(i, j, trees[i, j], trees, out rightDistance);

            return topDistance * bottomDistance * leftDistance * rightDistance;
        }

        private static bool CheckVisible(int row, int col, int[,] trees)
        {
            int treeHeight = trees[row, col];

            return CheckTop(row, col, treeHeight, trees, out _) ||
                    CheckBottom(row, col, treeHeight, trees, out _) ||
                    CheckLeft(row, col, treeHeight, trees, out _) ||
                    CheckRight(row, col, treeHeight, trees, out _);
        }

        private static bool CheckTop(int row, int col, int treeHeight, int[,] trees, out int viewDistance)
        {
            bool visible = true;
            viewDistance = 0;

            for (int i = row; i >= 0; i--) {
                //itself
                if (i == row)
                    continue;

                viewDistance++;
                if (trees[i, col] >= treeHeight) {
                    visible = false;
                    break;
                }
            }
            return visible;
        }

        private static bool CheckBottom(int row, int col, int treeHeight, int[,] trees, out int viewDistance)
        {
            bool visible = true;
            viewDistance = 0;

            for (int i = row; i < trees.GetLength(0); i++) {
                //itself
                if (i == row)
                    continue;

                viewDistance++;
                if (trees[i, col] >= treeHeight) {
                    visible = false;
                    break;
                }
            }
            return visible;
        }

        private static bool CheckLeft(int row, int col, int treeHeight, int[,] trees, out int viewDistance)
        {
            bool visible = true;
            viewDistance = 0;

            for (int j = col; j >= 0; j--) {
                //itself
                if (j == col)
                    continue;

                viewDistance++;
                if (trees[row, j] >= treeHeight) {
                    visible = false;
                    break;
                }
            }
            return visible;
        }

        private static bool CheckRight(int row, int col, int treeHeight, int[,] trees, out int viewDistance)
        {
            bool visible = true;
            viewDistance = 0;

            for (int j = col; j < trees.GetLength(1); j++) {
                //itself
                if (j == col)
                    continue;

                viewDistance++;
                if (trees[row, j] >= treeHeight) {
                    visible = false;
                    break;
                }
            }
            return visible;
        }
    }
}