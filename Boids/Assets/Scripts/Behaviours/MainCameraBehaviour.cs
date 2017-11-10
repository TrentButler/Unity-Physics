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

            Debug.Log(agentIndex);
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
                var rearAgent = allAgents[agentIndex].transform.position;

                GetComponent<Camera>().transform.position = rearAgent + Offset;
                GetComponent<Camera>().transform.LookAt(rearAgent);
            }

            if (Follow == false)
            {
                //FREE LOOK CAMERA
            }
        }
    }
}