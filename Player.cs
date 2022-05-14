namespace Warship
{
    internal class Player
    {
        public byte ships;
        public byte needsToPlace;
        public byte wins = 0;
        public bool itsTurn = true;
        public string name;

        public Player(byte aShips)
        {
            ships = aShips;
            needsToPlace = ships;
        }
    }
}