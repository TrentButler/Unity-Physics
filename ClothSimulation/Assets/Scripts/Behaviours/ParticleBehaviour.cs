using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Trent
{
    public class ParticleBehaviour : MonoBehaviour
    {
        public Particle particle;
        public aaBB _collider;
        public bool isColliding;

        //NEEDS WORK
        //CHANGE PARTICLE MODEL COLOR ONHOVER
        private void colresolution(aaBB other)
        {
            if (other.Id == -99) //CURSOR ID
            {
                //CHANGE COLOR
                //var rend = GetComponentInChildren<Renderer>().material.color = Color.red;
            }

            //NEEDS WORK
            if (other.Id == -98) //FLOOR COLLIDER ID
            {
                //isColliding = true;

                //var oldPos = particle.Position;
                //oldPos.y = other.Position.y;

                //particle.Update(oldPos);

                //var rend = GetComponentInChildren<Renderer>().material.color = Color.green;
            }
        }

        private void Start()
        {
            isColliding = false;
            _collider.resolution += colresolution;
            //Debug.Log("START METHOD @ PARTICLE BEHAVIOUR");
        }

        void FixedUpdate()
        {
            if (_collider.resolution == null)
            {
                _collider.resolution += colresolution;
            }
            
            if (particle.isKinematic == false)
            {
                transform.position = particle.Update(Time.fixedDeltaTime);
                GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            }

            if (particle.isKinematic == true)
            {
                particle.ZeroForces();
                particle.ZeroVelocity();

                //CHANGE THE COLOR TO RED
                GetComponentInChildren<MeshRenderer>().material.color = Color.black;
            }
        }

        private void LateUpdate()
        {
            transform.position = particle.Position;
            _collider._Update(particle.Position);
        }
    }
}