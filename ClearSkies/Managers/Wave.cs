using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearSkies.Managers
{
    struct Wave
    {
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
