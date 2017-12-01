using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class ParticleBehaviour : MonoBehaviour
    {
        public Particle particle;
        public bool isKinematic = false;
        
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
        }
    }
}