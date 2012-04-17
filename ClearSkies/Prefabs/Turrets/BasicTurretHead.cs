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
        #region Initializer Methods

        public BasicTurretHead(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard)
            : base(location + Settings.BASIC_TURRET_HEAD_OFFSET, rotation, scale, Settings.BASIC_TURRET_ROTATION_SPEED, keyboard)
        {
            this.models.Add(ContentLoader.BasicTurretHeadModel);

            addChild(new TurretBarrel(
                new Vector3(this.location.X, this.location.Y, this.location.Z),
                new Vector3(this.rotation.X, this.rotation.Y, this.rotation.Z) + Settings.BASIC_TURRET_BARREL_DEFAULT_ROTATION,
                scale,
                ContentLoader.BasicTurretBarrelModel,
                keyboard
                ));
        }

        #endregion
    }
}
