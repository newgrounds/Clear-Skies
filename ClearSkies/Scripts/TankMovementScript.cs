using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Prefabs;
using ClearSkies.Prefabs.Enemies;
using ClearSkies.Prefabs.Enemies.Tanks;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A script that will move a Tank Prefab forward
    /// </summary>
    class TankMovementScript : Script
    {
        #region Fields

        private Tank tank;
        private Prefab target;

        private MoveState state;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Script allows a Tank to move at a certain speed.
        /// </summary>
        /// <param name="turret">Tank to translate.</param>
        /// <param name="movementSpeed">Speed to move at.</param>
        public TankMovementScript(Tank tank, Prefab target)
        {
            this.tank = tank;
            this.target = target;

            this.state = MoveState.Turn;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Moves the prefab tank toward the turret.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last Update</param>
        public void run(float deltaTime)
        {
            Vector3 nextLocation = tank.Location;

            nextLocation += new Vector3(
                (float)(Math.Sin(tank.Rotation.X) * tank.DriveSpeed * deltaTime),
                0f,
                (float)(Math.Cos(tank.Rotation.X) * tank.DriveSpeed * deltaTime));

            if ((nextLocation - target.Location).Length() < tank.ColliderSize)
            {
                tank.detectCollision(target);  // explode
                target.detectCollision(tank);
            }

            switch (state)
            {
                case MoveState.Approach:
                    if ((tank.Location - target.Location).Length() < (nextLocation - target.Location).Length())
                    {
                        state = MoveState.Turn;
                    }
                    break;
                case MoveState.Turn:
                    Vector3 leftRotation = tank.Rotation;
                    leftRotation.X -= tank.TurnSpeed * deltaTime;
                    Vector3 rightRotation = tank.Rotation;
                    rightRotation.X += tank.TurnSpeed * deltaTime;

                    Vector3 turnLeftLocation = tank.Location +
                        new Vector3(
                            (float)(Math.Sin(leftRotation.X) * tank.DriveSpeed * deltaTime),
                            0f,
                            (float)(Math.Cos(leftRotation.X) * tank.DriveSpeed * deltaTime));
                    Vector3 turnRightLocation = tank.Location +
                        new Vector3(
                            (float)(Math.Sin(rightRotation.X) * tank.DriveSpeed * deltaTime),
                            0f,
                            (float)(Math.Cos(rightRotation.X) * tank.DriveSpeed * deltaTime));

                    if ((nextLocation - target.Location).Length() < (turnLeftLocation - target.Location).Length() &&
                        (nextLocation - target.Location).Length() < (turnRightLocation - target.Location).Length())
                    {
                        state = MoveState.Approach;
                    }
                    else if ((turnLeftLocation - target.Location).Length() < (turnRightLocation - target.Location).Length())
                    {
                        nextLocation = turnLeftLocation;
                        tank.Rotation = leftRotation;
                    }
                    else
                    {
                        nextLocation = turnRightLocation;
                        tank.Rotation = rightRotation;
                    }
                    break;
            }

            tank.Location = nextLocation;
        }

        #endregion

        enum MoveState
        {
            Approach,
            Turn
        }
    }
}
