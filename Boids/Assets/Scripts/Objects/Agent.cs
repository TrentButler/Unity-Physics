using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    abstract public class Agent : ScriptableObject
    {
        protected float mass;
        protected float max_speed;
        protected Vector3 velocity;
        protected Vector3 acceleration;
        protected Vector3 position;
        protected Vector3 force;

        public abstract void Initalize(Transform owner);
        public abstract bool Add_Force(float size, Vector3 direction); //WHEN YOU ADD FORCE, 
        public abstract Vector3 Update_Agent(float deltaTime); //UPDATE THE AGENT
    }
}