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

        public Turret(Vector3 location, Vector3 rotation, Vector3 scale, TurretHead head)
            : base(location, rotation, scale)
        {
            this.head = head;
            this.children.Add(head);
        }

        public TurretHead Head
        {
            get { return this.head; }
        }
    }
}
