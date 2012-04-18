using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Content;

namespace ClearSkies.Prefabs.Enemies.Tanks
{
    class BasicTankHead : TankHead
    {

        public BasicTankHead(Tank tank, Vector3 location, Vector3 rotation, Vector3 scale) : 
            base(location + Settings.BASIC_TANK_HEAD_OFFSET, rotation, scale, Settings.BASIC_TANK_HEAD_ROTATION_SPEED, Settings.BASIC_TANK_BARREL_ROTATION_SPEED)
        {
            this.models.Add(ContentLoader.BasicTankHeadModel);

            addChild(new TankBarrel(
                tank,
                new Vector3(this.location.X, this.location.Y, this.location.Z) + Settings.BASIC_TANK_BARREL_OFFSET,
                new Vector3(this.rotation.X, this.rotation.Y, this.rotation.Z) + Settings.BASIC_TANK_BARREL_DEFAULT_ROTATION,
                scale,
                ContentLoader.BasicTankBarrelModel,
                Settings.BASIC_TANK_BARREL_MAX_PITCH,
                Settings.BASIC_TANK_BARREL_MIN_PITCH,
                Settings.BASIC_TANK_BARREL_SHOOT_DISTANCE,
                Settings.BASIC_TANK_BARREL_SHOOT_DELAY,
                Settings.BASIC_TANK_BARREL_PUSH_SPEED,
                Settings.BASIC_TANK_BARREL_PULL_SPEED));
        }
    }
}
