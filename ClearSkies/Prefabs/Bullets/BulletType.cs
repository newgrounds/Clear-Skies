using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClearSkies.Prefabs.Bullets
{
    /// <summary>
    /// Types of Bullets available for use in game.
    /// </summary>
    enum BulletType
    {
        Basic,      // A basic bullet with no speical effects.
        Tracer,     // A basic bullet that glows for extra visiability and explodes on impact with enemy
        Tank        // A bullet for the tank with no special effects
    }
}
