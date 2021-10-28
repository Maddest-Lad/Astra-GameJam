using System;
using System.Diagnostics;
using Random = UnityEngine.Random;

namespace Map.Generator
{
    internal class Room
    {
        private static float RATIO = 0.45f;
        public readonly int bottom;
        public readonly int left;
        public readonly int right;
        public readonly int top; 
        public Room inside;
        private bool isInside;
        
        public Room(int left, int right, int top, int bottom, bool genInside=false)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;

            // Generate Interior Room
            if (genInside)
            {
                int offset = Random.Range(2, 5);
                inside = new Room(left + offset, right - offset, top + offset, bottom - offset);
            }
        }

        // Determine if a point is inside the room
        public bool isBorderTile(int x, int y)
        {
            return x == inside.left || x == inside.right - 1 || y == inside.top || y == inside.bottom - 1;
        }

        public static (Room, Room) AlternateSplitRoom(Room room)
        {
            while (true)
            {
                int mid;
                float r1, r2;
                Room left, right;

                // Split the Containers
                switch (Utils.getRandomDirection())
                {
                    case Direction.HORIZONTAL:
                        mid = Random.Range(room.left, room.right);
                        r1 = Math.Abs((float) (room.left - mid) / (float) (room.top - room.bottom));
                        r2 = Math.Abs((float) (room.right - mid) / (float) (room.top - room.bottom));

                        left = new Room(room.left, mid, room.top, room.bottom, true);
                        right = new Room(mid, room.right, room.top, room.bottom, true);
                        break;

                    case Direction.VERTICAL:
                        mid = Random.Range(room.bottom, room.top);
                        r1 = Math.Abs((float) (room.top - mid) / (room.left - room.right));
                        r2 = Math.Abs((float) (room.bottom - mid) / (room.left - room.right));

                        left = new Room(room.left, room.right, room.top, mid, true);
                        right = new Room(room.left, room.right, mid, room.bottom, true);
                        break;

                    default:
                        return (null, null);
                }

                if (r1 < RATIO || r2 < RATIO)
                {
                    continue;
                }

                return (left, right);
            }
        }

        public override string ToString()
        {
            return $"{left}, {right}, {top}, {bottom}";
        }
    }
}