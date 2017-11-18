using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class MainCameraBehaviour : MonoBehaviour
    {
        public bool Follow = false;
        public Vector3 Offset;
        public float CameraRotation;
        private int agentIndex = 0;

        public void FollowBoids()
        {
            if(Follow == true)
            {
                Follow = false;
            }

            else
            {
                Follow = true;
            }
        }

        public void NextBoid()
        {
            agentIndex += 1; //NEXT AGENT
        }

        public void PreviousBoid()
        {
            agentIndex -= 1; //PREVIOUS AGENT
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Offset.z -= 1f;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                Offset.z += 1f;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Offset.x -= 1f;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                Offset.x += 1f;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                agentIndex += 1; //PREVIOUS AGENT
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                agentIndex -= 1; //NEXT AGENT
            }

            Debug.Log(agentIndex); //REMOVE THIS
        }

        void FixedUpdate()
        {
            var allAgents = GameObject.FindObjectsOfType<BaseAgentBehaviour>().ToList();            

            if(agentIndex >= allAgents.Count || agentIndex < 0)
            {
                agentIndex = 0;
            }

            if (Follow == true)
            {
                if(allAgents.Count > 0)
                {
                    var rearAgent = allAgents[agentIndex].transform.position;

                    GetComponent<Camera>().transform.position = rearAgent + Offset;
                    GetComponent<Camera>().transform.LookAt(rearAgent);
                }
            }

            if (Follow == false)
            {
                //FIXED ANGLE CAM
                Vector3 fixedPos = new Vector3(40.0f, 20.0f, -10.0f);
                Quaternion fixedRot = Quaternion.Euler(16.0f, -31.0f, -2.5f);

                transform.position = fixedPos;
                transform.rotation = fixedRot;
            }
        }
    }
}