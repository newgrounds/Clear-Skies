using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSkies.Prefabs;
using DI = Microsoft.DirectX.DirectInput;
using Microsoft.DirectX;

namespace ClearSkies.Scripts
{
    /// <summary>
    /// A script that will allow a Prefab to be shifted based on the current key state.
    /// </summary>
    class TurretRotationScript : Script
    {
        #region Fields

        private float translateSpeed;
        private Prefab prefab;
        private DI.Device keyboard;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Script allows a Prefab to translate based on keyboard input at a 
        /// certain speed.
        /// </summary>
        /// <param name="prefab">Prefab to translate.</param>
        /// <param name="keyboard">Keyboard Device to recieve input from.</param>
        /// <param name="translateSpeed">Speed to translate at.</param>
        public TurretRotationScript(Prefab prefab, DI.Device keyboard, float translateSpeed)
        {
            this.prefab = prefab;
            this.keyboard = keyboard;
            this.translateSpeed = translateSpeed;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Translates the prefab according to the current KeyState. Will
        /// block movement in a certain direction if it is outside the worlds bounds.
        /// </summary>
        /// <param name="deltaTime"></param>
        public void run(float deltaTime)
        {
            DI.KeyboardState keys = keyboard.GetCurrentKeyboardState();
            Vector3 location = Vector3.Empty;

            if (keys[DI.Key.Up] || keys[DI.Key.UpArrow] || keys[DI.Key.W])
            {
                location.Z -= translateSpeed * deltaTime;
            }
            if (keys[DI.Key.Left] || keys[DI.Key.LeftArrow] || keys[DI.Key.A])
            {
                location.X += translateSpeed * deltaTime;
            }
            if (keys[DI.Key.Down] || keys[DI.Key.DownArrow] || keys[DI.Key.S])
            {
                location.Z += translateSpeed * deltaTime;
            }
            if (keys[DI.Key.Right] || keys[DI.Key.RightArrow] || keys[DI.Key.D])
            {
                location.X -= translateSpeed * deltaTime;
            }

            Matrix rotationMatrix = Matrix.RotationY(prefab.Rotation.X);
            Vector4 transformedReference = Vector3.Transform(location, rotationMatrix);

            location = new Vector3(transformedReference.X, transformedReference.Y, transformedReference.Z) + prefab.Location;

            // Keeps the Prefab inside world
            if (location.X > 20)
            {
                location.X = 20f;
            }
            else if (location.X < -20)
            {
                location.X = -20f;
            }

            if (location.Z > 20)
            {
                location.Z = 20f;
            }
            else if (location.Z < -20)
            {
                location.Z = -20f;
            }

            prefab.Location = location;
        }

        #endregion
    }
}
