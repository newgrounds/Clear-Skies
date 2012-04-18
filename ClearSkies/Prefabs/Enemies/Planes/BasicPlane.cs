using ClearSkies.Content;
using ClearSkies.Scripts;
using Microsoft.DirectX;

namespace ClearSkies.Prefabs.Enemies.Planes
{
    /// <summary>
    /// A Plane unit that will fly over and around the target location.
    /// </summary>
    class BasicPlane : Plane
    {
        /// <summary>
        /// Creates a Basic Plane at the given location facing the given 
        /// rotation, flying with the given speeds, and scaled to the give
        /// amount.
        /// </summary>
        /// <param name="location">The location of the Plane</param>
        /// <param name="rotation">The rotaiton of the Plane</param>
        /// <param name="scale">The scale of the Plane model</param>
        /// <param name="flightSpeed">The speed the Plane will fly at</param>
        /// <param name="turnSpeed">The speed the Plane will turn at</param>
        public BasicPlane(Vector3 location, Vector3 rotation, Vector3 scale, float flightSpeed, float turnSpeed) :
            base(ContentLoader.BasicPlaneModel, location, rotation, scale, flightSpeed, turnSpeed, Settings.BASIC_PLANE_COLLIDER_SIZE)
        {
            this.scripts.Add(new PlaneFlyOverScript(this, Vector3.Empty));
            this.scripts.Add(new DropBombScript(this, Vector3.Empty, Settings.PLANE_RELOAD_TIME));
        }
    }
}