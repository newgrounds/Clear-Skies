using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace ClearSkies.Prefabs.Turrets
{
    class TurretBarrel : Prefab
    {
        public TurretBarrel(Model barrelModel, Vector3 location, Vector3 rotation)
        {
            this.models.Add(barrelModel);
            this.location = location;
            this.rotation = rotation;
        }
    }
}
