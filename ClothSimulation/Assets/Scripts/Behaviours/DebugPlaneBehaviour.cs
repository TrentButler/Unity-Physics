using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class DebugPlaneBehaviour : MonoBehaviour
    {
        public aaBB col;
        public Vector3 size;
        public bool showBounds = false;

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
            transform.localScale = col.Scale * 2;

            if (showBounds)
            {
                var fbl = new Vector3(col.Min.x, col.Min.y, col.Max.z);
                var fbr = new Vector3(col.Max.x, col.Min.y, col.Max.z);
                var ftr = new Vector3(col.Max.x, col.Max.y, col.Max.z);
                var ftl = new Vector3(col.Min.x, col.Max.y, col.Max.z);

                var bbl = new Vector3(col.Min.x, col.Min.y, col.Min.z);
                var bbr = new Vector3(col.Max.x, col.Min.y, col.Min.z);
                var btr = new Vector3(col.Max.x, col.Max.y, col.Min.z);
                var btl = new Vector3(col.Min.x, col.Max.y, col.Min.z);

                Debug.DrawLine(fbl, fbr, Color.green);
                Debug.DrawLine(ftl, ftr, Color.green);

                Debug.DrawLine(bbl, bbr, Color.green);
                Debug.DrawLine(btl, btr, Color.green);
            }
        }
    }
}