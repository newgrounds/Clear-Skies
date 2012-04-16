using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Content;
using ClearSkies.Scripts;
using ClearSkies.Prefabs.Bullets;
using ClearSkies.Managers;
using ParticleEngine;

namespace ClearSkies.Prefabs.Enemies.Planes
{
    class BasicPlane : Plane
    { 
        public BasicPlane(Vector3 location, Vector3 rotation, Vector3 scale, float flightSpeed, float turnSpeed) : 
            base(ContentLoader.BasicPlaneModel, location, rotation, scale, Settings.PLANE_COLLIDER_SIZE)
        {
            this.scripts.Add(new PlaneFlyOverScript(this, Vector3.Empty, flightSpeed, turnSpeed));
            this.scripts.Add(new DropBombScript(this, Vector3.Empty, Settings.PLANE_RELOAD_TIME));
        }
    }
}
