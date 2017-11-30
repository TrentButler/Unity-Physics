using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class ClothBehaviour : MonoBehaviour
    {

        public int Rows = 2;
        public int Columns = 2;
        public float offset = 1.0f;

        //public bool useGravity = false;
        public GameObject model;
        public List<GameObject> GOs;
        public List<Particle> particles;
        public List<SpringDamper> dampers;

        // Use this for initialization
        void Start()
        {
            GOs = new List<GameObject>();
            particles = new List<Particle>();
            dampers = new List<SpringDamper>();

            #region Grid
            for(int r = 0; r < Rows; r++)
            {
                for(int c = 0; c < Columns; c++)
                {
                    //CREATE INSTANCE OF
                    //-PARTICLE
                    //-MODEL

                    Particle p = new Particle(new Vector3(r * offset, c * offset, 0), new Vector3(0, 0.1f, 0), 1.0f); //CREATE INSTANCE OF PARTICLE
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

            //NEEDS WORK
            //ADD SPRING DAMPERS
            //for(int i = 0; i < particles.Count; i++)
            //{
            //    #region Horizontal
            //    var p1 = particles[i];
            //    var p2 = particles[i + 1];
            //    #endregion
            //}
        }
        
        void Update()
        {

        }
    }
}