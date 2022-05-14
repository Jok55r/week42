using System;

namespace Warship
{
    class Game
    {
        public void GameStart(Player player1, Player player2, Field frontField, Field backField, Pixels pixel, Random rnd)
        {
            MakeFields(frontField.field, backField.field, player1, player2, pixel, rnd);
            while (!EndGame(frontField, backField, player1, player2, pixel, rnd))
            {
                DrawField(frontField.field, player1, player2);
                if (player1.itsTurn) HitSomething(player1, player2, false, frontField.field, backField.field, pixel);
                else if (player2.itsTurn) HitSomething(player2, player1, true, frontField.field, backField.field, pixel);
            }
            Console.Clear();
            Console.WriteLine(EndMessage(player1, player2));
            Console.ReadKey();
        }

        void MakeFields(char[,] frontField, char[,] backField, Player player1, Player player2, Pixels pixel, Random rnd)
        {
            string[] emptyField = 
            { " abcdefghij abcdefghij ",
              "0          0          0",
              "1          1          1",
              "2          2          2",
              "3          3          3",
              "4          4          4",
              "5          5          5",
              "6          6          6",
              "7          7          7",
              "8          8          8",
              "9          9          9",
              " abcdefghij abcdefghij ",
            };

            for (byte y = 0; y < frontField.GetLength(0); y++)
            {
                for (byte x = 0; x < frontField.GetLength(1); x++)
                {
                    bool isCor = y == frontField.GetLength(0) - 1 || y == 0 || x == frontField.GetLength(1) - 1 || x == 0 || x == frontField.GetLength(1) / 2;

                    if (!isCor && rnd.Next(0, 7) == 1 && x < frontField.GetLength(1) / 2 && player1.needsToPlace > 0) {
                        backField[y, x] = pixel.ship;
                        player1.needsToPlace--;
                    }

                    else if (!isCor && rnd.Next(0, 7) == 1 && x > frontField.GetLength(1) / 2 && player2.needsToPlace > 0) {
                        backField[y, x] = pixel.ship;
                        player2.needsToPlace--;
                    }

                    else {
                        frontField[y, x] = emptyField[y][x];
                        backField[y, x] = emptyField[y][x];
                    }
                }
            }
            player1.ships -= player1.needsToPlace;
            player2.ships -= player2.needsToPlace;
        }

        void DrawField(char[,] frontField, Player player1, Player player2)
        {
            Console.Clear();
            for (byte x = 0; x < frontField.GetLength(0); x++)
            {
                for (byte y = 0; y < frontField.GetLength(1); y++)
                {
                    Console.Write(frontField[x, y]);
                }
                if (x == 0) Console.Write($"    {player1.name} wins: {player1.wins}");
                if (x == 1) Console.Write($"    {player2.name} wins: {player2.wins}");
                Console.WriteLine();
            }
        }

        void HitSomething(Player currentPlayer, Player opponentPlayer, bool isSecondPlayer, char[,] frontField, char[,] backField, Pixels pixel)
        {
            string input = Console.ReadLine();

            if (input == "") return;

            currentPlayer.itsTurn = false;
            opponentPlayer.itsTurn = true;

            string abcs = "abcdefghij";

            byte halfF = (byte)(frontField.GetLength(1) / 2);

            byte xCor = (byte)(input[1] - 47);
            byte yCor = 0;

            if (isSecondPlayer) yCor += halfF;
            for (byte i = 0; i < abcs.Length; i++) 
            {
                if (abcs[i] == input[0])
                {
                    yCor += (byte)(i + 1);
                    break;
                }
            }

            for (byte x = 1; x < frontField.GetLength(0) - 1; x++)
            {
                for (byte y = 1; y < frontField.GetLength(1) - 1; y++)
                {
                    bool sameCor = y == yCor && x == xCor;

                    if (sameCor && backField[x, y] == pixel.ship)
                    {
                        frontField[x, y] = pixel.deadShip;
                        opponentPlayer.itsTurn = false;
                        currentPlayer.itsTurn = true;
                        opponentPlayer.ships--;

                        x = (byte)(frontField.GetLength(0) - 1);
                        break;
                    }

                    else if (sameCor && backField[x, y] == pixel.empty)
                    {
                        frontField[x, y] = pixel.miss;

                        x = (byte)(frontField.GetLength(0) - 1);
                        break;
                    }
                }
            }
        }

        bool EndGame(Field frontField, Field backField, Player player1, Player player2, Pixels pixel, Random rnd)
        {
            if (player1.ships == 0)
            {
                player1.wins++;
                MakeFields(frontField.field, backField.field, player1, player2, pixel, rnd);
            }

            else if (player2.ships == 0)
            {
                player2.wins++;
                MakeFields(frontField.field, backField.field, player1, player2, pixel, rnd);
            }

            if (player1.wins == 3 || player2.wins == 3) return true;

            return false;
        }

        string EndMessage(Player player1, Player player2)
        {
            if (player1.wins == 3) 
                return $"---{player1.name} win!---";

            else
                return $"---{player2.name} win!---";
        }
    }
}