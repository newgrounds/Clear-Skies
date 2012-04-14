using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ParticleEngine
{
    interface ParticleEmitter
    {
        void updateParticles(float deltaTime);
        void draw(Vector3 cameraRotation, Device device);
    }
}
