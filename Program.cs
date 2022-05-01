using System;

namespace week42
{
    public enum Pixel
    {
        iShot = (char)250, //'·'
        iHit = (char)88, //'X'
        heShot = (char)250, //'·'
        heHit = (char)177 //'░'
    }
    internal class Program
    {
        public const int width = 12, height = 23;
        static bool xCordinatsNow = true;
        static bool yourTurn = true;

        static sbyte howManyMyShips = 10, howManyOppShips = 10;
        static sbyte needToWin = 10, needToLose = 10;

        static sbyte xChoose = 0, yChoose = 0;

        static char[,] field = new char[height, width];
        static int[,] shipCorr = new int[height, width];

        static void Main(string[] args)
        {
            MakeField();
            while(true)
            {
                Console.Clear();
                DrawField();
                ChooseCordinats();
                BotHitYou();
                CheckIfEnd();
            }
        }
        static void CheckIfEnd()
        {
            if (needToWin == 0 || needToLose == 0)
            {
                Console.WriteLine();
                if (needToWin == 0)
                    Console.WriteLine("---You Win---");
                else
                    Console.WriteLine("---You Lose---");
                Console.ReadLine();
                Restart();
            }
        }
        static void Restart()
        {
            Array.Clear(shipCorr, 0, shipCorr.Length);
            xCordinatsNow = true;
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
                    int xRand = rnd.Next(1, 11);
                    int yRand = rnd.Next(12, 22);
                
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

            if (input == 'r') Restart();

            else if ((int)input >= '0' && (int)input <= '9')
            {
                if (xCordinatsNow)
                    xChoose = (sbyte)(input - 48);

                else {
                    yChoose = (sbyte)(input - 48);
                    hitSomething();
                }
                xCordinatsNow = !xCordinatsNow;
            }
        }
        static void hitSomething()
        {
            yChoose++;
            xChoose++;
            if (shipCorr[yChoose, xChoose] == 1) 
            {
                shipCorr[yChoose, xChoose] = 3;
                needToWin--;
            }
            else { 
                shipCorr[yChoose, xChoose] = 2;
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
                    if (rnd.Next(0, 7) == 1 && howManyMyShips > 0 && i > height - 11 && i < height - 1 && j > 0 && j < width - 1){
                        field[i, j] = '▓';
                        shipCorr[i, j] = 1;
                        howManyMyShips--;
                    }
                    else if (rnd.Next(0, 7) == 1 && howManyOppShips > 0 && i < height - 12 && i > 0 && j > 0 && j < width - 1){
                        howManyOppShips--;
                        shipCorr[i, j] = 1;
                    }
                    else field[i, j] = ' ';
                }
            }
        }
        static void DrawField()
        {
            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || j == 0 || i == height - 1 || j == width - 1 || i == 11)
                        field[i, j] = '█';
                    else if ( i < 12)
                        field[i, j] = '▒';

                    PlacePixel(i, j);

                    if (xCordinatsNow && j > 0 && j < width - 1 && i == 0)
                        field[i, j] = nums[j - 1];
                    else if (!xCordinatsNow && i > 0 && i < 11 && j == 0)
                        field[i, j] = nums[i - 1];

                    Console.Write((char)field[i, j]);
                }
                Console.WriteLine();
            }
        }
        static void PlacePixel(int i, int j)
        {
            for (int n = 0; n < 4; n++)
            {
                if (shipCorr[i, j] == n + 2)
                    field[i, j] = (char)(Pixel)n;
            }
        }
    }
}