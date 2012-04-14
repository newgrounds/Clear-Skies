using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using ClearSkies.Content;

namespace ClearSkies.Prefabs.Turrets
{
    /// <summary>
    /// A simple dual barreled Turret used for testing.
    /// </summary>
    class TestTurretHead : TurretHead
    {
        #region Fields

        private static Vector3 DEFAULT_TURRET_ROTATION = new Vector3(0f, (float)Math.PI / 2f, 0f);
        private static float DEFLAUT_TURRET_ROTATION_SPEED = 1f;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a simple twin barreled Turret at the given location facing
        /// the given rotation and controlled by the given kayboard Device.
        /// </summary>
        /// <param name="location">Location of the Turret in the gameworld</param>
        /// <param name="rotation">Rotation the Turret is facing</param>
        /// <param name="keyboard">Keyboard Device used to controll the Turret</param>
        public TestTurretHead(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard)
            : base(location, rotation, scale, DEFLAUT_TURRET_ROTATION_SPEED, keyboard)
        {
            this.models.Add(ContentLoader.TestTurretHeadModel);

            addChild(new TurretBarrel(
                new Vector3(location.X + 0.25f, location.Y + 0.5f, location.Z + 0.5f),
                new Vector3(rotation.X, rotation.Y, rotation.Z) + DEFAULT_TURRET_ROTATION,
                scale,
                ContentLoader.TestTurretBarrelModel,
                keyboard
                ));

            addChild(new TurretBarrel(
                new Vector3(location.X - 0.25f, location.Y + 0.5f, location.Z + 0.5f),
                new Vector3(rotation.X, rotation.Y, rotation.Z) + DEFAULT_TURRET_ROTATION,
                scale,
                ContentLoader.TestTurretBarrelModel,
                keyboard
                ));
        }

        #endregion
    }
}
