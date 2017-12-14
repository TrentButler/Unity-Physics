using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class AABBBehaviour : MonoBehaviour
    {
        private aaBB col;
        public int ID;
        public Vector3 scale;

        private void aabbResoultion(aaBB other)
        {
            if(other.Id == 1) //OTHER GAMEOBJECT
            {
                //CHANGE COLOR OF THIS OBJECT
                GetComponent<Renderer>().material.color = Random.ColorHSV();
            }
        }

        void Start()
        {
            var pos = GetComponent<Transform>().position;
            col = ScriptableObject.CreateInstance<aaBB>() as aaBB;
            col._Init(ID, pos, scale);
            col.resolution += aabbResoultion;
        }
        
        void Update()
        {
            var pos = GetComponent<Transform>().position;
            col._Init(ID, pos, scale);
        }
    }
}