using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class SpringDamper
    {
        private Particle one, two;

        public Particle One { get { return one; } }
        public Particle Two { get { return two; } }

        public float Ks; //SPRING CONSTANT
        public float Kd; //DAMPING FACTOR
        public float Lo; //RESTING LENGTH

        public SpringDamper()
        {
            one = new Particle();
            two = new Particle();
            Ks = 0;
            Kd = 0;
            Lo = 0;
        }
        public SpringDamper(Particle p1, Particle p2, float sC, float dF, float rL)
        {
            one = p1;
            two = p2;
            Ks = sC;
            Kd = dF;
            Lo = rL;
        }        
        
        public void CalculateForces()
        {
            //CALCULATE THE UNIT LENGTH BETWEEN TWO VECTORS
            Vector3 length = two.Position - one.Position;
            float L = length.magnitude;
            Vector3 E = length / L;

            //CALCULATE THE 1D VELOCITIES
            Vector3 vOne = one.Velocity;
            Vector3 vTwo = two.Velocity;
            float v1 = Vector3.Dot(E, vOne);
            float v2 = Vector3.Dot(E, vTwo);

            //CONVERT FROM 1D TO 3D
            float sMinusd = -Ks * (Lo - L) - Kd * (v1 - v2);
            Vector3 force = sMinusd * E;

            one.AddForce(force);
            two.AddForce(-force);
        }
    }
}