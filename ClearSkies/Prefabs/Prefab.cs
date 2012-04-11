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
    /// be run. This is useful for building any in game object.
    /// </summary>
    abstract class Prefab
    {
        #region Fields

        protected bool alive;
        protected Vector3 location;
        protected Vector3 rotation;
        protected List<Model> models;
        protected List<Prefab> children;
        protected List<Script> scripts = new List<Script>();

        #endregion

        #region Initializer Methods

        /// <summary>
        /// Creates an Prefab at a given location facing a given rotation. This
        /// object is used as the base for all in game objects.
        /// </summary>
        /// <param name="location">Location of the Prefab</param>
        /// <param name="rotation">Rotation the Prefab is facing</param>
        protected Prefab(Vector3 location, Vector3 rotation)
        {
            this.alive = true;
            this.location = location;
            this.rotation = rotation;

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
        public virtual Vector3 Rotation
        {
            get { return rotation; }
            set 
            {
                foreach (Prefab child in children)
                {
                    child.Rotation = child.Rotation - this.Rotation + value;

                    Vector3 deltaRotation = value - this.Rotation;

                    Vector3 temp = child.Location - this.Location;
                    Vector4 transformed = Vector3.Transform(temp, Matrix.RotationYawPitchRoll(deltaRotation.X, deltaRotation.Y, deltaRotation.Z));
                    temp = new Vector3(transformed.X, transformed.Y, transformed.Z);
                    child.Location = this.Location + temp;
                }
                this.rotation = value;
            }
        }
        /// <summary>
        /// Determines if the Prefab is still alive in the scene. If not it
        /// should be removed by its Manager.
        /// </summary>
        public bool Alive
        {
            get { return alive; }
            set { this.alive = value; }
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
        /// Detects a collision with the given Prefab.
        /// </summary>
        /// <param name="collider">Prefab that has collided with this Prefab</param>
        public virtual void detectCollision(Prefab collider) { }

        /// <summary>
        /// Update simply runs every script associated with the current Prefab.
        /// </summary>
        /// <param name="deltaTime">Time since last update in seconds</param>
        public virtual void update(float deltaTime)
        {
            foreach (Script script in scripts)
            {
                script.run(deltaTime);
            }

            foreach (Prefab child in children)
            {
                child.update(deltaTime);
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
