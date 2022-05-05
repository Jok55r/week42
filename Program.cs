using System;

namespace Warship
{
    class Player
    {
        public byte ships = 0;
        public bool itsTurn = true;
        public byte needsToPlace = 10;
    }

    internal class Program
    {
        public const byte players = 2;
        public const byte height = 23;
        public const byte width = 12;
        static void Main(string[] args)
        {
            Player firstPlayer = new Player();
            Player secondPlayer = new Player();
            char[,] field = MakeScreen(firstPlayer, secondPlayer);

            while (!EndGame(firstPlayer, secondPlayer))
            {
                DrawScreen(field);
                if (firstPlayer.itsTurn) HitSomething(firstPlayer, secondPlayer, field, false);
                else if (secondPlayer.itsTurn) HitSomething(secondPlayer, firstPlayer, field, true);
            }
            Console.WriteLine();
            if (firstPlayer.ships == 0) Console.WriteLine("---second player wins!---");
            else  Console.WriteLine("---first player wins!---");
            Console.ReadLine();
        }
        static void HitSomething(Player currentPlayer, Player opponentPlayer, char[,] field, bool isSecondPlayer)
        {
            currentPlayer.itsTurn = false;

            if (Console.ReadLine()[0] == 'r') Main(new string[0]);
            char[] abcs = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            byte xCor = (byte)(Console.ReadLine()[1] - 47);
            byte yCor = 0;

            if (isSecondPlayer) xCor += 11;
            for (byte i = 0; i < abcs.Length; i++) if (abcs[i] == Console.ReadLine()[0]) yCor = (byte)(i + 1);

            for (byte i = 1; i < field.GetLength(0) - 1; i++)
            {
                for (byte j = 1; j < field.GetLength(1) - 1; j++)
                {
                    if (i == yCor && j == xCor && field[i, j] == ' ') {
                        field[i, j] = 'O';
                        opponentPlayer.itsTurn = true;
                        i = (byte)(field.GetLength(0) - 1);
                        break;
                    }
                    else if (i == yCor && j == xCor && field[i, j] == '*') {
                        field[i, j] = 'X';
                        currentPlayer.itsTurn = true;
                        opponentPlayer.ships--;
                        i = (byte)(field.GetLength(0) - 1);
                        break;
                    }
                }
            }
        }
        static void DrawScreen(char[,] field)
        {
            Console.Clear();
            for (byte i = 0; i < field.GetLength(0); i++)
            {
                for (byte j = 0; j < field.GetLength(1); j++) Console.Write(field[i, j]);
                Console.WriteLine();
            }
        }
        static bool EndGame(Player firstPlayer, Player secondPlayer)
        {
            if (firstPlayer.ships == 0 || secondPlayer.ships == 0) return true;
            return false;
        }
        static char[,] MakeScreen(Player firstPlayer, Player secondPlayer)
        {
            Random rnd = new Random();
            char[,] field = new char[width, height];
            char[] nums = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] abcs = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            for (byte i = 0; i < width; i++)
            {
                for (byte j = 0; j < height; j++)
                {
                    if ((i == width - 1 || i == 0) && j > 0 && j < 11)
                        field[i, j] = nums[j - 1];

                    else if ((i == width - 1 || i == 0) && j > 11 && j < 22)
                        field[i, j] = nums[j - 12];

                    else if ((j == 0 || j == height - 1 ||j == 11) && i > 0 && i < 11)
                        field[i, j] = abcs[i - 1];

                    else if (j < 11 && rnd.Next(0, 7) == 0 && firstPlayer.needsToPlace >= firstPlayer.ships)
                    {
                        field[i, j] = '*';
                        firstPlayer.ships++;
                    }

                    else if (j > 11 && rnd.Next(0, 7) == 0 && secondPlayer.needsToPlace >= secondPlayer.ships)
                    {
                        field[i, j] = '*';
                        secondPlayer.ships++;
                    }

                    else field[i, j] = ' ';
                }
            }
            return field;
        }
    }
}