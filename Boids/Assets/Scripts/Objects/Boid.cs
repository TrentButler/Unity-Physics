using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class Boid : Agent
    {
        public override void Initalize(Transform owner)
        {
            //NEEDS WORK
            //INITILIZE THE AGENT

            mass = owner.localScale.x;
            position = owner.position;
            velocity = new Vector3(0.1f, 0.1f, 0.1f);
            acceleration = Vector3.zero;
            max_speed = 1;
        }

        public override bool Add_Force(float size, Vector3 direction)
        {
            var f = direction * size; //FORCE CALCULATION

            if (f.magnitude == 0) //ZERO MAGNITUDE CHECK
            {
                return false;
            }

            force = f;
            return true;
        }

        public override Vector3 Update_Agent(float deltaTime)
        {
            acceleration = force * deltaTime; //NEEDS WORK, MASS????

            velocity = velocity + (acceleration * deltaTime); //VELOCITY CALCULATION

            position = position + (velocity * deltaTime); //POSITION CALCULATION

            return position; //RETURN THE CURRENT POSITION
        }
    }
}