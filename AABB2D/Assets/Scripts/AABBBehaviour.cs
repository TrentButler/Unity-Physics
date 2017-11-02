using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABBBehaviour : MonoBehaviour {
    
    public AABB collider1 = new AABB();
    public AABB collider2 = new AABB();
    public AABB collider3 = new AABB();
    public AABB collider4 = new AABB();

    public Utilities util = new Utilities();

    private List<AABB> objects = new List<AABB>();
    public List<AABB> colliderPairs = new List<AABB>();

    public bool Collision = false;
    //public Vector2 Collider1BOXMAX = Vector2.zero;
    //public Vector2 Collider1BOXMIN = Vector2.zero;
    //public Vector2 Collider2BOXMAX = Vector2.zero;
    //public Vector2 Collider2BOXMIN = Vector2.zero;

    private Transform colGO1;
    private Transform colGO2;
    private Transform colGO3;
    private Transform colGO4;

    private Vector2 col1Pos;
    private Vector2 col2Pos;
    private Vector2 col3Pos;
    private Vector2 col4Pos;
    
    void Start ()
    {
        colGO1 = GameObject.FindGameObjectWithTag("col1").transform;
        colGO2 = GameObject.FindGameObjectWithTag("col2").transform;
        colGO3 = GameObject.FindGameObjectWithTag("col3").transform;
        colGO4 = GameObject.FindGameObjectWithTag("col4").transform;

        col1Pos = new Vector2(colGO1.transform.position.x, colGO1.transform.position.y);
        col2Pos = new Vector2(colGO2.transform.position.x, colGO2.transform.position.y);
        col3Pos = new Vector2(colGO3.transform.position.x, colGO3.transform.position.y);
        col4Pos = new Vector2(colGO4.transform.position.x, colGO4.transform.position.y);

        collider1.Init("A", col1Pos, 1);
        collider2.Init("B", col2Pos, 1);
        collider3.Init("C", col3Pos, 1);
        collider4.Init("D", col4Pos, 1);
        
        objects.Add(collider1);
        objects.Add(collider2);
        objects.Add(collider3);
        objects.Add(collider4);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        col1Pos = new Vector2(colGO1.transform.position.x, colGO1.transform.position.y);
        col2Pos = new Vector2(colGO2.transform.position.x, colGO2.transform.position.y);
        col3Pos = new Vector2(colGO3.transform.position.x, colGO3.transform.position.y);
        col4Pos = new Vector2(colGO4.transform.position.x, colGO4.transform.position.y);

        collider1.Update(col1Pos);
        collider2.Update(col2Pos);
        collider3.Update(col3Pos);
        collider4.Update(col4Pos);

        //Collider1BOXMAX = collider1.Max;
        //Collider1BOXMIN = collider1.Min;

        //Collider2BOXMAX = collider2.Max;
        //Collider2BOXMIN = collider2.Min;

        Collision = util.TestOverLap(collider1, collider2);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            util.SortandSweep(objects);
        }
    }
}