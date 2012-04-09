using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ClearSkies.Prefabs.Cameras
{
    /// <summary>
    /// A simple Third Person Camera that will chance the main prefab around at a set distance.
    /// </summary>
    class ThirdPersonCamera : Prefab
    {
        #region Fields

        private Prefab focusOn;
        private Device device;

        #endregion

        public ThirdPersonCamera(Device device, Prefab focusOn, Vector3 location)
        {
            this.device = device;
            this.location = location;
            this.focusOn = focusOn;
        }

        #region Public Methods

        /// <summary>
        /// Sets the devices View and Projection matrixes. Also readjusts 
        /// the cameras position to be behind the prefab it focuses on.
        /// </summary>
        public void view()
        {
            device.Transform.View = Matrix.LookAtLH(Location, focusOn.Location, new Vector3(0, 1f, 0));
            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4f, 1f, 1f, 100f);
        }

        #endregion
    }
}
