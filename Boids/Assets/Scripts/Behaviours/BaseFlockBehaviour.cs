using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class BaseFlockBehaviour : MonoBehaviour
    {
        public float FlockMovementSpeed = 0.5f;
        public float AlignmentForce = 0.0f;
        public float CohesionForce = 0.0f;
        public float DispersionForce = 0.0f;
        public float AgentOffset = 1.0f;
        public float PerchingLevel = 1.0f;

        //public List<Boid> currentPerching;

        public Vector3 minBound;
        public Vector3 maxBound;

        public List<Boid> neighbors;

        public Vector3 alignment(Boid b)
        {
            Vector3 force = Vector3.zero;

            foreach (var boid in neighbors)
            {
                if (b != boid)
                {
                    force += boid.Velocity;
                }
            }

            force /= neighbors.Count - 1;

            return (force - b.Velocity);
        }

        public Vector3 cohesion(Boid b)
        {
            Vector3 force = Vector3.zero;

            foreach (var boid in neighbors)
            {
                if (b != boid)
                {
                    force += boid.Position;
                }
            }

            force /= neighbors.Count - 1.0f;

            return (force - b.Position);
        }

        public Vector3 seperation(Boid b, float distance)
        {
            Vector3 force = Vector3.zero;

            if (distance < 0.0f)
            {
                distance = 0.0f;
            }

            foreach (var boid in neighbors)
            {
                if (b != boid)
                {
                    var distanceMag = (boid.Position - b.Position).magnitude;
                    if (distanceMag < distance)
                    {
                        force -= (boid.Position - b.Position);
                    }
                }
            }

            return force;
        }

        //NEEDS WORK
        private void Bounds()
        {
            //NEEDS WORK
            neighbors.ForEach(boid =>
            {
                var x = boid.Position.x;
                var y = boid.Position.y;
                var z = boid.Position.z;

                //PERCHING CHECK
                if (y <= PerchingLevel)
                {
                    boid.SetPerching(true, PerchingLevel);
                }

                if (x > maxBound.x)
                {
                    var xdist = maxBound.x - x;
                    boid.Add_Force(xdist, -boid.Velocity.normalized);
                    //Debug.Log(boid.name + "OUT OF BOUNDS");

                }

                if (y > maxBound.y)
                {
                    var ydist = maxBound.y - y;
                    boid.Add_Force(ydist, -boid.Velocity.normalized);
                    //Debug.Log(boid.name + "OUT OF BOUNDS");
                }

                if (z > maxBound.z)
                {
                    var zdist = maxBound.z - y;
                    boid.Add_Force(zdist, -boid.Velocity.normalized);
                    //Debug.Log(boid.name + "OUT OF BOUNDS");
                }

                if (x < minBound.x)
                {
                    var Xdist = minBound.x - x;
                    boid.Add_Force(Xdist, -boid.Velocity.normalized);
                    //Debug.Log(boid.name + "OUT OF BOUNDS");
                }

                if (y < minBound.y)
                {
                    var Ydist = minBound.y - y;
                    boid.Add_Force(Ydist, -boid.Velocity.normalized);
                    //Debug.Log(boid.name + "OUT OF BOUNDS");
                }

                if (z < minBound.z)
                {
                    var Zdist = minBound.z - z;
                    boid.Add_Force(Zdist, -boid.Velocity.normalized);
                    //Debug.Log(boid.name + "OUT OF BOUNDS");
                }

                boid.Update_Agent(Time.deltaTime);
            });
        }

        void Start()
        {
            neighbors = GameObject.FindObjectsOfType<Boid>().ToList();
        }

        private void Update()
        {
            neighbors = GameObject.FindObjectsOfType<Boid>().ToList();
        }

        //NEEDS WORK
        void FixedUpdate()
        {
            if (neighbors.Count > 1)
            {
                Bounds(); //BOUNDING VOLUME

                foreach (var boid in neighbors)
                {
                    //NEEDS WORK
                    if (boid.Perching == true)
                    {
                        if (boid.PerchTimer > 0.0f)
                        {
                            boid.PerchTimer -= Time.deltaTime;
                        }

                        if (boid.PerchTimer <= 0.0f)
                        {
                            boid.SetPerching(false);
                            boid.ResetPerchTimer();
                        }
                    }

                    var vAlignment = alignment(boid) * AlignmentForce;
                    var vCohesion = cohesion(boid) * CohesionForce;
                    var vSeperation = seperation(boid, AgentOffset) * DispersionForce;

                    var force = (vAlignment + vCohesion + vSeperation);
                    //var force = new Vector3(0, 0, 0.5f);

                    boid.Add_Force(FlockMovementSpeed, force);
                    boid.Update_Agent(Time.deltaTime);
                }
            }
        }

        private void LateUpdate()
        {
            var agentGameObjects = GameObject.FindObjectsOfType<BaseAgentBehaviour>().ToList(); //GET ALL GAMEOBJECTS WITH A A 'BOIDBEHAVIOUR'
            var agents = GameObject.FindObjectsOfType<Boid>().ToList();

            for (int i = 0; i < agentGameObjects.Count; i++)
            {
                agentGameObjects[i].transform.position = agents[i].Position;
                //CHANGE THE MESH'S HEADING
                agentGameObjects[i].transform.forward = agents[i].Velocity.normalized;
            }
        }
    }
}