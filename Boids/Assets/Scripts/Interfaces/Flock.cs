using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    interface IFlock
    {
        Vector3 seperation(Boid b, float distance);
        Vector3 alignment(Boid b);
        Vector3 cohesion(Boid b);
    }
}
