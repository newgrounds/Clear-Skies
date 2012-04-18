using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs.Enemies.Tanks;
using Microsoft.DirectX;
using ClearSkies.Managers;

namespace ClearSkies.Scripts
{
    class TankHeadRotationScript : Script
    {
        #region Fields

        TankHead tankHead;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Script allows a TankHead to rotate based on keyboard input at a 
        /// certain speed.
        /// </summary>
        /// <param name="turret">Turret to translate.</param>
        public TankHeadRotationScript(TankHead tankHead)
        {
            this.tankHead = tankHead;
        }

        #endregion

        #region Public Methods

        public void run(float deltaTime)
        {
            Vector3 rotation = tankHead.Rotation;
            float barrelRotation = tankHead.BarrelRotation;

            Vector3 targetVector = TurretManager.ManagedTurrets[0].Location - tankHead.Location;
            rotation.X = (float)Math.Atan2(targetVector.X, targetVector.Z);
            tankHead.Rotation = rotation;

            Vector2 distanceXZ = new Vector2(targetVector.X, targetVector.Z);
            tankHead.BarrelRotation = (float)Math.Atan2(distanceXZ.Length(), targetVector.Y);
        }

        #endregion
    }
}
