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

        public BasicTurret(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard)
            : base(location, rotation, scale,
                new BasicTurretHead(location + new Vector3(0f, 3.5f, 0f), rotation, scale, keyboard))
        {
            this.models.Add(ContentLoader.BasicTurretBaseModel);
        }

        #endregion
    }
}