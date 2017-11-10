using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    public class AgentFactoryBehaviour : MonoBehaviour
    {
        public int SpawnCount = 0;
        public Vector3 SpawnPosition = Vector3.zero;
        public List<Agent> activeAgents;
        public List<GameObject> activeObjects;
        public GameObject Mesh;

        public void Create()
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

            //go.name = string.Format("{0} {1} {2}", "BOID(", activeObjects.Count, ")"); //NAME THE GAMEOBJECT
            go.name = "BOID";
            go.AddComponent<BaseAgentBehaviour>();

            if (Mesh == null)
            {
                var mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var col = mesh.GetComponent<BoxCollider>();
                Destroy(col);
                mesh.transform.SetParent(go.transform, false);
            }

            if (Mesh != null)
            {
                var prefab = GameObject.Instantiate(Mesh, go.transform.position, go.transform.rotation);
                prefab.transform.SetParent(go.transform, false);
            }

            var behaviour = go.GetComponent<AgentBehaviour>();
            behaviour.setAgent(boid);

            boid.Initalize(go.transform, 1, 2); //INITILIZE THE BOID OBJECT
        }

        public void Create(int count)
        {
            if (count <= 0)
            {
                count = 1;
            }

            for (int i = 0; i < count; i++)
            {
                //RANDOM NUMBER
                float randX = Random.Range(0.0f, 600.0f);
                float randY = Random.Range(0.0f, 600.0f);
                float randZ = Random.Range(0.0f, 600.0f);
                Vector3 randPosition = new Vector3(randX, randY, randZ);

                var go = new GameObject(); //CREATE AN EMPTY GAMEOBJECT,
                var boid = new Boid(); //CREATE AN BOID OBJECT

                activeAgents.Add(boid); //STORE THE BOID
                activeObjects.Add(go); //STORE THE GAMEOBJECT

                var rb = go.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Destroy(rb);
                }

                //go.name = string.Format("{0} {1} {2}", "BOID(", activeObjects.Count, ")"); //NAME THE GAMEOBJECT
                go.name = "BOID";
                go.AddComponent<BaseAgentBehaviour>();

                if (Mesh == null)
                {
                    var mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    var col = mesh.GetComponent<BoxCollider>();
                    Destroy(col);
                    mesh.transform.SetParent(go.transform, false);
                }

                if (Mesh != null)
                {
                    var prefab = GameObject.Instantiate(Mesh, go.transform.position, go.transform.rotation);
                    prefab.transform.SetParent(go.transform, false);
                }

                var behaviour = go.GetComponent<AgentBehaviour>();
                behaviour.setAgent(boid);

                go.transform.position = randPosition;
                boid.Initalize(go.transform, 1, 2); //INITILIZE THE BOID OBJECT
            }
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

            //go.name = string.Format("{0} {1} {2}", "BOID(", activeObjects.Count, ")"); //NAME THE GAMEOBJECT
            go.name = "BOID";
            go.AddComponent<BaseAgentBehaviour>();

            if (Mesh == null)
            {
                var mesh = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var col = mesh.GetComponent<BoxCollider>();
                Destroy(col);
                mesh.transform.SetParent(go.transform, false);
            }

            if (Mesh != null)
            {
                var prefab = GameObject.Instantiate(Mesh, go.transform.position, go.transform.rotation);
                prefab.transform.SetParent(go.transform, false);
            }

            var behaviour = go.GetComponent<AgentBehaviour>();
            behaviour.setAgent(boid);

            go.transform.position = pos;
            boid.Initalize(go.transform, 1, 2); //INITILIZE THE BOID OBJECT
        }

        public void DestroyAll()
        {
            for (int i = 0; i < activeObjects.Count; i++)
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
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                Create(SpawnPosition);
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                DestroyAll();
            }
        }
    }
}