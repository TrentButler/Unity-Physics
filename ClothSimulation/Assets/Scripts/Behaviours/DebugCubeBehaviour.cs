using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class DebugCubeBehaviour : MonoBehaviour
    {
        private aaBB col;
        public Vector3 size;
        public List<ParticleBehaviour> objectsInScene; //THIS IS A NO NO. COLLECT AN LIST OF 'aaBB' INSTEAD
        private ColliderSystem colSystem;
        
        void Start()
        {
            colSystem = new ColliderSystem();
            objectsInScene = GameObject.FindObjectsOfType<ParticleBehaviour>().ToList();
            col = ScriptableObject.CreateInstance<aaBB>() as aaBB;
            col._Init(-98, transform.position, size);
        }

        void FixedUpdate()
        {
            objectsInScene = GameObject.FindObjectsOfType<ParticleBehaviour>().ToList();
            col._Update(transform.position, size);

            objectsInScene.ForEach(x =>
            {
                if(colSystem.TestOverLap(col, x._collider) == true)
                {
                    var vel = x.particle.Velocity;
                    x.particle.AddForce(-vel);
                    //x.isKinematic = true;
                }

                else
                {
                    x.isKinematic = false;
                }
            });
        }

        private void LateUpdate()
        {
            transform.localScale = col.Scale;
            
            //var bl = new Vector3(col.Min.x, col.Min.y);
            //var tr = new Vector3(col.Max.x, col.Max.y);
            //var tl = new Vector3(col.Min.x, col.Max.y);
            //var br = new Vector3(col.Max.x, col.Min.y);

            //Debug.DrawLine(bl, br);
            //Debug.DrawLine(bl, tl);
            //Debug.DrawLine(tl, tr);
            //Debug.DrawLine(br, tr);
        }
    }
}