using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABBBehaviour : MonoBehaviour {

    public AABB collider1 = new AABB();
    public AABB collider2 = new AABB();
    public Utilities util = new Utilities();

    public bool Collision = false;
    public Vector2 Collider1BOXMAX = Vector2.zero;
    public Vector2 Collider1BOXMIN = Vector2.zero;

    public Vector2 Collider2BOXMAX = Vector2.zero;
    public Vector2 Collider2BOXMIN = Vector2.zero;

    private Transform colGO1;
    private Transform colGO2;

    private Vector2 col1Pos;
    private Vector2 col2Pos;

    // Use this for initialization
    void Start ()
    {
        colGO1 = GameObject.FindGameObjectWithTag("col1").transform;
        colGO2 = GameObject.FindGameObjectWithTag("col2").transform;

        col1Pos = new Vector2(colGO1.transform.position.x, colGO1.transform.position.y);
        col2Pos = new Vector2(colGO2.transform.position.x, colGO2.transform.position.y);

        collider1.Init(col1Pos, 1);
        collider2.Init(col2Pos, 1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        col1Pos = new Vector2(colGO1.transform.position.x, colGO1.transform.position.y);
        col2Pos = new Vector2(colGO2.transform.position.x, colGO2.transform.position.y);

        collider1.Update(col1Pos);
        collider2.Update(col2Pos);

        Collider1BOXMAX = collider1.Max;
        Collider1BOXMIN = collider1.Min;

        Collider2BOXMAX = collider2.Max;
        Collider2BOXMIN = collider2.Min;

        Collision = util.TestOverLap(collider1, collider2);
    }
}