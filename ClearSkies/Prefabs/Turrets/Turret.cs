using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;

namespace ClearSkies.Prefabs.Turrets
{
    abstract class Turret : Prefab
    {
        protected TurretHead head;
        protected float colliderSize = 1.0f;

        public Turret(Vector3 location, Vector3 rotation, Vector3 scale, TurretHead head, float colliderSize)
            : base(location, rotation, scale)
        {
            this.head = head;
            this.colliderSize = colliderSize;
            this.children.Add(head);
        }

        public TurretHead Head
        {
            get { return this.head; }
        }

        public float ColliderSize
        {
            get { return this.colliderSize; }
        }
    }
}
