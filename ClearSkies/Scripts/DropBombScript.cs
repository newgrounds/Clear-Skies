using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using Microsoft.DirectX;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Bullets;

namespace ClearSkies.Scripts
{
    class DropBombScript : Script
    {
        private Prefab bomber;
        private Vector3 target;
        private float reloadTime;
        private float timeSinceLastBomb;

        public DropBombScript(Prefab bomber, Vector3 target, float reloadTime)
        {
            this.bomber = bomber;
            this.target = target;
            this.reloadTime = reloadTime;
            this.timeSinceLastBomb = reloadTime;
        }

        public void run(float deltaTime)
        {
            timeSinceLastBomb += deltaTime;

            bool closeToTarget = new Vector3(bomber.Location.X - target.X, 0, bomber.Location.Z - target.Z).Length() < Settings.BOMB_DROP_DISTANCE_FROM_TARGET;

            if (timeSinceLastBomb > reloadTime && closeToTarget)
            {
                BulletManager.spawn(BulletType.Bomb, bomber, bomber.Location, bomber.Rotation, bomber.Scale);
                timeSinceLastBomb = 0f;
            }
        }
    }
}
