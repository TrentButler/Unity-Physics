using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class AABBBehaviour : MonoBehaviour
    {
        public aaBB collider1;
        public aaBB collider2;
        public aaBB collider3;
        public aaBB collider4;

        public ColliderSystem util = new ColliderSystem();

        private List<aaBB> objects = new List<aaBB>();
        public List<string> currentOBJSColliding = new List<string>();
        public List<string> colliderXPairs = new List<string>();
        public List<string> colliderYPairs = new List<string>();
        public List<string> colliderZPairs = new List<string>();

        public bool CollisionAB = false;
        public bool CollisionCD = false;

        private Transform colGO1;
        private Transform colGO2;
        private Transform colGO3;
        private Transform colGO4;

        private Vector3 col1Pos;
        private Vector3 col2Pos;
        private Vector3 col3Pos;
        private Vector3 col4Pos;

        void Start()
        {   
            #region GameObjectTOAABB
            colGO1 = GameObject.FindGameObjectWithTag("col1").transform;
            colGO2 = GameObject.FindGameObjectWithTag("col2").transform;
            colGO3 = GameObject.FindGameObjectWithTag("col3").transform;
            colGO4 = GameObject.FindGameObjectWithTag("col4").transform;

            col1Pos = new Vector3(colGO1.transform.position.x, colGO1.transform.position.y, colGO1.transform.position.z);
            col2Pos = new Vector3(colGO2.transform.position.x, colGO2.transform.position.y, colGO2.transform.position.z);
            col3Pos = new Vector3(colGO3.transform.position.x, colGO3.transform.position.y, colGO3.transform.position.z);
            col4Pos = new Vector3(colGO4.transform.position.x, colGO4.transform.position.y, colGO4.transform.position.z);
            #endregion

            collider1 = ScriptableObject.CreateInstance<aaBB>() as aaBB;
            collider2 = ScriptableObject.CreateInstance<aaBB>() as aaBB;
            collider3 = ScriptableObject.CreateInstance<aaBB>() as aaBB;
            collider4 = ScriptableObject.CreateInstance<aaBB>() as aaBB;

            collider1._Init(1, col1Pos, 0.5f);
            collider2._Init(2, col2Pos, 0.5f);
            collider3._Init(3, col3Pos, 0.5f);
            collider4._Init(4, col4Pos, 0.5f);

            objects.Add(collider1);
            objects.Add(collider2);
            objects.Add(collider3);
            objects.Add(collider4);
        }

        void FixedUpdate()
        {
            #region UpdateColliders
            col1Pos = new Vector3(colGO1.transform.position.x, colGO1.transform.position.y, colGO1.transform.position.z);
            col2Pos = new Vector3(colGO2.transform.position.x, colGO2.transform.position.y, colGO2.transform.position.z);
            col3Pos = new Vector3(colGO3.transform.position.x, colGO3.transform.position.y, colGO3.transform.position.z);
            col4Pos = new Vector3(colGO4.transform.position.x, colGO4.transform.position.y, colGO4.transform.position.z);

            collider1._Update(col1Pos);
            collider2._Update(col2Pos);
            collider3._Update(col3Pos);
            collider4._Update(col4Pos);
            #endregion

            //CollisionAB = util.TestOverLap(collider1, collider2);
            //CollisionCD = util.TestOverLap(collider3, collider4);

            #region SortAndSweep
            colliderXPairs = null;
            colliderXPairs = new List<string>();

            colliderYPairs = null;
            colliderYPairs = new List<string>();

            colliderZPairs = null;
            colliderZPairs = new List<string>();

            util.SortandSweep(objects);

            currentOBJSColliding.Clear();
            util.currentColliding.ForEach(x =>
            {
                currentOBJSColliding.Add(x.GetPairAsString());
            });
            
            colliderXPairs.Clear();
            util.Xpairs.ForEach(x =>
            {
                colliderXPairs.Add(x.GetPairAsString());
            });

            colliderYPairs.Clear();
            util.Ypairs.ForEach(x =>
            {
                colliderYPairs.Add(x.GetPairAsString());
            });

            colliderZPairs.Clear();
            util.Zpairs.ForEach(x =>
            {
                colliderZPairs.Add(x.GetPairAsString());
            });
            #endregion
        }

        private void LateUpdate()
        {
            Debug.DrawLine(collider1.Min, collider1.Max, Color.green);
            Debug.DrawLine(collider2.Min, collider2.Max, Color.green);
            Debug.DrawLine(collider3.Min, collider3.Max, Color.green);
            Debug.DrawLine(collider4.Min, collider4.Max, Color.green);
        }
    }
}