using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearSkies.Prefabs.Enemies
{
    /// <summary>
    /// Types of Enemies available for use in game.
    /// </summary>
    enum EnemyType
    {
        BasicTank,      // A Tank Enemy will wander around on the ground trying to move from point A to point B.
        BasicPlane      // A Plane Enemy will fly in the air trying to avoid Bullets. TODO: Shoot at Player?
    }
}
