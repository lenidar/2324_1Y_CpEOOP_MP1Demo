using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace _2324_1Y_CpEOOP_MP1Demo
{
    internal class Program
    {
        static List<string[][]> _bingoCardList =
            new List<string[][]>();
        static Random _rnd = new Random();

        static void Main(string[] args)
        {
            int displayedCard = 0;
            char uInput = ' ';
            //GenerateCard();
            _bingoCardList.Add(ConvertIntArrToStringArr(GenerateCard()));
            while (true)
            {
                CardDisplay(_bingoCardList[displayedCard]);
                Console.Write("\nWhat do you want to do? <L or R>: ");
                uInput = Console.ReadKey().KeyChar;

                switch (uInput) 
                {
                    case 'l':
                        displayedCard--;
                        break;
                    case 'r':
                        displayedCard++;
                        break;
                }

                if (displayedCard == -1 
                    || displayedCard == _bingoCardList.Count())
                {
                    _bingoCardList.Add(ConvertIntArrToStringArr(GenerateCard()));
                    displayedCard = _bingoCardList.Count() - 1;
                }
                Console.Clear();
            }
        }

        static int[][] GenerateCard()
        {
            // initialize the card
            int[][] tempCard = new int[5][];
            int temp = -1;

            // initialize internal lengths
            for (int x = 0; x < tempCard.Length; x++)
            {
                tempCard[x] = new int[5];
                for (int y = 0; y < tempCard[x].Length; y++)
                    tempCard[x][y] = -1;
            }

            // placing the numbers to the card
            // remember the outer array is the column and not the row
            //for (int x = 0; x < tempCard.Length; x++)
            //    for (int y = 0; y < tempCard[x].Length; y++)
            //    {
            //        temp = GenerateNumber(x);
            //        if (RemoveDuplicates(tempCard[x], temp))
            //            y--;
            //        else
            //            tempCard[x][y] = temp;
            //    }

            for (int x = 0; x < tempCard.Length; x++)
            {
                tempCard[x] = GenerateNumbersWithoutDuplicate(x);
            }

            //TestDisplay(tempCard);
            //Console.WriteLine();

            return tempCard;
        }

        static int GenerateNumber(int column)
        {
            int rNum = _rnd.Next(1, 16);
            rNum += column * 15;
            return rNum;
        }

        static bool RemoveDuplicates(int[] numArr, int num)
        {
            bool duplicateFlag = false;
            
            for(int x = 0; x < numArr.Length; x++)
            {
                if (numArr[x] == -1)
                    break;
                else if (numArr[x] == num)
                {
                    duplicateFlag = true;
                    break;
                }
            }

            return duplicateFlag;
        }

        static int[] GenerateNumbersWithoutDuplicate(int column)
        {
            List<int> nums = new List<int>();
            int[] bNums = new int[5];

            for(int x = 1; x < 16; x++)
                nums.Add(x + (column * 15));

            nums = ShuffleList(nums);

            for(int x = 0; x < bNums.Count(); x++)
            {
                bNums[x] = nums[_rnd.Next(nums.Count())];
                nums.Remove(bNums[x]);
            }

            return bNums;
        }

        static List<int> ShuffleList(List<int> nums)
        {
            List<int> tempArr = new List<int>();
            int temp = 0;

            while(nums.Count > 0)
            {
                temp = _rnd.Next(nums.Count);
                tempArr.Add(nums[temp]);
                nums.RemoveAt(temp);
            }

            return tempArr;
        }

        static string[][] ConvertIntArrToStringArr(int[][] tempCard)
        {
            string[][] theCard = new string[tempCard.Length][];

            for(int x = 0; x < tempCard.Length; x++)
            {
                theCard[x] = new string[tempCard[x].Length];
                for(int y = 0; y < tempCard[x].Length; y++)
                {
                    theCard[x][y] = tempCard[x][y] + "";
                    while (theCard[x][y].Length < 3)
                        theCard[x][y] = " " + theCard[x][y];
                }
            }

            theCard[2][2] = "FRE";

            return theCard;
        }

        static void TestDisplay(int[][] tempCard)
        {
            for (int x = 0; x < tempCard.Length; x++)
            {
                for (int y = 0; y < tempCard[x].Length; y++)
                    Console.Write(tempCard[y][x] + "\t");
                Console.WriteLine();
            }
        }

        static void TestDisplay(string[][] tempCard)
        {
            for (int x = 0; x < tempCard.Length; x++)
            {
                for (int y = 0; y < tempCard[x].Length; y++)
                    Console.Write(tempCard[y][x] + "\t");
                Console.WriteLine();
            }
        }

        static void CardDisplay(string[][] tempCard)
        {
            for (int y = 0; y < 21; y++)
                Console.Write("=");
            Console.WriteLine();
            Console.WriteLine("| B | I | N | G | O |");
            for (int y = 0; y < 21; y++)
                Console.Write("=");
            Console.WriteLine();
            for (int x = 0; x < tempCard.Length; x++)
            {
                Console.Write("|");
                for (int y = 0; y < tempCard[x].Length; y++)
                    Console.Write(tempCard[y][x] + "|");
                Console.WriteLine();
                for (int y = 0; y < 21; y++)
                    Console.Write("=");
                Console.WriteLine();
            }
        }
    }
}
