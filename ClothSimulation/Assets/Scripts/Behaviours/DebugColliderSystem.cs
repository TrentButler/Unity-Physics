using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class DebugColliderSystem : MonoBehaviour
    {
        public List<aaBB> colliders;
        public bool showColliders = false;

        private ColliderSystem colSystem;
        
        void Start()
        {

            colSystem = new ColliderSystem(); //INTILIZE THE COLLIDERSYSTEM OBJECT

            //COLLECT ALL COLLIDERS IN THE SCENE
            colliders = new List<aaBB>();
        }

        void FixedUpdate()
        {
            var allColliders = GameObject.FindObjectsOfType<aaBB>().ToList(); //COLLECT ALL COLLIDERS IN THE SCENE

            allColliders.ForEach(x =>
            {
                if(!colliders.Contains(x))
                    colliders.Add(x);
            });

            foreach (var agent in allColliders)
            {
                foreach(var agent2 in allColliders)
                {
                    if(agent2 == agent)
                    {
                        continue;
                    }

                    if(colSystem.TestOverLap(agent, agent2))
                    {
                        agent.resolution.Invoke(agent2);
                        agent2.resolution.Invoke(agent);
                    }
                }

                if (showColliders)
                {
                    var fbl = new Vector3(agent.Min.x, agent.Min.y, agent.Max.z);
                    var fbr = new Vector3(agent.Max.x, agent.Min.y, agent.Max.z);
                    var ftr = new Vector3(agent.Max.x, agent.Max.y, agent.Max.z);
                    var ftl = new Vector3(agent.Min.x, agent.Max.y, agent.Max.z);

                    var bbl = new Vector3(agent.Min.x, agent.Min.y, agent.Min.z);
                    var bbr = new Vector3(agent.Max.x, agent.Min.y, agent.Min.z);
                    var btr = new Vector3(agent.Max.x, agent.Max.y, agent.Min.z);
                    var btl = new Vector3(agent.Min.x, agent.Max.y, agent.Min.z);

                    Debug.DrawLine(fbl, fbr, Color.red);
                    Debug.DrawLine(ftl, ftr, Color.red);
                    Debug.DrawLine(bbl, bbr, Color.red);
                    Debug.DrawLine(btl, btr, Color.red);

                    Debug.DrawLine(btl, bbl, Color.green);
                    Debug.DrawLine(btr, bbr, Color.green);
                    Debug.DrawLine(ftl, fbl, Color.green);
                    Debug.DrawLine(ftr, fbr, Color.green);

                    Debug.DrawLine(ftl, btl, Color.blue);
                    Debug.DrawLine(fbl, bbl, Color.blue);
                    Debug.DrawLine(ftr, btr, Color.blue);
                    Debug.DrawLine(fbr, bbr, Color.blue);
                }
            }
        }
    }
}