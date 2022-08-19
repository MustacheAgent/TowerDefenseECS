namespace Scripts
{
    public static class PathfindingExtensions
    {
        public static int CalculateIndex(int x, int y, int width)
        {
            return x + y * width;
        }
    }
}
