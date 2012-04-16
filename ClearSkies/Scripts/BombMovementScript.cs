using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using Microsoft.DirectX;

namespace ClearSkies.Scripts
{
    class BombMovementScript : Script
    {
        private Prefab bomb;
        private float speed;

        public BombMovementScript(Prefab bomb, float speed)
        {
            this.bomb = bomb;
            this.speed = speed;
        }

        public void run(float deltaTime)
        {
            Vector3 nextLocation = bomb.Location;
            nextLocation.Y -= speed * deltaTime;
            bomb.Location = nextLocation;
        }
    }
}
