using System;

namespace Warship
{
    internal class Lobby
    {
        public void InLobby()
        {
            const byte height = 23;
            const byte width = 12;

            Field frontField = new Field(width, height);
            Field backField = new Field(width, height);
            Player player1 = new Player(10);
            Player player2 = new Player(10);
            Pixels pixel = new Pixels();
            Random rnd = new Random();

            Console.WriteLine("         ***LOBBY***");

            Console.Write("player 1 name: ");
            player1.name = Console.ReadLine();
            Console.Write("player 2 name: ");
            player2.name = Console.ReadLine();

            Console.WriteLine("       -starting game-");
            Console.ReadKey();

            Game game = new Game();
            game.GameStart(player1, player2, frontField, backField, pixel, rnd);
        }
    }
}