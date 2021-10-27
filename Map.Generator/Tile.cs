namespace Map.Generator
{
    public struct Tile
    {
        private TileType Type;
        private bool Locked;

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