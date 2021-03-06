﻿using System;
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
    /// <summary>
    /// A simple collision script that detects if a Bullet collides with an
    /// Enemy.
    /// </summary>
    class BulletCollisionScript : Script
    {
        #region Fields

        private Bullet bullet;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates the bullet movement Script to apply to the given Bullet.
        /// </summary>
        /// <param name="bullet">Bullet to apply the Script to</param>
        public BulletCollisionScript(Bullet bullet)
        {
            this.bullet = bullet;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Checks if the Bullet has collided with any Enemies. If a collision
        /// is detected it is registered on the Enemy and the Bullet is marked
        /// as dead.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void run(float deltaTime)
        {
            foreach (Prefab prefab in EnemyManager.ManagedEnemies)
            {
                Enemy enemy = (Enemy)prefab;
                if (bullet.Owner != null && bullet.Owner != enemy && enemy.ColliderSize > (enemy.Location - bullet.Location).Length())
                {
                    bullet.detectCollision(enemy);
                    enemy.detectCollision(bullet);
                }
            }

            foreach (Prefab prefab in TurretManager.ManagedTurrets)
            {
                Turret turret = (Turret)prefab;

                if (bullet.Owner != null && bullet.Owner != turret && turret.ColliderSize > (turret.Location - bullet.Location).Length())
                {
                    bullet.detectCollision(turret);
                    turret.detectCollision(bullet);
                }
            }
        }

        #endregion
    }
}
