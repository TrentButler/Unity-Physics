using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class SpringDamperBehaviour : MonoBehaviour
    {
        private SpringDamper spring;

        public float springConstant = 1.0f;
        public float dampingFactor = 1.0f;
        public float restLength = 1.0f;

        public GameObject model1;
        public GameObject model2;

        void Start()
        {
            Particle particle1 = new Particle(new Vector3(0,0,0), new Vector3(-0.1f, 0, 0), 1);
            Particle particle2 = new Particle(new Vector3(10, 0, 0), new Vector3(0.1f, 0, 0), 1);

            var go1 = new GameObject();
            go1.AddComponent<ParticleBehaviour>();            
            go1.GetComponent<ParticleBehaviour>().particle = particle1;

            var go2 = new GameObject();
            go2.AddComponent<ParticleBehaviour>();
            go2.GetComponent<ParticleBehaviour>().particle = particle2;

            spring = new SpringDamper(particle1, particle2, springConstant, dampingFactor, restLength);
        }
        
        void FixedUpdate()
        {
            spring.Ks = springConstant;
            spring.Kd = dampingFactor;
            spring.Lo = restLength;
            
            spring.CalculateForces();
        }

        private void LateUpdate()
        {
            model1.transform.position = spring.One.Position;
            model2.transform.position = spring.Two.Position;
        }
    }
}
