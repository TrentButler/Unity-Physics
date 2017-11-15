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
        #endregion

        private List<Boid> neighbors;

        public void AddNeighbor(Boid b)
        {
            if(neighbors.Contains(b) == false)
            {
                neighbors.Add(b);
            }
        }

        public void RemoveNeighbor(Boid b)
        {
            if (neighbors.Contains(b) == true)
            {
                neighbors.Remove(b);
            }
        }

        public override void Initalize(Transform Owner, float Mass, float maxSpeed)
        {
            //NEEDS WORK
            //INITILIZE THE AGENT

            neighbors = new List<Boid>();

            mass = Mass;
            max_speed = maxSpeed;
            velocity = new Vector3(0.1f, 0.1f, 1.0f); //NEEDS WORK
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

            position += velocity * deltaTime; //POSITION CALCULATION

            return position; //RETURN THE CURRENT POSITION
        }
    }
}