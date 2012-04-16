using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using ClearSkies.Content;

namespace ClearSkies.Prefabs.Turrets
{
    class BasicTurret : Turret
    {
        #region Initializer Methods

        public BasicTurret(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard) : base(location, rotation, scale,
            new BasicTurretHead(location, rotation, scale, keyboard), Settings.BASIC_TURRET_COLLIDER_SIZE)
        {
            this.models.Add(ContentLoader.BasicTurretBaseModel);
        }

        #endregion
    }
}
