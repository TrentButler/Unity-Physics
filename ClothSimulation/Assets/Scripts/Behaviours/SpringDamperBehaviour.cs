using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class SpringDamperBehaviour : MonoBehaviour
    {
        private SpringDamper spring;
        
        public GameObject visualRep1;
        public GameObject visualRep2;

        void Start()
        {
            Particle particle1 = new Particle(visualRep1.transform.position, new Vector3(-0.1f, 0, 0), 1);
            Particle particle2 = new Particle(visualRep2.transform.position, new Vector3(0.1f,0,0), 1);

            spring = new SpringDamper(particle1, particle2, 1, 1);
        }
        
        void FixedUpdate()
        {
            spring.Update(Time.deltaTime);
        }

        private void LateUpdate()
        {
            visualRep1.transform.position = spring.One.Position;
            visualRep2.transform.position = spring.Two.Position;
        }
    }
}
