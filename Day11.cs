using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace AoC_2022
{
    internal class Day11
    {
        private class Monkey
        {
            Queue<long> items;
            string operation;
            int testDivisibleBy;
            int trueTestMonkeyPos;
            int falseTestMonkeyPos;

            public int ItemsCount => items.Count;

            public Monkey(Queue<long> items, string operation, int testDivisibleBy, int truePos, int falsePos)
            {
                this.items = items;
                this.operation = operation;
                this.testDivisibleBy = testDivisibleBy;
                this.trueTestMonkeyPos = truePos;
                this.falseTestMonkeyPos = falsePos;
            }

            public long InspectItem()
            {
                long actualItem = items.Dequeue();
                actualItem = WorryItem(actualItem);
                actualItem = BoredItem(actualItem);

                return actualItem;
            }

            public void ReceiveItem(long newItem)
            {
                items.Enqueue(newItem);
            }

            public int PassItemToMonkeyPos(long item) {
                if (item % testDivisibleBy == 0)
                    return trueTestMonkeyPos;

                return falseTestMonkeyPos;
            }

            private long WorryItem(long item)
            {
                string actualOperation = operation.Replace("old", item.ToString()+".0");
                var calculate = new DataTable().Compute(actualOperation, null);
                long result = Convert.ToInt64(calculate);   //just in case divisions

                return (long)result;
            }

            private long BoredItem(long item)
            {
                return item / 3;
            }

        }

        public static int PartA()
        {
            string[] input = File.ReadAllLines("../../inputEjer11.txt");

            //create monkeys
            Monkey[] monkeys = CreateAllMonkeys(input).ToArray();
            int[] inspectedItems = new int[monkeys.Length];

            //manage rounds
            int rounds = 20;
            int i = 0;
            while (i < rounds) {
                //each monkey plays
                for (int j = 0; j < monkeys.Length; j++) {
                    Monkey monkey = monkeys[j];
                    inspectedItems[j] += monkey.ItemsCount;

                    //each item
                    while (monkey.ItemsCount > 0) {
                        long newWorryLvlItem = monkey.InspectItem();
                        int monkeyDestination = monkey.PassItemToMonkeyPos(newWorryLvlItem);
                        monkeys[monkeyDestination].ReceiveItem(newWorryLvlItem);
                    }
                }

                i++;
            }

            //get top two inspectedItems
            int[] topTwoInspectedIndex = new int[] { inspectedItems[0], inspectedItems[1] };
            Array.Sort(topTwoInspectedIndex);

            for (i = 2; i < inspectedItems.Length; i++) {
                if (inspectedItems[i] > topTwoInspectedIndex[0]) {
                    topTwoInspectedIndex[0] = inspectedItems[i];
                    Array.Sort(topTwoInspectedIndex);
                }
            }

            return topTwoInspectedIndex[0] * topTwoInspectedIndex[1];
        }

        private static List<Monkey> CreateAllMonkeys(string[] input)
        {
            List<Monkey> monkeys = new List<Monkey>();
            for (int i = 0; i < input.Length; i += 7) {
                string[] parameters;

                //get items
                parameters = input[i + 1].Split();
                Queue<long> items = new Queue<long>();
                for (int j = 0; j < parameters.Length; j++) {
                    string parToCheck = parameters[j].Replace(',', ' ');
                    if (int.TryParse(parToCheck, out int item)) {
                        items.Enqueue(item);
                    }
                }

                //get operation
                parameters = input[i + 2].Split();
                string operation = parameters[parameters.Length - 3] + parameters[parameters.Length - 2] + parameters[parameters.Length - 1];

                //get testDivisibleBy
                parameters = input[i + 3].Split();
                int testDivisibleBy = Convert.ToInt32(parameters[parameters.Length - 1]);

                //get truePos and falsePos
                parameters = input[i + 4].Split();
                int truePos = Convert.ToInt32(parameters[parameters.Length - 1]);

                parameters = input[i + 5].Split();
                int falsePos = Convert.ToInt32(parameters[parameters.Length - 1]);

                //add monkey to list of monkeys
                monkeys.Add(new Monkey(items, operation, testDivisibleBy, truePos, falsePos));
            }

            return monkeys;
        }
    }
}