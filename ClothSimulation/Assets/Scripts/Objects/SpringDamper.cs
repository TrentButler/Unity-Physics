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
        public float damperTearCoefficent;
        public bool isBroken;

        public SpringDamper()
        {
            one = new Particle();
            two = new Particle();
            Ks = 0;
            Kd = 0;
            Lo = 0;
            damperTearCoefficent = 0;
            isBroken = false;

        }
        public SpringDamper(Particle p1, Particle p2, float sC, float dF, float tC)
        {
            one = p1;
            two = p2;
            Ks = sC;
            Kd = dF;
            Lo = Vector3.Distance(p1.Position, p2.Position);
            damperTearCoefficent = tC;
            isBroken = false;
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

        public void TestDamperTear()
        {
            var dist = Vector3.Distance(two.Position, one.Position);
            if(dist > damperTearCoefficent)
            {
                //DELETE THIS SPRING DAMPER
                isBroken = true;
            }
        }
    }
}