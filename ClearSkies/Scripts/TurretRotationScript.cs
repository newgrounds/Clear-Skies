using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using DI = Microsoft.DirectX.DirectInput;
using Microsoft.DirectX;
using ClearSkies.Prefabs.Turrets;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A script that will allow a Turret Prefab to be shifted based on the 
    /// current key state.
    /// </summary>
    class TurretRotationScript : Script
    {
        #region Fields

        private float rotationSpeed;
        private TurretHead turret;
        private DI.Device keyboard;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Script allows a Turret to rotate based on keyboard input at a 
        /// certain speed.
        /// </summary>
        /// <param name="turret">Turret to translate.</param>
        /// <param name="keyboard">Keyboard Device to recieve input from.</param>
        /// <param name="translateSpeed">Speed to translate at.</param>
        public TurretRotationScript(TurretHead turret, DI.Device keyboard, float rotationSpeed)
        {
            this.turret = turret;
            this.keyboard = keyboard;
            this.rotationSpeed = rotationSpeed;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Translates the prefab according to the current KeyState. Will
        /// block movement in a certain direction if it is outside the worlds bounds.
        /// </summary>
        /// <param name="deltaTime">Time in seconds since last Update</param>
        public void run(float deltaTime)
        {
            DI.KeyboardState keys = keyboard.GetCurrentKeyboardState();
            Vector3 rotation = turret.Rotation;

            if (keys[DI.Key.Up] || keys[DI.Key.UpArrow] || keys[DI.Key.W])
            {
                turret.BarrelRotation -= rotationSpeed * deltaTime;
            }
            if (keys[DI.Key.Left] || keys[DI.Key.LeftArrow] || keys[DI.Key.A])
            {
                rotation.X -= rotationSpeed * deltaTime;
            }
            if (keys[DI.Key.Down] || keys[DI.Key.DownArrow] || keys[DI.Key.S])
            {
                turret.BarrelRotation += rotationSpeed * deltaTime;
            }
            if (keys[DI.Key.Right] || keys[DI.Key.RightArrow] || keys[DI.Key.D])
            {
                rotation.X += rotationSpeed * deltaTime;
            }

            turret.Rotation = rotation;
        }

        #endregion
    }
}
