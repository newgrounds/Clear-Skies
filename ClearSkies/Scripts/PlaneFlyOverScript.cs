using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using Microsoft.DirectX;
using CS = ClearSkies.Prefabs.Enemies.Planes;

namespace ClearSkies.Scripts
{
    class PlaneFlyOverScript : Script
    {
        private CS.Plane plane;
        private Vector3 target;

        private MoveState state;
        private float overrunTime;

        public PlaneFlyOverScript(CS.Plane plane, Vector3 target)
        {
            this.plane = plane;
            this.target = target;
            this.state = MoveState.Turn;
        }

        #region Public Methods

        /// <summary>
        /// Moves the prefab tank toward the turret.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last Update</param>
        public void run(float deltaTime)
        {
            Vector3 nextLocation = plane.Location;

            nextLocation += new Vector3(
                (float)(Math.Sin(plane.Rotation.X) * plane.FlightSpeed * deltaTime),
                0f,
                (float)(Math.Cos(plane.Rotation.X) * plane.FlightSpeed * deltaTime));

            float oldDist = (plane.Location - target).Length();
            float newDist = (nextLocation - target).Length();

            bool isCloser = oldDist > newDist;
            switch (state)
            {
                case MoveState.Approach:
                    if (!isCloser)
                    {
                        state = MoveState.Turn;
                    }
                    break;
                case MoveState.Overrun:
                    overrunTime += deltaTime;
                    if (overrunTime > Settings.PLANE_FLY_OVER_OVERRUN_TIME)
                    {
                        overrunTime = 0f;
                        state = MoveState.Turn;
                    }
                    break;
                case MoveState.Turn:
                    Vector3 leftRotation = plane.Rotation;
                    leftRotation.X -= plane.TurnSpeed * deltaTime;
                    Vector3 rightRotation = plane.Rotation;
                    rightRotation.X += plane.TurnSpeed * deltaTime;

                    Vector3 turnLeftLocation = plane.Location +
                        new Vector3(
                            (float)(Math.Sin(leftRotation.X) * plane.FlightSpeed * deltaTime),
                            0f,
                            (float)(Math.Cos(leftRotation.X) * plane.FlightSpeed * deltaTime));
                    Vector3 turnRightLocation = plane.Location +
                        new Vector3(
                            (float)(Math.Sin(rightRotation.X) * plane.FlightSpeed * deltaTime),
                            0f,
                            (float)(Math.Cos(rightRotation.X) * plane.FlightSpeed * deltaTime));

                    if ((nextLocation - target).Length() < (turnLeftLocation - target).Length() &&
                        (nextLocation - target).Length() < (turnRightLocation - target).Length())
                    {
                        state = MoveState.Approach;
                    }
                    else if ((turnLeftLocation - target).Length() < (turnRightLocation - target).Length())
                    {
                        nextLocation = turnLeftLocation;
                        plane.Rotation = leftRotation;
                    }
                    else
                    {
                        nextLocation = turnRightLocation;
                        plane.Rotation = rightRotation;
                    }
                    break;
            }

            plane.Location = nextLocation;
        }

        #endregion

        enum MoveState
        {
            Approach,
            Overrun,
            Turn
        }
    }
}
