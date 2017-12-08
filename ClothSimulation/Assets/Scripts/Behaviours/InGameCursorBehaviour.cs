using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Trent
{
    public class InGameCursorBehaviour : MonoBehaviour
    {
        public GameObject cursorModel;
        public Vector3 cursorOffset;
        public float cursorSpeed;
        public float clickDragCoefficient;

        public Vector3 cursorWorldPosition;
        private GameObject cursorGO;

        //FOR COLLISION
        public Vector3 cursorColScale;
        public aaBB col;

        public bool showCollider = false;

        //DISABLE THE MAINCAMERABEHAVIOUR WHEN THE SCROLL WHEEL IS MOVING AND THE SHIFT KEY IS PRESSED
        private MainCameraBehaviour mainCamBehaviour;

        private void colresolution(aaBB other)
        {
            Debug.Log("CURSOR COLLIDE WITH: " + other.Id);
        }

        void Start()
        {
            mainCamBehaviour = GetComponent<MainCameraBehaviour>();

            //INSTANTIATE THE CURSOR MODEL, PARENT IT TO THE CURSORGO FIELD
            cursorGO = new GameObject();
            cursorGO.name = "cursorGO";
            var model = Instantiate(cursorModel, cursorGO.transform.position, cursorGO.transform.rotation);
            model.transform.SetParent(cursorGO.transform);

            var cam = GetComponent<Camera>();
            var mousePos = Input.mousePosition;

            cursorWorldPosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0) + cursorOffset);

            cursorGO.transform.position = cursorWorldPosition;
            cursorGO.transform.rotation = cam.transform.rotation;

            col = ScriptableObject.CreateInstance<aaBB>() as aaBB;
            col._Init(-99, cursorGO.transform.position, cursorColScale);
            col.resolution += colresolution;
        }

        void Update()
        {
            var cam = GetComponent<Camera>();
            var mousePos = Input.mousePosition;

            if(Input.GetKey(KeyCode.LeftShift))
            {
                mainCamBehaviour.enabled = false;
                var scrollDelta = Input.GetAxis("Mouse ScrollWheel"); //GET THE MOUSE SCROLL DELTA
                Vector3 translation = new Vector3(0, 0, scrollDelta * cursorSpeed); //DERIVE A TRANSLATION VECTOR
                cursorOffset += translation;
            }
            else
            {
                mainCamBehaviour.enabled = true;
            }

            cursorOffset.z = Mathf.Clamp(cursorOffset.z, 2.0f, 100.0f);

            cursorWorldPosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0) + cursorOffset);
            
            cursorGO.transform.position = cursorWorldPosition;
            cursorGO.transform.rotation = cam.transform.rotation;

            col._Update(cursorGO.transform.position, cursorColScale);
        }

        private void LateUpdate()
        {
            if(showCollider)
            {
                var fbl = new Vector3(col.Min.x, col.Min.y, col.Max.z);
                var fbr = new Vector3(col.Max.x, col.Min.y, col.Max.z);
                var ftr = new Vector3(col.Max.x, col.Max.y, col.Max.z);
                var ftl = new Vector3(col.Min.x, col.Max.y, col.Max.z);

                var bbl = new Vector3(col.Min.x, col.Min.y, col.Min.z);
                var bbr = new Vector3(col.Max.x, col.Min.y, col.Min.z);
                var btr = new Vector3(col.Max.x, col.Max.y, col.Min.z);
                var btl = new Vector3(col.Min.x, col.Max.y, col.Min.z);

                var br = new Vector3(col.Max.x, col.Min.y);

                Debug.DrawLine(fbl, fbr, Color.green);
                Debug.DrawLine(ftl, ftr, Color.green);

                Debug.DrawLine(bbl, bbr, Color.green);
                Debug.DrawLine(btl, btr, Color.green);
                //Debug.DrawLine(bl, tl);
                //Debug.DrawLine(br, tr);
            }
        }
    }
}