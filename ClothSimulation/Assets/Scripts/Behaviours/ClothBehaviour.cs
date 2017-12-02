using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class ClothBehaviour : MonoBehaviour
    {
        public int Rows = 2;
        public int Columns = 2;
        public float Mass = 1.0f;
        public float offset = 1.0f;
        public bool useGravity = false;

        public float springConstant = 1.0f;
        public float dampingFactor = 1.0f;
        public float restLength = 1.0f;

        public GameObject model;
        public List<GameObject> GOs;
        public List<Particle> particles;
        public List<SpringDamper> dampers;

        private Vector3 gravity()
        {
            return new Vector3(0, -9.81f, 0);
        }

        void Start()
        {
            GOs = new List<GameObject>();
            particles = new List<Particle>();
            dampers = new List<SpringDamper>();

            #region Generate Grid
            for (int c = 0; c < Columns; c++)
            {
                for (int r = 0; r < Rows; r++)
                {
                    //CREATE INSTANCE OF
                    //-PARTICLE
                    //-MODEL

                    Particle p = new Particle(new Vector3(r * offset, c * offset, 0), new Vector3(0, 0.1f, 0), Mass); //CREATE INSTANCE OF PARTICLE
                    var go = new GameObject();
                    go.AddComponent<ParticleBehaviour>();
                    go.GetComponent<ParticleBehaviour>().particle = p;
                    
                    var m = Instantiate(model, go.transform.position, go.transform.rotation);
                    m.transform.SetParent(go.transform);

                    particles.Add(p);
                    GOs.Add(go);
                }
            }
            #endregion

            #region Apply SpringDampers
            //APPLY A SPRING DAMPER BETWEEN TWO PARTICLES
            //HORIZONTALLY
            int xIncrementor = 1;
            for (int i = 0; i < Rows * Columns; i++)
            {
                int second = i + 1;

                if (xIncrementor == Rows)
                {
                    xIncrementor = 1; //RESET THE INCREMENTOR
                    continue;
                }

                var p1 = particles[i];
                var p2 = particles[second];

                var springDamper = new SpringDamper(p1, p2, springConstant, dampingFactor, restLength);
                dampers.Add(springDamper);
                xIncrementor++;
            }

            //VERTICALLY
            for (int i = 0; i < Rows * Columns; i++)
            {
                int second = i + Columns;

                if (i + Columns > (Rows * Columns) - 1)
                {
                   //GOs[i].GetComponent<ParticleBehaviour>().isKinematic = true;
                    continue; //TOP OF GRID CHECK
                }

                var p1 = particles[i];
                var p2 = particles[second];

                var springDamper = new SpringDamper(p1, p2, springConstant, dampingFactor, restLength);
                dampers.Add(springDamper);
            }

            //DIAGONALLY UP RIGHT
            xIncrementor = 1;
            for (int i = 0; i < Rows * Columns; i++)
            {
                int second = (i + 1) + Columns; // UP RIGHT

                if (i + Columns > (Rows * Columns) - 1)
                {
                    continue; //TOP OF GRID CHECK
                }

                if (xIncrementor == Rows)
                {
                    //CHECK IF AT END OF ROW
                    xIncrementor = 1; //RESET THE INCREMENTOR
                    continue;
                }

                var p1 = particles[i];
                var p2 = particles[second];

                var springDamper = new SpringDamper(p1, p2, springConstant, dampingFactor, restLength);
                dampers.Add(springDamper);
                xIncrementor++;
            }

            //DIAGONALLY UP LEFT
            for (int i = 0; i < Rows * Columns; i++)
            {
                if(i == 0)
                {
                    continue; //SKIP THIS PARTICLE
                }

                if(i % Columns == 0)
                {
                    continue; //SKIP THIS PARTICLE
                }

                int second = (i - 1) + Columns; // UP LEFT

                if (i + Columns > (Rows * Columns) - 1)
                {
                    continue; //TOP OF GRID CHECK
                }
                
                var p1 = particles[i];
                var p2 = particles[second];

                var springDamper = new SpringDamper(p1, p2, springConstant, dampingFactor, restLength);
                dampers.Add(springDamper);
            }
            #endregion
        }

        void FixedUpdate()
        {
            dampers.ForEach(x =>
            {
                Debug.DrawLine(x.One.Position, x.Two.Position, Color.green);

                x.Ks = springConstant;
                x.Kd = dampingFactor;
                x.Lo = restLength;

                if(useGravity == true)
                {
                    x.One.AddForce(gravity());
                    x.Two.AddForce(gravity());
                }

                x.CalculateForces();
            });
        }
    }
}