namespace Map.Generator
{
    public class Utils
    {
        public static void Fill2DArray<T>(T[,] arr, T value)
        {
            var numRows = arr.GetLength(0);
            var numCols = arr.GetLength(1);

            for (var i = 0; i < numRows; ++i)
                for (var j = 0; j < numCols; ++j)
                    arr[i, j] = value;
        }
    }
}