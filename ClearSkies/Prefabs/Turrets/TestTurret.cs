using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using ClearSkies.Content;

namespace ClearSkies.Prefabs.Turrets
{
    class TestTurret : Turret
    {
        #region Initializer Methods

        public TestTurret(Vector3 location, Vector3 rotation, Vector3 scale, Device keyboard)
            : base(location, rotation, scale, new TestTurretHead(location + new Vector3(0f, 1.25f, 0f), rotation, scale, keyboard))
        {
            this.models.Add(ContentLoader.TestTurretBaseModel);
            this.rotation = new Vector3(0f, (float)(Math.PI / 2), 0f);
        }

        #endregion
    }
}
