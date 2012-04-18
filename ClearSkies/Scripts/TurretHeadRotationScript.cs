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
    class TurretHeadRotationScript : Script
    {
        #region Fields

        private TurretHead turretHead;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Script allows a TurretHead to rotate based on keyboard input at a 
        /// certain speed.
        /// </summary>
        /// <param name="turretHead">TurretHead to translate.</param>
        public TurretHeadRotationScript(TurretHead turretHead)
        {
            this.turretHead = turretHead;
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
            DI.KeyboardState keys = turretHead.Keyboard.GetCurrentKeyboardState();
            Vector3 rotation = turretHead.Rotation;

            if (keys[DI.Key.Up] || keys[DI.Key.UpArrow] || keys[DI.Key.W])
            {
                turretHead.BarrelRotation -= turretHead.BarrelRotationSpeed * deltaTime;
            }
            if (keys[DI.Key.Left] || keys[DI.Key.LeftArrow] || keys[DI.Key.A])
            {
                rotation.X -= turretHead.HeadRotationSpeed * deltaTime;
            }
            if (keys[DI.Key.Down] || keys[DI.Key.DownArrow] || keys[DI.Key.S])
            {
                turretHead.BarrelRotation += turretHead.BarrelRotationSpeed * deltaTime;
            }
            if (keys[DI.Key.Right] || keys[DI.Key.RightArrow] || keys[DI.Key.D])
            {
                rotation.X += turretHead.HeadRotationSpeed * deltaTime;
            }

            turretHead.Rotation = rotation;
        }

        #endregion
    }
}
