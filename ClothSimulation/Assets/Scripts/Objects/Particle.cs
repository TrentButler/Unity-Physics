using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    [System.Serializable]
    public class Particle
    {
        
        private float mass;
        private Vector3 force;
        private Vector3 acceleration;
        [SerializeField]
        private Vector3 velocity;
        [SerializeField]
        private Vector3 position;

        #region Propteries
        public Vector3 Position { get { return position; } }
        public Vector3 Velocity { get { return velocity; } }
        #endregion

        public Particle()
        {
            force = Vector3.zero;
            acceleration = Vector3.zero;
            velocity = Vector3.zero;
            position = Vector3.zero;
            mass = 1.0f;
        }
        public Particle(Vector3 p, Vector3 v, float m)
        {
            force = Vector3.zero;
            acceleration = Vector3.zero;
            velocity = v;
            position = p;
            mass = m;
        }

        public void AddForce(Vector3 f)
        {
            force += f;
        }

        public Vector3 Update(float deltaTime)
        {
            acceleration = force / mass;
            velocity += acceleration * deltaTime;
            velocity = Vector3.ClampMagnitude(velocity, 10);
            position += velocity * deltaTime;
            return position;
        }
    }
}