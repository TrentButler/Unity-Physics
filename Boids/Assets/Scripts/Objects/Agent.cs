using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public abstract class Agent : ScriptableObject
    {
        [SerializeField]
        protected float mass;

        [SerializeField]
        protected float max_speed;

        [SerializeField]
        protected Vector3 velocity;

        [SerializeField]
        protected Vector3 acceleration;

        [SerializeField]
        protected Vector3 force;

        [SerializeField]
        protected Vector3 position;
        

        public abstract void Initalize(Transform Owner, float Mass, float maxSpeed);
        public abstract bool Add_Force(float size, Vector3 direction); //WHEN YOU ADD FORCE, 
        public abstract Vector3 Update_Agent(float deltaTime); //UPDATE THE AGENT
    }
}