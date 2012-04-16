using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using Microsoft.DirectX;

namespace ClearSkies.Scripts
{
    class PlaneFlyOverScript : Script
    {
        private Prefab plane;
        private Vector3 target;
        private float flightSpeed;
        private float turnSpeed;
        private FlightState state;
        private float overrunTime;

        public PlaneFlyOverScript(Prefab plane, Vector3 target, float flightSpeed, float turnSpeed)
        {
            this.plane = plane;
            this.target = target;
            this.flightSpeed = flightSpeed;
            this.turnSpeed = turnSpeed;
            this.state = FlightState.Turn;
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
                (float)(Math.Sin(plane.Rotation.X) * flightSpeed * deltaTime),
                0f,
                (float)(Math.Cos(plane.Rotation.X) * flightSpeed * deltaTime));

            float oldDist = (plane.Location - target).Length();
            float newDist = (nextLocation - target).Length();

            bool isCloser = oldDist > newDist;
            switch (state)
            {
                case FlightState.Approach:
                    if (!isCloser)
                    {
                        state = FlightState.Turn;
                    }
                    break;
                case FlightState.Overrun:
                    overrunTime += deltaTime;
                    if (overrunTime > Settings.PLANE_FLY_OVER_OVERRUN_TIME)
                    {
                        overrunTime = 0f;
                        state = FlightState.Turn;
                    }
                    break;
                case FlightState.Turn:
                    Vector3 leftRotation = plane.Rotation;
                    leftRotation.X -= turnSpeed * deltaTime;
                    Vector3 rightRotation = plane.Rotation;
                    rightRotation.X += turnSpeed * deltaTime;

                    Vector3 turnLeftLocation = plane.Location +
                        new Vector3(
                            (float)(Math.Sin(leftRotation.X) * flightSpeed * deltaTime),
                            0f,
                            (float)(Math.Cos(leftRotation.X) * flightSpeed * deltaTime));
                    Vector3 turnRightLocation = plane.Location +
                        new Vector3(
                            (float)(Math.Sin(rightRotation.X) * flightSpeed * deltaTime),
                            0f,
                            (float)(Math.Cos(rightRotation.X) * flightSpeed * deltaTime));

                    if ((nextLocation - target).Length() < (turnLeftLocation - target).Length() &&
                        (nextLocation - target).Length() < (turnRightLocation - target).Length())
                    {
                        state = FlightState.Approach;
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

        enum FlightState
        {
            Approach,
            Overrun,
            Turn
        }
    }
}
