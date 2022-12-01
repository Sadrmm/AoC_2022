using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_2022
{
    internal class Day1
    {
        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer1A.txt");

            //obtain the food by elf
            List<List<int>> foodByElf = ObtainFoodByElf(input);

            //check who carries most
            int mostElfCalories = 0;
            for (int i = 1; i < foodByElf.Count; i++) {

                int actualCalories = ElfTotalCalories(foodByElf[i]);

                if (actualCalories > mostElfCalories)
                    mostElfCalories = actualCalories;
            }

            return mostElfCalories;
        }

        public static int PartB()
        {
            string[] input = File.ReadAllLines("../../inputEjer1B.txt");

            //obtain the food by elf
            List<List<int>> foodByElf = ObtainFoodByElf(input);

            //check top 3 who carry most
            int[] top3ElfCalories = new int[3];
            for (int i = 0; i < 3; i++) {
                top3ElfCalories[i] = ElfTotalCalories(foodByElf[i]);
            }
            Array.Sort(top3ElfCalories);

            for (int i = 3; i < foodByElf.Count; i++) {

                int actualCalories = ElfTotalCalories(foodByElf[i]);
                
                if (actualCalories > top3ElfCalories[0]) {  //if has more calories than the third one, he is inside top3
                    top3ElfCalories[0] = actualCalories;
                    Array.Sort(top3ElfCalories);
                }
            }

            //total calories of top3
            int totalCalories = top3ElfCalories[0] + top3ElfCalories[1] + top3ElfCalories[2];

            return totalCalories;
        }

        private static List<List<int>> ObtainFoodByElf(string[] input)
        {
            List<List<int>> foodByElf = new List<List<int>>();

            List<int> food = new List<int>();
            foreach (string line in input) {
                //new elf
                if (line.Equals("")) {
                    foodByElf.Add(food);
                    food = new List<int>();
                    continue;
                }

                //same elf
                food.Add(Convert.ToInt32(line));
            }

            return foodByElf;
        }

        private static int ElfTotalCalories(List<int> elfFood)
        {
            int elfTotalCalories = 0;

            for (int i = 0; i < elfFood.Count; i++) {
                elfTotalCalories += elfFood[i];
            }

            return elfTotalCalories;
        }
    }
}
