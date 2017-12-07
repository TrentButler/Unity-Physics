using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class DebugCubeBehaviour : MonoBehaviour
    {
        private aaBB col;
        public float size;
        public List<ParticleBehaviour> objectsInScene;
        private ColliderSystem colSystem;
        
        void Start()
        {
            colSystem = new ColliderSystem();
            objectsInScene = GameObject.FindObjectsOfType<ParticleBehaviour>().ToList();
            col = ScriptableObject.CreateInstance<aaBB>() as aaBB;
            col._Init(99, transform.position, size);
        }

        void FixedUpdate()
        {
            objectsInScene = GameObject.FindObjectsOfType<ParticleBehaviour>().ToList();
            col._Update(transform.position, size);

            objectsInScene.ForEach(x =>
            {
                if(colSystem.TestOverLap(col, x._collider) == true)
                {
                    x.isKinematic = true;
                }

                else
                {
                    x.isKinematic = false;
                }
            });
        }

        private void LateUpdate()
        {
            Vector3 modelScale = new Vector3(size * 2, size * 2, size * 2);

            transform.localScale = modelScale;
            
            var bl = new Vector3(col.Min.x, col.Min.y);
            var tr = new Vector3(col.Max.x, col.Max.y);
            var tl = new Vector3(col.Min.x, col.Max.y);
            var br = new Vector3(col.Max.x, col.Min.y);

            Debug.DrawLine(bl, br);
            Debug.DrawLine(bl, tl);
            Debug.DrawLine(tl, tr);
            Debug.DrawLine(br, tr);
        }
    }
}