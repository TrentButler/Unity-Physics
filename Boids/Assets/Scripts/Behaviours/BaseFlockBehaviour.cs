using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class BaseFlockBehaviour : MonoBehaviour
    {
        public float FlockMovementSpeed = 10.0f;

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

            return (force - b.Velocity) / 8;
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

            return (force - b.Position) / 100;
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

        // Use this for initialization
        void Start()
        {
            neighbors = GameObject.FindObjectsOfType<Boid>().ToList();
        }

        private void Update()
        {
            neighbors = GameObject.FindObjectsOfType<Boid>().ToList();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            neighbors.ForEach(boid =>
            {
                var vAlignment = alignment(boid);
                var vCohesion = cohesion(boid);
                var vSeperation = seperation(boid, 1);

                //var force = (vAlignment + vCohesion + vSeperation);
                var force = new Vector3(0, 0, 0.5f);

                boid.Add_Force(FlockMovementSpeed, force);
                boid.Update_Agent(Time.deltaTime);
            });
        }

        private void LateUpdate()
        {
            var agentGameObjects = GameObject.FindObjectsOfType<BaseAgentBehaviour>().ToList();
            var agents = GameObject.FindObjectsOfType<Boid>().ToList();
            agentGameObjects.ForEach(boid =>
            {
                int i = 0;
                if (agents.Count >= 3)
                {
                    boid.transform.position = agents[i].Position;
                }
                i++;
            });
        }
    }
}