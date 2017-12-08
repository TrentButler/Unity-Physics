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
                isColliding = true;

                //CHANGE COLOR
                var rend = GetComponentInChildren<Renderer>().material.color = Color.red;
                Debug.Log("COLLISION WITH CURSOR");
            }

            if (other.Id == -98) //FLOOR COLLIDER ID
            {
                isColliding = true;

                var oldPos = particle.Position;
                oldPos.y = other.Position.y;

                particle.Update(oldPos);

                var rend = GetComponentInChildren<Renderer>().material.color = Color.green;
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

            if (isColliding == false)
            {
                GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            }

            if (particle.isKinematic == false)
            {
                transform.position = particle.Update(Time.fixedDeltaTime);

                //CHANGE THE COLOR TO WHITE
                //GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            }

            if (particle.isKinematic == true)
            {
                particle.ZeroForces();
                particle.ZeroVelocity();

                //CHANGE THE COLOR TO RED
                //GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            }
        }

        private void LateUpdate()
        {
            transform.position = particle.Position;
            _collider._Update(particle.Position);
        }
    }
}