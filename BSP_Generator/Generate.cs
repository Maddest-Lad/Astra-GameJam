using System.Collections.Generic;
using System.Linq;
using BSP_Generator;
using UnityEngine;

namespace BSP_Generator
{
    public class Generate : MonoBehaviour
    {
        public static Tile[,] GenerateMap(int width, int height, int numIterations)
        {
            var rooms = GenerateRooms(width, height, numIterations);
            return RoomsToTilemap(rooms, width, height);
        }

        private static IEnumerable<Room> GenerateRooms(int width, int height, int numIterations)
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
                    var (r1, r2) = Room.AlternateSplitRoom(current);
                    nextLevel.Add(r1);
                    nextLevel.Add(r2);
                }

                // Add Next Level to the (now empty) Leaf Nodes
                foreach (var room in nextLevel) { 
                    leafNodes.Enqueue(room);
                }
                
            }
            
            // // Randomly Remove 40% Of the Items in leafNodes
            // var randomRooms = 
            // var random = new System.Random();
            // var numToRemove = (int) (randomRooms.Count * 0.4);
            // for (var i = 0; i < numToRemove; i++)
            // {
            //     var index = random.Next(randomRooms.Count);
            //     randomRooms.RemoveAt(index);
            // }

            return leafNodes.ToList();; 
        }

        // For Now Fairly Simple - Just Iterating Through Each Room and Mapping It's Tiles to the Tile[][] map
        private static Tile[,] RoomsToTilemap(IEnumerable<Room> rooms, int width, int height)
        {
            var tileMap = new Tile[width, height];
            Utils.Fill2DArray(tileMap, new Tile(TileType.Empty));
            
            // For Each Room, Iterate Through Each Tile and Set the Tile Type
            foreach (var room in rooms)
            {
                for(var i = room.inside.left; i < room.inside.right; i++)
                {
                    for(var j = room.inside.top; j < room.inside.bottom; j++)
                    {
                        if(room.isBorderTile(i, j)) {
                            tileMap[i, j] = new Tile(TileType.Wall);
                        } else {
                            tileMap[i, j] = new Tile(TileType.Floor);
                        }
                    }
                }
            }
            return tileMap;
        }
    }
}