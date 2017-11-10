using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class AgentFactoryBehaviour : MonoBehaviour
    {
        public Vector3 SpawnPosition = Vector3.zero;
        public List<Agent> activeAgents;
        public List<GameObject> activeObjects;

        public void Create()
        {
            //MAKE A BOID

            var go = new GameObject(); //CREATE AN EMPTY GAMEOBJECT,
            var boid = new Boid(); //CREATE AN BOID OBJECT

            activeAgents.Add(boid); //STORE THE BOID
            activeObjects.Add(go); //STORE THE GAMEOBJECT

            var rb = go.GetComponent<Rigidbody>();
            if(rb != null)
            {
                Destroy(rb);
            }
            
            go.name = string.Format("{0} {1} {2}", "BOID(", activeObjects.Count, ")"); //NAME THE GAMEOBJECT
            go.AddComponent<BaseAgentBehaviour>();

            var mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
            mesh.transform.SetParent(go.transform, false);

            var behaviour = go.GetComponent<AgentBehaviour>();
            behaviour.setAgent(boid);

            boid.Initalize(go.transform, 1, 2); //INITILIZE THE BOID OBJECT
        }

        public void Create(Vector3 pos)
        {
            //MAKE A BOID

            var go = new GameObject(); //CREATE AN EMPTY GAMEOBJECT,
            var boid = new Boid(); //CREATE AN BOID OBJECT

            activeAgents.Add(boid); //STORE THE BOID
            activeObjects.Add(go); //STORE THE GAMEOBJECT

            var rb = go.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Destroy(rb);
            }

            go.name = string.Format("{0} {1} {2}", "BOID(", activeObjects.Count, ")"); //NAME THE GAMEOBJECT
            go.AddComponent<BaseAgentBehaviour>();

            var mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
            mesh.transform.SetParent(go.transform, false);

            var behaviour = go.GetComponent<AgentBehaviour>();
            behaviour.setAgent(boid);


            go.transform.position = pos;
            boid.Initalize(go.transform, 1, 2); //INITILIZE THE BOID OBJECT
        }

        public void DestroyAll()
        {
            for(int i = 0; i < activeObjects.Count; i++)
            {
                DestroyImmediate(activeObjects[i]);
            }
            activeObjects.Clear();
            activeAgents.Clear();
        }

        public void Destroy(int index)
        {
            if (activeObjects.Count > 0)
            {
                activeObjects.RemoveAt(index);
                activeAgents.RemoveAt(index);
            }
        }

        private void OnEnable()
        {
            activeAgents = new List<Agent>();
            activeObjects = new List<GameObject>();
        }

        private void OnDisable()
        {
            DestroyAll();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Keypad0))
            {
                Create(SpawnPosition);
            }

            if(Input.GetKeyDown(KeyCode.Keypad1))
            {
                DestroyAll();
            }
        }
    }
}