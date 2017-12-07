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

        private void colresolution(aaBB other)
        {
            if(other.Id == -99) //CURSOR ID
            {
                //CHANGE COLOR
                Debug.Log("COLLISION WITH CURSOR");
                GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            }
        }
        
        private void Start()
        {
            _collider.resolution += colresolution;
            //Debug.Log("START METHOD @ PARTICLE BEHAVIOUR");
        }

        void FixedUpdate()
        {
            if(_collider.resolution == null)
            {
                _collider.resolution += colresolution;
            }

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