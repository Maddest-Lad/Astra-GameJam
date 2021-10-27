namespace Map.Generator
{
    public struct Tile
    {
        TileType Type;
        bool Locked;

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
    }
}