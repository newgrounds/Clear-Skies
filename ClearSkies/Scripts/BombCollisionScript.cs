using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using ClearSkies.Managers;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Prefabs.Bullets;
using ClearSkies.Prefabs.Turrets;

namespace ClearSkies.Scripts
{
    class BombCollisionScript : Script
    {
        private Bullet bomb;

        public BombCollisionScript(Bullet bomb)
        {
            this.bomb = bomb;
        }

        public void run(float deltaTime)
        {
            bool collisionDetected = false;
            
            foreach (Prefab prefab in EnemyManager.ManagedEnemies)
            {
                Enemy enemy = (Enemy)prefab;
                if (bomb.Owner != null && bomb.Owner != enemy && enemy.ColliderSize > (enemy.Location - bomb.Location).Length())
                {
                    bomb.detectCollision(enemy);
                    enemy.detectCollision(bomb);
                }
            }

            foreach (Prefab prefab in TurretManager.ManagedTurrets)
            {
                Turret turret = (Turret)prefab;
                if (bomb.Owner != null && bomb.Owner != turret && turret.ColliderSize > (turret.Location - bomb.Location).Length())
                {
                    bomb.detectCollision(turret);
                    turret.detectCollision(bomb);
                }
            }

            if (!collisionDetected && bomb.Location.Y < 0)
            {
                bomb.detectCollision(null); // this means explode with nothing
            }
        }
    }
}
