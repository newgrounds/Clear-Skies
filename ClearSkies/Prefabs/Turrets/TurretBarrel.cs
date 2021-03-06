﻿using System;
using ClearSkies.Content;
using ClearSkies.Scripts;
using Microsoft.DirectX;
using D3D = Microsoft.DirectX.Direct3D;
using DI = Microsoft.DirectX.DirectInput;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// A part of a Turret used to launch projectiles. Should only be used 
    /// with Turret Prefabs.
    /// </summary>
    class TurretBarrel : Prefab
    {
        #region Fields

        private DI.Device keyboard;
        private float minRotation;
        private float maxRotation;
        private float shootDelay;
        private float pushSpeed;
        private float pullSpeed;

        private Vector3 drawLocation;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a single TurretBarrel which will be represented by the
        /// given Model at the given location facing the given rotation. The
        /// TurretBarrel will be controlled by the given keyboard Device.
        /// </summary>
        /// <param name="barrelModel">
        /// Model to represent the TurretBarrel
        /// </param>
        /// <param name="location">Location of the TurretBarrel</param>
        /// <param name="rotation">Rotation the TurretBarrel is facing</param>
        /// <param name="scale">Scale of the TurretBarrel</param>
        /// <param name="keyboard">
        /// Keyboard Device used to control the TurretBarrel
        /// </param>
        public TurretBarrel(Vector3 location, Vector3 rotation, Vector3 scale, Model barrelModel, float maxRotation,
            float minRotation, float shootDelay, float pushSpeed, float pullSpeed, DI.Device keyboard)
            : base(location, rotation, scale)
        {
            this.keyboard = keyboard;
            this.minRotation = minRotation;
            this.maxRotation = maxRotation;
            this.drawLocation = location;
            this.shootDelay = shootDelay;
            this.pushSpeed = pushSpeed;
            this.pullSpeed = pullSpeed;

            this.models.Add(barrelModel);
            this.scripts.Add(new TurretShootScript(this));
        }

        #endregion

        #region Getter and Setter Methods

        public Vector3 DrawLocation
        {
            get { return this.drawLocation; }
            set { this.drawLocation = value; }
        }

        /// <summary>
        /// The location of the TurretBarrel. Barrel contains a special draw location
        /// variable is moved according to the given location.
        /// </summary>
        public override Vector3 Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                drawLocation += value - this.Location;

                base.Location = value;
            }
        }
        /// <summary>
        /// The rotation of the TurretBarrel. This value is clamped to a 
        /// 90 degree turn.
        /// </summary>
        public override Vector3 Rotation
        {
            get { return this.rotation; }
            set
            {
                this.rotation = value;

                if (rotation.Y < minRotation)
                {
                    this.rotation.Y = minRotation;
                }
                else if (rotation.Y >= maxRotation)
                {
                    this.rotation.Y = maxRotation;
                }
            }
        }

        public DI.Device Keyboard
        {
            get { return this.keyboard; }
        }

        public float ShootDelay
        {
            get { return this.shootDelay; }
        }

        public float PushSpeed
        {
            get { return this.pushSpeed; }
        }

        public float PullSpeed
        {
            get { return this.pullSpeed; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draws the TurretBarrel to the given Device. Overloaded to allow a 
        /// special draw position used to keep the barrels from running away
        /// during animation.
        /// </summary>
        /// <param name="device">The Device to draw to</param>
        public override void draw(D3D.Device device)
        {
            //do transformations
            device.Transform.World = Matrix.Multiply(
                Matrix.RotationYawPitchRoll(rotation.X, rotation.Y, rotation.Z),
                Matrix.Translation(drawLocation.X, drawLocation.Y, drawLocation.Z));

            foreach (Model model in models)
            {
                model.draw(device);
            }

            foreach (Prefab child in children)
            {
                child.draw(device);
            }
        }

        #endregion
    }
}
