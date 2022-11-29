namespace Enums
{
    public enum TileContent
    {
        /// <summary>
        /// Avalaible to build/pass.
        /// </summary>
        Empty,
        /// <summary>
        /// Destination tile.
        /// </summary>
        Destination,
        /// <summary>
        /// Wall tile (probably don't need it).
        /// </summary>
        Wall,
        /// <summary>
        /// Spawn tile.
        /// </summary>
        SpawnPoint,
        /// <summary>
        /// Tower tile.
        /// </summary>
        Tower,
        /// <summary>
        /// Unable to build.
        /// </summary>
        NonBuildable,
        /// <summary>
        /// Unable to build/pass.
        /// </summary>
        Restricted
    }
}
