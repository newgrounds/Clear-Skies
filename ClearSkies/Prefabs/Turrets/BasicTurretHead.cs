using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using ClearSkies.Content;

namespace ClearSkies.Prefabs.Turrets
{
    class BasicTurretHead : TurretHead
    {
        #region Fields

        private static Vector3 DEFAULT_TURRET_ROTATION = new Vector3(0f, (float)Math.PI / 2f, 0f);
        private static float DEFLAUT_TURRET_ROTATION_SPEED = 1f;

        #endregion

        #region Initializer Methods

        public BasicTurretHead(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard)
            : base(location, rotation, scale, DEFLAUT_TURRET_ROTATION_SPEED, keyboard)
        {
            this.models.Add(ContentLoader.BasicTurretHeadModel);

            addChild(new TurretBarrel(
                new Vector3(location.X, location.Y, location.Z),
                new Vector3(rotation.X, rotation.Y, rotation.Z) + DEFAULT_TURRET_ROTATION,
                scale,
                ContentLoader.BasicTurretBarrelModel,
                keyboard
                ));
        }

        #endregion
    }
}
