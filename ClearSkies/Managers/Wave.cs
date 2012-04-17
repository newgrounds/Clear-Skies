namespace ClearSkies.Managers
{
    /// <summary>
    /// Structure for an Enemy Wave.
    /// </summary>
    struct Wave
    {
        public int waveNumber = 0;
        public int planesToSpawn = 0;
        public int tanksToSpawn = 0;

        public float planeSpeed = 0;
        public float planeTurnSpeed = 0;
        public float tankSpeed = 0;
        public float tankTurnSpeed = 0;

        public int tanksSpawned = 0;
        public int planesSpawned = 0;

        public int tanksDestroyed = 0;
        public int planesDestroyed = 0;

        public float spawnDelay = 0;
        public int enemiesPerSpawn = 0;

        public float spawnDistance = 0;
    }
}
