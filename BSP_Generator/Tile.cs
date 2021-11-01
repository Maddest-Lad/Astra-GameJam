namespace BSP_Generator
{
    public struct Tile
    {
        public TileType Type;
        public bool Locked;

        public Tile(TileType type, bool locked)
        {
            Type = type;
            Locked = locked;
        }

        public Tile(TileType type)
        {
            Type = type;
            Locked = false;
        }

        public override string ToString()
        {
            return "" + (int) Type;
        }
    }
}