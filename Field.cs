namespace Warship
{
    internal class Field
    {
        public char[,] field;

        public Field(byte width, byte height)
        {
            field = new char[width, height];
        }
    }
}