using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB
{
    public void Init(Vector2 position, float size)
    {
        //XMIN = POSITION.X -= SIZE
        //XMAX = POSITION.X += SIZE
        //YMIN = POSITION.Y -= SIZE
        //YMAX = POSITION.Y += SIZE

        Position = position;
        _size = size;
        Min.x = position.x - size;
        Min.y = position.y - size;
        Max.x = position.x + size;
        Max.y = position.y + size;
    }

    public void Update(Vector2 position)
    {
        Position = position;

        Min.x = position.x - _size;
        Min.y = position.y - _size;
        Max.x = position.x + _size;
        Max.y = position.y + _size;
    }

    public Vector2 Position = Vector2.zero;
    public Vector2 Min = Vector2.zero;
    public Vector2 Max = Vector2.zero;
    private float _size;
}

public class Utilities
{
    public bool TestOverLap(AABB col1, AABB col2)
    {
        if (col1.Max.x >= col2.Min.x && col1.Max.y >= col2.Min.y)
        {
            return true;
        }
        return false;
    }
}