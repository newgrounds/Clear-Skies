using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using System.Drawing;

namespace ParticleEngine
{
    struct ParticleData
    {
        public float timeAlive;
        public float lifespan;
        public Vector3 spawnLocation;
        public Vector3 acceleration;
        public Vector3 rotation;
        public Vector3 location;
        public float size;
        public float growthRate;
        public Color modColor;
    }
}
