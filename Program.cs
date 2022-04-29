using System;

namespace week42
{
    internal class Program
    {
        public const int width = 12;
        public const int height = 23;
        static char[,] field = new char[height, width];

        static bool xCordinatsNow = true;

        static int howManyMyShips = 10;
        static int howManyOppShips = 10;

        static int xChoose = 0;
        static int yChoose = 0;

        static int needToWin = 10;
        static int needToLose = 10;

        static int[,] shipCorr = new int[height, width];

        static char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        static bool yourTurn = true;

        static void Main(string[] args)
        {
            MakeField();
            for (; ; )
            {
                Console.Clear();
                DrawField();
                ChooseCordinats();
                BotHitYou();
                CheckIfEnd();
                xCordinatsNow = !xCordinatsNow;
            }
        }

        static void CheckIfEnd()
        {
            if (needToWin == 0)
            {
                Console.WriteLine();
                Console.WriteLine("---You Win---");
                Console.ReadLine();
                Restart();
            }
            if (needToLose == 0)
            {
                Console.ReadLine();
                Console.WriteLine("---You Lose---");
                Console.ReadLine();
                Restart();
            }
        }

        static void Restart()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    shipCorr[i, j] = 0;
                }
            }
            xCordinatsNow = false;
            xChoose = 0;
            yChoose = 0;
            needToWin = 10;
            howManyOppShips = 10;
            howManyMyShips = 10;
            MakeField();
        }

        static void BotHitYou()
        { 
            while (!yourTurn)
            {
                Random rnd = new Random();
                bool hitBoat = false;
                bool hitAnything = false;
                for(; !hitAnything;)
                {
                    int xRand = rnd.Next(0, 10);
                    int yRand = rnd.Next(12, 23);
                
                    if (shipCorr[yRand, xRand] == 0)
                    {
                        shipCorr[yRand, xRand] = 4;
                        hitAnything = true;
                    }
                    else if (shipCorr[yRand, xRand] == 1)
                    {
                        shipCorr[yRand, xRand] = 5;
                        hitBoat = true;
                        hitAnything = true;
                        needToLose--;
                    }
                }
                if (!hitBoat) yourTurn = true;
            }
        }

        static void ChooseCordinats()
        {
            char input = Console.ReadKey().KeyChar;

            if (input == '0' || input == '1' || input == '2' || input == '3' || input == '4' || input == '5' ||
                input == '6' || input == '7' || input == '8' || input == '9' || input == 'r')
            {

                if ((char)input == 'r')
                {
                    Restart();
                }
                else if (xCordinatsNow)
                {
                    xChoose = (int)input - 48;
                }
                else
                {
                    yChoose = (int)input - 48;
                    hitSomething();
                }
            }
        }

        static void hitSomething()
        {
            if (shipCorr[yChoose, xChoose] == 1)
            {
                shipCorr[yChoose, xChoose] = 3;
                needToWin--;
            }
            else 
            { 
                shipCorr[yChoose + 1, xChoose + 1] = 2;
                yourTurn = false;
            }
        }

        static void MakeField()
        {
            Random rnd = new Random();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (rnd.Next(0, 7) == 1 && howManyMyShips > 0 && i > height - 11 && i < height - 1 && j > 0 && j < width - 1)
                    {
                        field[i, j] = '▓';
                        shipCorr[i, j] = 1;
                        howManyMyShips--;
                    }
                    else if (rnd.Next(0, 7) == 1 && howManyOppShips > 0 && i < height - 12 && i > 0 && j > 0 && j < width - 1)
                    {
                        howManyOppShips--;
                        shipCorr[i, j] = 1;
                    }

                    else field[i, j] = ' ';
                }
            }
        }

        static void DrawField()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //je korbl ale she ne vdaryv = 1
                    //ja vdaryv = 2
                    //ja popav = 3
                    //vin vdaryv = 4
                    //vin popav = 5
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1 || i == 11)
                    {
                        field[i, j] = '█';
                    }
                    else if (shipCorr[i, j] == 2)
                    {
                        field[i, j] = ' ';
                    }
                    else if (shipCorr[i, j] == 3)
                    {
                        field[i, j] = 'X';
                    }
                    else if (shipCorr[i, j] == 4)
                    {
                        field[i, j] = '·';
                    }
                    else if (shipCorr[i, j] == 5)
                    {
                        field[i, j] = '░';
                    }
                    else if (i < 12)
                    {
                        field[i, j] = '▒';
                    }

                    if (xCordinatsNow && j > 0 && j < width - 1 && i == 0)
                    {
                        field[i, j] = nums[j - 1];
                    }
                    else if (!xCordinatsNow && i > 0 && i < 11 && j == 0)
                    {
                        field[i, j] = nums[i - 1];
                    }
                    Console.Write(field[i, j]);
                }
                if (i == 0) Console.Write($"    {xChoose}");
                if (i == 1) Console.Write($"    {yChoose}");
                Console.WriteLine();
            }
        }
    }
}