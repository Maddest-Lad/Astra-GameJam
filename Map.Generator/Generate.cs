using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Map.Generator
{
    public class Generate : MonoBehaviour
    {
        [Range(1, 10)] public int numIterations = 5;

        [Range(1, 200)] public int width = 100;

        [Range(1, 200)] public int height = 100;

        private void run()
        {
            var rooms = GenerateRooms();
            var tiles = RoomsToTilemap(rooms);
        }

        private IEnumerable<Room> GenerateRooms()
        {
            // Top Left is (0,0) Bottom Right is (Width, Height)
            var gameMap = new Room(0, width, 0, height);
            var leafNodes = new Queue<Room>();
            leafNodes.Enqueue(gameMap);

            // For Each Iteration, The Number of Rooms is Doubled 
            for (var i = 0; i < numIterations; i++)
            {
                // Tracker for Next Iteration
                var nextLevel = new List<Room>();

                // Split Current Leaf Nodes, Discarding them Afterwards
                while (leafNodes.Count > 0)
                {
                    var current = leafNodes.Dequeue();
                    var (r1, r2) = Room.SplitRoom(current);
                    nextLevel.Add(r1);
                    nextLevel.Add(r2);
                }

                // Add Next Level to the (now empty) Leaf Nodes
                foreach (var room in nextLevel) leafNodes.Enqueue(room);
            }

            return leafNodes.ToList();
        }

        // For Now Fairly Simple - Just Iterating Through Each Room and Mapping It's Tiles to the Tile[][] map
        private Tile[,] RoomsToTilemap(IEnumerable<Room> rooms)
        {
            var tileMap = new Tile[width, height];

            foreach (var room in rooms)
                for (var x = room.left; x < room.right; x++)
                    for (var y = room.top; y < room.bottom; y++)
                        tileMap[x, y] = new Tile(room.isBorderTile(x, y) ? TileType.Wall : TileType.Floor);
            return tileMap;
        }
    }
}