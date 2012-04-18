using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using ClearSkies.Scripts;
using Microsoft.DirectX.Direct3D;
using ClearSkies.Content;

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
        protected Vector3 scale;
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
        /// <param name="scale">Scale of the Prefab</param>
        protected Prefab(Vector3 location, Vector3 rotation, Vector3 scale)
        {
            this.alive = true;
            this.location = location;
            this.rotation = rotation;
            this.scale = scale;

            this.children = new List<Prefab>();
            this.scripts = new List<Script>();
            this.models = new List<Model>();
        }

        #endregion

        #region Getters and Setters

        public List<Prefab> Children
        {
            get { return children; }
        }

        /// <summary>
        /// Gets or Sets the Prefabs current location.
        /// </summary>
        public virtual Vector3 Location
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
        /// Gets or Sets the Prefabs current rotation.
        /// </summary>
        public virtual Vector3 Rotation
        {
            get { return this.rotation; }
            set 
            {
                foreach (Prefab child in children)
                {
                    child.Rotation = child.Rotation - this.rotation + value;

                    Vector3 deltaRotation = value - this.rotation;

                    Vector3 temp = child.Location - this.location;
                    Vector4 transformed = Vector3.Transform(temp, Matrix.RotationYawPitchRoll(deltaRotation.X, deltaRotation.Y, deltaRotation.Z));
                    temp = new Vector3(transformed.X, transformed.Y, transformed.Z);
                    child.Location = this.location + temp;
                }
                this.rotation = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Prefabs current scaling values.
        /// </summary>
        public virtual Vector3 Scale
        {
            get { return this.scale; }
            set
            {
                this.scale = value;
                foreach (Prefab child in children)
                {
                    child.Scale = new Vector3(child.Scale.X * value.X, child.Scale.Y * value.Y, child.Scale.Z * value.Z);
                }
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

        #endregion

        #region Public Methods

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

        /// <summary>
        /// Detects a collision with the given Prefab.
        /// </summary>
        /// <param name="collider">
        /// Prefab that has collided with this Prefab
        /// </param>
        public virtual void detectCollision(Prefab collider) { }

        /// <summary>
        /// Update simply runs every script associated with the current Prefab.
        /// </summary>
        /// <param name="deltaTime">Time since last update in seconds</param>
        public virtual void update(float deltaTime)
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                scripts[i].run(deltaTime);
            }

            for (int i = 0; i < children.Count; i++)
            {
                if(children[i].Alive)
                    children[i].update(deltaTime);
            }
        }

        /// <summary>
        /// Draws the current Prefab and all of its children to the given device.
        /// There is a bug currently in this code as the children do not have
        /// coordinatesystems within their parent.
        /// </summary>
        /// <param name="device">Device to draw Prefab to</param>
        public virtual void draw(Device device)
        {
            //do transformations
            device.Transform.World =
                Matrix.Multiply(Matrix.Scaling(scale),
                    Matrix.Multiply(
                        Matrix.RotationYawPitchRoll(rotation.X, rotation.Y, rotation.Z),
                        Matrix.Translation(location.X, location.Y, location.Z)));

            foreach (Model model in models)
            {
                model.draw(device);
            }

            foreach (Prefab child in children)
            {
                child.draw(device);
            }
        }

        #endregion
    }
}
