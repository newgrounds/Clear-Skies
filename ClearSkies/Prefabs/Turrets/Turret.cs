using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using ClearSkies.Scripts;
using DI = Microsoft.DirectX.DirectInput;

namespace ClearSkies.Prefabs.Turrets
{
    class Turret : Prefab
    {
        private List<TurretBarrel> barrels;

        public Turret(Model barrelModel, Model baseModel, Vector3 location, Vector3 rotation, DI.Device keyboard)
        {
            this.models.Add(baseModel);
            this.location = location;
            this.rotation = rotation;
            
            barrels = new List<TurretBarrel>();
            
            addChild(new TurretBarrel(barrelModel,
                new Vector3(location.X + 0.25f, location.Y + 0.5f, location.Z + 0.5f),
                new Vector3(rotation.X, rotation.Y + 45f, rotation.Z)
                ));

            addChild(new TurretBarrel(barrelModel,
                new Vector3(location.X - 0.25f, location.Y + 0.5f, location.Z + 0.5f),
                new Vector3(rotation.X, rotation.Y + 45f, rotation.Z)
                ));
            
            this.scripts.Add(new TurretRotationScript(this, keyboard, 10f));
        }

        public float BarrelRotation
        {
            get 
            {
                float barrelRotation = 0.0f;
                if (children.Count > 0)
                {
                    barrelRotation = children[0].Rotation.Y;
                }
                return barrelRotation;
            }
            set 
            { 
                foreach (Prefab child in children)
                {
                    if (child is TurretBarrel)
                    {
                        Vector3 childRotation = child.Rotation;
                        childRotation.Y = value;
                        child.Rotation = childRotation;
                    }
                }
            }
        }

        //public override void draw(Device device)
        //{
        //    base.draw(device);

        //    this.baseModel.draw();

        //    device.Transform.World = Matrix.Multiply(
        //        Matrix.RotationYawPitchRoll(rotation.X, rotation.Y + barrelRotation, rotation.Z),
        //        Matrix.Translation(location.X + 0.25f, location.Y + 0.5f, location.Z + 0.5f));

        //    this.barrelModel1.draw();

        //    device.Transform.World = Matrix.Multiply(
        //        Matrix.RotationYawPitchRoll(rotation.X, rotation.Y + barrelRotation, rotation.Z),
        //        Matrix.Translation(location.X - 0.25f, location.Y + 0.5f, location.Z + 0.5f));

        //    this.barrelModel1.draw();
        //}
    }
}
