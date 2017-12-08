using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class DebugPlaneBehaviour : MonoBehaviour
    {
        public aaBB col;
        public Vector3 size;

        void Start()
        {
            col = ScriptableObject.CreateInstance<aaBB>() as aaBB;
            col._Init(-98, transform.position, size);
        }

        void FixedUpdate()
        {
            col._Update(transform.position, size);
        }

        private void LateUpdate()
        {
            transform.localScale = col.Scale;
        }
    }
}