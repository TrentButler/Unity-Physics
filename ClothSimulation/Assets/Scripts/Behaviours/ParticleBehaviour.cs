using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class ParticleBehaviour : MonoBehaviour
    {
        public Particle particle;
        
        void FixedUpdate()
        {
            transform.position = particle.Update(Time.fixedDeltaTime);
        }
    }
}