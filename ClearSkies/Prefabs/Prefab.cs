using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Scripts;
using Microsoft.DirectX.Direct3D;

namespace ClearSkies.Prefabs
{
    /// <summary>
    /// A prefab object is an object that is a mixture of models and scripts to
    /// be run. This is useful for building any in game object. Some limitations
    /// do apply such as to the Third Person Camera.
    /// </summary>
    abstract class Prefab
    {
        #region Fields

        protected Vector3 location;
        protected Vector3 rotation;
        protected List<Model> models;
        protected List<Prefab> children;
        protected List<Script> scripts = new List<Script>();

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates an empty Prefab, this way every component
        /// can be tweaked as needed.
        /// </summary>
        protected Prefab()
        {
            this.location = Vector3.Empty;
            this.rotation = Vector3.Empty;
            this.children = new List<Prefab>();
            this.scripts = new List<Script>();
            this.models = new List<Model>();
        }

        #endregion

        #region Getters and Setters

        /// <summary>
        /// Gets or Sets the Prefabs current location.
        /// </summary>
        public Vector3 Location
        {
            get { return location; }
            set 
            {
                foreach (Prefab child in children)
                {
                    child.Location = value + child.Location - this.Location;
                }
                this.location = value; 
            }
        }
        /// <summary>
        /// Gets or Sets the Prefabs current rotation. TODO: TEST!!!
        /// </summary>
        public Vector3 Rotation
        {
            get { return rotation; }
            set 
            {
                foreach (Prefab child in children)
                {
                    child.Rotation = value + this.Rotation - child.Rotation;

                    Matrix rotationMatrix = Matrix.RotationYawPitchRoll(value.X, value.Y, value.Z);
                    Vector4 transformedReference = Vector3.Transform(child.Location, rotationMatrix);
                    child.Location = new Vector3(transformedReference.X, transformedReference.Y, transformedReference.Z) + this.Location;
                }
                this.rotation = value; 
            }
        }

        /// <summary>
        /// Adds a new Child to the current Prefab.
        /// </summary>
        /// <param name="child"></param>
        public void addChild(Prefab child)
        {
            this.children.Add(child);
        }
        /// <summary>
        /// Adds a new Script to the current Prefab.
        /// </summary>
        /// <param name="script"></param>
        public void addScript(Script script)
        {
            this.scripts.Add(script);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update simply runs every script associated with the current Prefab.
        /// </summary>
        /// <param name="deltaTime">Time since last update in seconds</param>
        public void update(float deltaTime)
        {
            foreach (Script script in scripts)
            {
                script.run(deltaTime);
            }
        }

        /// <summary>
        /// Draws the current Prefab and all of its children to the given device.
        /// There is a bug currently in this code as the children do not have
        /// coordinatesystems within their parent.
        /// </summary>
        /// <param name="device"></param>
        public virtual void draw(Device device)
        {
            //do transformations
            device.Transform.World = Matrix.Multiply(
                Matrix.RotationYawPitchRoll(rotation.X, rotation.Y, rotation.Z),
                Matrix.Translation(location.X, location.Y, location.Z));

            foreach (Model model in models)
            {
                model.draw();
            }

            foreach (Prefab child in children)
            {
                child.draw(device);
            }
        }

        #endregion
    }
}
