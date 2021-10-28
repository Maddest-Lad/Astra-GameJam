using System;
using UnityEngine;
using Map.Generator;

namespace Map.Builder
{
    public class Builder : MonoBehaviour
    {
        [Range(100, 1000)] public int width = 100;

        [Range(100, 1000)] public int height = 100;

        [Range(1, 8)] public int numIterations = 5;

        public GameObject wall;
        public GameObject floor;
        public GameObject door;
        public GameObject empty;
        
        public void Start()
        {
            var tiles = Generate.GenerateMap(width, height, numIterations);
            
            // Create the map
            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    var tile = tiles[x, y];
                    GameObject prefab;
                    
                    switch (tile.Type)
                    {
                        case TileType.Wall:
                            prefab = wall;
                            break;
                            
                        case TileType.Floor:
                            prefab = floor;
                            break;
                        
                        case TileType.Door:
                            prefab = door;
                            break;
                        
                        default:
                            prefab = empty;
                            break;
                    }
                    
                    var tileObject = Instantiate(prefab, new Vector3(x, 0, y), Quaternion.identity);
                    tileObject.transform.parent = transform;
                }
            }
        }
        
        
    }
}