using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    //IMPLEMENT APPLICATION UI
    public class ClothBehaviour : MonoBehaviour
    {
        public int Rows = 2;
        public int Columns = 2;
        public float Mass = 1.0f;
        public float offset = 1.0f;
        public Vector3 colScale;
        public bool showSpringDampers = false;

        public float springConstant = 1.0f;
        public float dampingFactor = 1.0f;
        public float tearCoefficient;

        public bool useGravity = false;
        public bool isActive = true;
        public bool windActive = false;

        public float windDragCoefficient;
        public float airDensity;
        public Vector3 WindDirection;

        public GameObject model;
        public List<GameObject> GOs;
        public List<Particle> particles;
        public List<SpringDamper> dampers;

        private Aerodynamics aero;

        private Vector3 gravity()
        {
            return new Vector3(0, -9.81f, 0);
        }

        void Start()
        {
            aero = new Aerodynamics(windDragCoefficient, airDensity, WindDirection);
            GOs = new List<GameObject>();
            particles = new List<Particle>();
            dampers = new List<SpringDamper>();

            #region Generate Grid
            int colCount = 0;
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

                    var col = ScriptableObject.CreateInstance<aaBB>() as aaBB;
                    col._Init(colCount, go.transform.position, colScale);
                    go.GetComponent<ParticleBehaviour>()._collider = col;

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

                var springDamper = new SpringDamper(p1, p2, springConstant, dampingFactor, tearCoefficient);
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

                var springDamper = new SpringDamper(p1, p2, springConstant, dampingFactor, tearCoefficient);
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

                var springDamper = new SpringDamper(p1, p2, springConstant, dampingFactor, tearCoefficient);
                dampers.Add(springDamper);
                xIncrementor++;
            }

            //DIAGONALLY UP LEFT
            for (int i = 0; i < Rows * Columns; i++)
            {
                if (i == 0)
                {
                    continue; //SKIP THIS PARTICLE
                }

                if (i % Columns == 0)
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

                var springDamper = new SpringDamper(p1, p2, springConstant, dampingFactor, tearCoefficient);
                dampers.Add(springDamper);
            }
            #endregion
        }

        void FixedUpdate()
        {
            for(int i = 0; i < dampers.Count; i++)
            {
                if (dampers[i] == null)
                {
                    continue;
                }

                dampers[i].TestDamperTear();
                if (dampers[i].isBroken)
                {
                    //DELETE THIS DAMPER
                    dampers.Remove(dampers[i]);
                    dampers[i] = null;
                }
                
                if (isActive == true)
                {
                    if(dampers[i] == null)
                    {
                        continue;
                    }

                    dampers[i].Ks = springConstant;
                    dampers[i].Kd = dampingFactor;

                    if (useGravity == true)
                    {
                        dampers[i].One.AddForce(gravity());
                        dampers[i].Two.AddForce(gravity());
                    }

                    dampers[i].CalculateForces();
                }
            }
            
            if (isActive)
            {
                if(windActive)
                {
                    aero.UpdateAerodynamics(windDragCoefficient, airDensity, WindDirection); //UPDATE THE AERODYNAMICS OBJECT

                    //FORWARD 
                    int xIncrementor = 1;
                    for (int i = 0; i < particles.Count; i++)
                    {
                        if (i + Columns > (Rows * Columns) - 1)
                        {
                            continue; //TOP OF GRID CHECK
                        }

                        if (xIncrementor == Rows)
                        {
                            xIncrementor = 1; //RESET THE INCREMENTOR
                            continue;
                        }

                        var one = particles[i];
                        var two = particles[i + 1];
                        var three = particles[i + Columns];

                        List<Particle> tri = new List<Particle> { one, two, three };
                        aero.CalculateForces(tri);

                        xIncrementor++;
                    }

                    //REVERSE
                    xIncrementor = 1;
                    int count = 1;
                    for (int i = 0; i < particles.Count; i++)
                    {
                        if(count <= Rows)
                        {
                            count++;
                            continue;
                        }

                        if (xIncrementor == Rows)
                        {
                            xIncrementor = 1; //RESET THE INCREMENTOR
                            continue;
                        }

                        var one = particles[i];
                        var two = particles[i + 1];
                        var three = particles[i - Columns];

                        List<Particle> tri = new List<Particle> { one, two, three };
                        aero.CalculateForces(tri);

                        xIncrementor++;
                    }
                }
            }

            else
            {
                particles.ForEach(x =>
                {
                    x.ZeroForces();
                    x.ZeroVelocity();
                });
            }
        }

        private void LateUpdate()
        {
            if (showSpringDampers)
            {
                dampers.ForEach(x =>
                {
                    if(x != null)
                    {
                        Debug.DrawLine(x.One.Position, x.Two.Position, Color.green);
                    }
                });
            }
        }
    }
}