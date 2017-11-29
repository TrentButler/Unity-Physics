using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class ParticleBehaviour : MonoBehaviour
    {
        public Particle particle;
        
        void Start()
        {
            particle = new Particle(Vector3.zero, Vector3.zero, 1.0f);
        }
        
        void FixedUpdate()
        {
            particle.AddForce(Vector3.right);
            transform.position = particle.Update(Time.fixedDeltaTime);
        }
    }
}