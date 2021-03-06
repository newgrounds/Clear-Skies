﻿using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ClearSkies.Prefabs.Cameras
{
    /// <summary>
    /// A simple Third Person Camera that will focus on a given Prefab.
    /// </summary>
    class ThirdPersonCamera : Prefab
    {
        #region Fields

        private Prefab focusOn;

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates a ThirdPersonCamera that will focus on the given Prefab.
        /// Note that if you parent the Camera to the Prefab it will follow
        /// the Prefab around the world.
        /// </summary>
        /// <param name="focusOn">Prefab to focus the Camera's view on.</param>
        /// <param name="location">Location of the Camera in the world</param>
        public ThirdPersonCamera(Prefab focusOn, Vector3 location) : 
            base(location, Vector3.Empty, Vector3.Empty)
        {   
            this.focusOn = focusOn;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the devices View and Projection matrixes to focus on its 
        /// target.
        /// </summary>
        public void view(Device device)
        {
            device.Transform.View = Matrix.LookAtLH(Location, focusOn.Location + Settings.CAMERA_FOCUS_POINT_OFFSET, Settings.CAMERA_UP_VECTOR);
            device.Transform.Projection = Matrix.PerspectiveFovLH(Settings.CAMERA_FIELD_OF_VIEW, Settings.CAMERA_ASPECT_RATIO, Settings.CAMERA_NEAR_PLANE, Settings.CAMERA_FAR_PLANE);
        }

        #endregion
    }
}