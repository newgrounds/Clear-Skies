using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace ParticleEngine
{
    class ParticleComparer : IComparer<ParticleData>
    {
        private Vector3 cameraLocation;

        public ParticleComparer(Vector3 cameraLocation) 
        { 
            this.cameraLocation = cameraLocation;
        }

        
        public int Compare(ParticleData particle1, ParticleData particle2)
        {
            int sortValue = 0;

            float p1 = (particle1.location - cameraLocation).Length();
            float p2 = (particle2.location - cameraLocation).Length();
            
            if (p1 > p2)
            {
                sortValue = 1;
            }
            else
            {
                sortValue = -1;
            }

            return sortValue;
        }
    }
}
