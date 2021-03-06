﻿using ClearSkies.Content;
using ClearSkies.Scripts;
using Microsoft.DirectX;
using ClearSkies.Managers;

namespace ClearSkies.Prefabs.Enemies.Tanks
{
    /// <summary>
    /// An Basic Enemy Tank that will drive to the user and shoot.
    /// </summary>
    class BasicTank : Tank
    {
        #region Initializer Methods

        /// <summary>
        /// Creates a simple Tank at the given location, facing the given
        /// rotation, scaled to the given amount, driving at the given speed,
        /// and rotating at the given speed.
        /// </summary>
        /// <param name="location">Location of the Tank in the gameworld</param>
        /// <param name="rotation">Rotation the Tank is facing</param>
        /// <param name="scale">Scale of the Tank</param>
        /// <param name="driveSpeed">Speed the Tank drives at</param>
        /// <param name="turnSpeed">Speed the Tank turns at</param>
        public BasicTank(Vector3 location, Vector3 rotation, Vector3 scale, float driveSpeed, float turnSpeed)
            : base(ContentLoader.BasicTankBodyModel, location, rotation, scale, driveSpeed, turnSpeed, Settings.TANK_COLLIDER_SIZE)
        {
            this.children.Add(new BasicTankHead(this, location, rotation, scale));
            this.scripts.Add(new TankMovementScript(this, TurretManager.ManagedTurrets[0]));
        }

        #endregion
    }
}