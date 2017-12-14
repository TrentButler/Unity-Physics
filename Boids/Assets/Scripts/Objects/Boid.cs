using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class Boid : Agent
    {
        #region ParentClassMemberVariables
        public float Mass { get { return mass; } }
        public float MaxSpeed { get { return max_speed; } }
        public Vector3 Velocity { get { return velocity; } }
        public Vector3 Acceleration { get { return acceleration; } }
        public Vector3 Force { get { return force; } }
        public Vector3 Position { get { return position; } }
        private float perchTimer;
        public float PerchTimer
        {
            get { return perchTimer; }
            set { perchTimer = value; _originalPerchTimer = value; }
        }
        public bool Perching { get { return perching; } }
        private float _originalPerchTimer;

        private bool perching;
        #endregion
       
        public void SetPerching(bool isPerching)
        {
            perching = isPerching;
        }
        public void SetPerching(bool isPerching, float perchLevel)
        {
            perching = isPerching;
            position.y = perchLevel;
        }
        public void SetPerching(bool isPerching, Vector3 target)
        {
            perching = isPerching;
            position = target;
        }

        public void ResetPerchTimer()
        {
            PerchTimer = _originalPerchTimer;
        }

        public override void Initalize(Transform Owner, float Mass, float maxSpeed)
        {
            //NEEDS WORK
            //INITILIZE THE AGENT

            mass = Mass;
            max_speed = maxSpeed;
            velocity = new Vector3(0.0f, 0.0f, 0.5f); //NEEDS WORK
            acceleration = Vector3.zero;
            force = Vector3.zero;
            position = Owner.position;
        }

        public override bool Add_Force(float size, Vector3 direction)
        {
            var f = direction * size; //FORCE CALCULATION

            if (f.magnitude == 0) //ZERO MAGNITUDE CHECK
            {
                return false;
            }

            force += f;
            return true;
        }

        public override Vector3 Update_Agent(float deltaTime)
        {
            acceleration = force * (1 / mass);

            velocity += acceleration * deltaTime; //VELOCITY CALCULATION

            velocity = Vector3.ClampMagnitude(velocity, max_speed); //CLAMP THE VELOCITY BY THE BOID'S MAXIMUM SPEED

            position += velocity * deltaTime; //POSITION CALCULATION

            return position; //RETURN THE CURRENT POSITION
        }
    }
}