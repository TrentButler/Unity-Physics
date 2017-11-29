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

        private float Ks; //SPRING CONSTANT
        private float Lo; //RESTING LENGTH

        public SpringDamper()
        {
            one = new Particle();
            two = new Particle();
            Ks = 0;
            Lo = 0;
        }
        public SpringDamper(Particle p1, Particle p2, float sC, float rL)
        {
            one = p1;
            two = p2;
            Ks = sC;
            Lo = rL;
        }
        

        //NEEDS WORK
        public void Update(float deltaTime)
        {
            ////CONVERT ALL THE 3D DISTANCES AND VELOCITIES INTO 1D
            //var convertedDist = (two.Position - one.Position).magnitude;
            //var convertecVelo = (two.Velocity - one.Velocity).magnitude;
            ////COMPUTE THE SPRING FORCE IN 1D
            ////TURN THE 1D FORCE INTO A 3D FORCE            

            var dist = one.Position - two.Position;
            //var relativeHeading = (two.Velocity - one.Velocity).normalized;
            var heading = two.Velocity.normalized;

            //var offset = Lo - dist.magnitude;
            var offset = one.Position.magnitude - dist.magnitude;

            Vector3 force = heading * offset;

            //One.AddForce(-force);
            Two.AddForce(force);
            //One.Update(deltaTime);
            Two.Update(deltaTime);
        }
    }
}