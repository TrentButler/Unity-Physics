using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Trent
{
    public class ParticleBehaviour : MonoBehaviour
    {
        public Particle particle;
        public bool isKinematic = false;
        public aaBB _collider;
        
        void FixedUpdate()
        {
            if(isKinematic == false)
            {
                transform.position = particle.Update(Time.fixedDeltaTime);                
            }
        }

        private void LateUpdate()
        {
            transform.position = particle.Position;
            _collider._Update(particle.Position);
        }
    }
}