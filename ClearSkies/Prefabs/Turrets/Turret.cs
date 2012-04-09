using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace ClearSkies.Prefabs.Turrets
{
    class Turret : Prefab
    {
        private Model barrelModel1;
        private Model barrelModel2;
        private Model baseModel;
        private float barrelRotation;

        public Turret(Model barrelModel, Model baseModel, Vector3 location, Vector3 rotation)
        {
            this.barrelModel1 = barrelModel;
            this.barrelModel2 = barrelModel;
            this.baseModel = baseModel;
            
            this.location = location;
            this.rotation = rotation;
            this.barrelRotation = 45.0f;
        }

        public float BarrelRotation
        {
            get { return this.barrelRotation; }
            set { this.barrelRotation = value; }
        }

        public override void draw(Device device)
        {
            base.draw(device);

            this.baseModel.draw();

            device.Transform.World = Matrix.Multiply(
                Matrix.RotationYawPitchRoll(rotation.X, rotation.Y + barrelRotation, rotation.Z),
                Matrix.Translation(location.X + 0.25f, location.Y + 0.5f, location.Z + 0.5f));

            this.barrelModel1.draw();

            device.Transform.World = Matrix.Multiply(
                Matrix.RotationYawPitchRoll(rotation.X, rotation.Y + barrelRotation, rotation.Z),
                Matrix.Translation(location.X - 0.25f, location.Y + 0.5f, location.Z + 0.5f));

            this.barrelModel1.draw();
        }
    }
}
