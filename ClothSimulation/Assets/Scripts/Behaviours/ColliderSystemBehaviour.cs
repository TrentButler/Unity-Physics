using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class ColliderSystemBehaviour : MonoBehaviour
    {
        public float collisionCheckTimer;
        public List<aaBB> colliders;
        public bool showColliders = false;

        private ColliderSystem colSystem;
        public float timer;
        
        void Start()
        {
            timer = 0.0f;
            colSystem = new ColliderSystem(); //INTILIZE THE COLLIDERSYSTEM OBJECT

            //COLLECT ALL COLLIDERS IN THE SCENE
            colliders = GameObject.FindObjectsOfType<aaBB>().ToList();
        }

        void FixedUpdate()
        {
            colliders = GameObject.FindObjectsOfType<aaBB>().ToList(); //COLLECT ALL COLLIDERS IN THE SCENE

            colliders.ForEach(x =>
            {
                if (showColliders)
                {
                    var fbl = new Vector3(x.Min.x, x.Min.y, x.Max.z);
                    var fbr = new Vector3(x.Max.x, x.Min.y, x.Max.z);
                    var ftr = new Vector3(x.Max.x, x.Max.y, x.Max.z);
                    var ftl = new Vector3(x.Min.x, x.Max.y, x.Max.z);

                    var bbl = new Vector3(x.Min.x, x.Min.y, x.Min.z);
                    var bbr = new Vector3(x.Max.x, x.Min.y, x.Min.z);
                    var btr = new Vector3(x.Max.x, x.Max.y, x.Min.z);
                    var btl = new Vector3(x.Min.x, x.Max.y, x.Min.z);

                    var br = new Vector3(x.Max.x, x.Min.y);

                    Debug.DrawLine(fbl, fbr, Color.green);
                    Debug.DrawLine(ftl, ftr, Color.green);

                    Debug.DrawLine(bbl, bbr, Color.green);
                    Debug.DrawLine(btl, btr, Color.green);
                    //Debug.DrawLine(bl, tl);
                    //Debug.DrawLine(br, tr);
                }
            });

            timer += Time.fixedDeltaTime;
            if(timer >= collisionCheckTimer)
            {
                timer = 0.0f;
                colSystem.SortandSweep(colliders);
                colSystem.TestCollision();
                colSystem.ResolveCollision();
            }
        }
    }
}