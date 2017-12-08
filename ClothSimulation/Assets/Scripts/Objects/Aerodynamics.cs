using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class Aerodynamics
    {
        public float _Cd;
        public float _p;
        public Vector3 _windDirection;

        public Aerodynamics()
        {
            _Cd = 1.0f;
            _p = 1.0f;
            _windDirection = Vector3.up;
        }
        public Aerodynamics(float dragCo, float airDensity, Vector3 windDirection)
        {
            _Cd = dragCo;
            _p = airDensity;
            _windDirection = windDirection;
        }

        public void UpdateAerodynamics(float dragCo, float airDensity, Vector3 windDirection)
        {
            _Cd = dragCo;
            _p = airDensity;
            _windDirection = windDirection;
        }

        public void CalculateForces(List<Particle> aeroParticles)
        {
            if(aeroParticles.Count < 3)
            {
                return;
            }

            Vector3 p1 = aeroParticles[0].Position;
            Vector3 p2 = aeroParticles[1].Position;
            Vector3 p3 = aeroParticles[2].Position;

            Vector3 v1 = aeroParticles[0].Velocity;
            Vector3 v2 = aeroParticles[1].Velocity;
            Vector3 v3 = aeroParticles[2].Velocity;

            Vector3 vSurface = (v1 + v2 + v3) / 3.0f; //AVERAGE VELOCITY CALCULATION

            Vector3 v = vSurface - _windDirection;

            Vector3 n = Vector3.Cross((p2 - p1), (p3 - p1)) / (Vector3.Cross((p2 - p1), (p3 - p1))).magnitude; //NORMALIZED SURFACE NORMAL
            Vector3 nStar = Vector3.Cross((p2 - p1), (p3 - p1)); //NON NORMALIZED SURFACE NORMAL

            float Ao = 0.5f * (Vector3.Cross((p2 - p1), (p3 - p1))).magnitude;
            float A = Ao * (Vector3.Dot(v, n) / v.magnitude); //AREA OF TRIANGE CALCULATION

            var vMagSquared = Mathf.Pow(v.magnitude, 2);

            Vector3 force = -0.5f * _p * vMagSquared * _Cd * A * n;
            force /= 3; //DIVIDE THE FORCE BY 3

            aeroParticles.ForEach(x =>
            {
                x.AddForce(force);
            }); //APPLY THE CALCULATED FOR TO EACH PARTICLE IN THE TRIANGLE
        }
    }
}