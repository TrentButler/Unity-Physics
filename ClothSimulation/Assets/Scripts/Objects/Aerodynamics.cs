using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class Aerodynamics
    {
        public float _Cd;
        public float _p;
        public Vector3 windDirection;
        private List<Particle> aeroParticles;


        Aerodynamics()
        {
            _Cd = 1.0f;
            _p = 1.0f;
            aeroParticles = new List<Particle>();
        }
        Aerodynamics(List<Particle> p)
        {
            _Cd = 1.0f;
            _p = 1.0f;

            aeroParticles = p;
        }
        Aerodynamics(List<Particle> p, float dragCo, float airDensity)
        {
            _Cd = dragCo;
            _p = airDensity;

            aeroParticles = p;
        }

        public void CalculateForces()
        {
            var p1 = aeroParticles[0].Position;
            var p2 = aeroParticles[1].Position;
            var p3 = aeroParticles[2].Position;

            var Ao = 0.5f * (Vector3.Cross((p2 - p1), (p3 - p1))).magnitude;
            //var A = Ao()
        }
    }
}