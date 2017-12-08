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

        private InGameCursorBehaviour cursor;
        private DebugPlaneBehaviour floor;

        void Start()
        {
            cursor = GameObject.FindObjectOfType<InGameCursorBehaviour>();
            floor = GameObject.FindObjectOfType<DebugPlaneBehaviour>();

            timer = 0.0f;
            colSystem = new ColliderSystem(); //INTILIZE THE COLLIDERSYSTEM OBJECT

            //COLLECT ALL COLLIDERS IN THE SCENE
            colliders = new List<aaBB>();
        }

        void FixedUpdate()
        {
            var allParticles = GameObject.FindObjectsOfType<ParticleBehaviour>().ToList(); //COLLECT ALL COLLIDERS IN THE SCENE

            allParticles.ForEach(x =>
            {
                colliders.Add(x._collider);
            });

            foreach (var agent in allParticles)
            {
                if (agent._collider.Id == -99 || agent._collider.Id == -98)
                {
                    continue;
                }

                if (colSystem.TestOverLap(cursor.col, agent._collider))
                {
                    agent.isColliding = true;

                    agent._collider.resolution.Invoke(cursor.col);

                    if (Input.GetMouseButton(0))
                    {
                        //get the mouse delta
                        //var deltaX = Input.GetAxis("Mouse X");
                        //var deltaY = Input.GetAxis("Mouse Y");

                        //Vector3 translation = new Vector3(deltaX * cursor.clickDragCoefficient, deltaY * cursor.clickDragCoefficient, 0);

                        //agent.particle.AddForce(translation);
                        agent.particle.Update(cursor.cursorWorldPosition);
                    }

                    if (Input.GetMouseButtonDown(1))
                    {
                        agent.particle.isKinematic = true;
                    }

                    if (Input.GetMouseButtonDown(2))
                    {
                        agent.particle.isKinematic = false;
                    }

                    //Debug.Log("CURSOR COLLIDE WITH PARTICLE");
                }

                if (colSystem.TestOverLap(floor.col, agent._collider))
                {
                    agent.isColliding = true;
                    agent._collider.resolution.Invoke(floor.col);

                    Debug.Log("FLOOR COLLIDE WITH PARTICLE");
                }

                else
                {
                    agent.isColliding = false;
                }

                if (showColliders)
                {
                    var fbl = new Vector3(agent._collider.Min.x, agent._collider.Min.y, agent._collider.Max.z);
                    var fbr = new Vector3(agent._collider.Max.x, agent._collider.Min.y, agent._collider.Max.z);
                    var ftr = new Vector3(agent._collider.Max.x, agent._collider.Max.y, agent._collider.Max.z);
                    var ftl = new Vector3(agent._collider.Min.x, agent._collider.Max.y, agent._collider.Max.z);

                    var bbl = new Vector3(agent._collider.Min.x, agent._collider.Min.y, agent._collider.Min.z);
                    var bbr = new Vector3(agent._collider.Max.x, agent._collider.Min.y, agent._collider.Min.z);
                    var btr = new Vector3(agent._collider.Max.x, agent._collider.Max.y, agent._collider.Min.z);
                    var btl = new Vector3(agent._collider.Min.x, agent._collider.Max.y, agent._collider.Min.z);

                    Debug.DrawLine(fbl, fbr, Color.green);
                    Debug.DrawLine(ftl, ftr, Color.green);

                    Debug.DrawLine(bbl, bbr, Color.green);
                    Debug.DrawLine(btl, btr, Color.green);
                    //Debug.DrawLine(bl, tl);
                    //Debug.DrawLine(br, tr);
                }
            }

            //timer += Time.fixedDeltaTime;
            //if (timer >= collisionCheckTimer)
            //{
            //    timer = 0.0f;
            //    colSystem.SortandSweep(colliders);
            //    colSystem.TestCollision();
            //    colSystem.ResolveCollision();
            //}
        }
    }
}