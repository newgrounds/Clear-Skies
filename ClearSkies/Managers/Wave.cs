namespace ClearSkies.Managers
{
    /// <summary>
    /// Structure for an Enemy Wave.
    /// </summary>
    struct Wave
    {
        public int waveNumber;
        public int planesToSpawn;
        public int tanksToSpawn;

        public float planeSpeed;
        public float planeTurnSpeed;
        public float tankSpeed;
        public float tankTurnSpeed;

        public int tanksSpawned;
        public int planesSpawned;

        public int tanksDestroyed;
        public int planesDestroyed;

        public float spawnDelay;
        public int enemiesPerSpawn;

        public float spawnDistance;
    }
}
