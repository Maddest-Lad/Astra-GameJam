using System;
using UnityEngine;

namespace Map.Generator
{
    public static class Utils
    {
        public static void Fill2DArray<T>(T[,] arr, T value)
        {
            var numRows = arr.GetLength(0);
            var numCols = arr.GetLength(1);

            for (var i = 0; i < numRows; ++i)
                for (var j = 0; j < numCols; ++j)
                    arr[i, j] = value;
        }

        // Print 2d array to console
        public static void Print2DArray<T>(T[,] arr, bool debug = true)
        {
            var numRows = arr.GetLength(0);
            var numCols = arr.GetLength(1);

            for (var i = 0; i < numRows; ++i)
            {
                for (var j = 0; j < numCols; ++j)
                    if (debug)
                        Debug.Log(arr[i, j]);
                    else
                        Console.Write(arr[i, j] + " ");
                Console.WriteLine();
            }
        }
    }
}