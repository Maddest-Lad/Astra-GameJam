using System;

namespace Map.Generator
{
    class Room
    {
        private static Random random = new Random();
        public int left;
        public int right;
        public int top;
        public int bottom;

        public Room(int left, int right, int top, int bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

        // Determine if a point is inside the room
        public bool isBorderTile(int x, int y)
        {
            return (x == left || x == right || y == top || y == bottom);
        }

        public static (Room, Room) SplitRoom(Room room)
        {
            var direction = (Direction) random.Next(0, 1); //TODO Verify that this works
            int length, middle;
            
            // Generate Random Middle Between 80% and 120% of the length (ie Generally Middle)
            switch (direction)
            {
                case Direction.HORIZONTAL:
                    length = Math.Abs(room.top - room.bottom);
                    middle = random.Next((int) ((length / 2) * 0.8f), (int) ((length / 2) * 1.2));
                    return (new Room(room.left, room.right, room.top, middle), new Room(room.left, room.right, middle, room.bottom));

                case Direction.VERTICAL:
                    length = Math.Abs(room.left - room.right);
                    middle = random.Next((int) ((length / 2) * 0.8f), (int) ((length / 2) * 1.2));
                    return (new Room(middle, room.right, room.top, room.bottom), new Room(room.left, middle, room.top, room.bottom));

                default:
                    throw new Exception("Invalid Direction");
            }
        }
    }
}