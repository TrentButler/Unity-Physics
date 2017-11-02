using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB
{
    public void Init(string name, Vector2 position, float size)
    {
        //XMIN = POSITION.X -= SIZE
        //XMAX = POSITION.X += SIZE
        //YMIN = POSITION.Y -= SIZE
        //YMAX = POSITION.Y += SIZE

        Name = name;
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

    public string Name = "";
    public Vector2 Position = Vector2.zero;
    public Vector2 Min = Vector2.zero;
    public Vector2 Max = Vector2.zero;
    public float _size;
}

public class ColPair
{
    private List<AABB> pair = new List<AABB>();

    public void AddPair(AABB one, AABB two)
    {
        if(pair.Count <= 0)
        {
            pair.Add(one);
            pair.Add(two);
        }
    }

    public List<AABB> GetPair()
    {
        return pair;
    }
}

[System.Serializable]
public class Utilities
{
    
    private List<AABB> axisList = new List<AABB>();
    public List<ColPair> pairs = new List<ColPair>();

    public bool TestOverLap(AABB col1, AABB col2)
    {
        var d1x = col2.Min.x - col1.Max.x;
        var d1y = col2.Min.y - col1.Max.y;

        var d2x = col1.Min.x - col2.Max.x;
        var d2y = col1.Min.y - col2.Max.y;

        if (d1x > 0 || d1y > 0)
        {
            return false;
        }

        if (d2x > 0 || d2y > 0)
        {
            return false;
        }

        return true;
    }

    public void SortandSweep(List<AABB> objectsInScene)
    {
        var sortedObjects = objectsInScene.OrderBy(x => x.Min.x).ToList(); //SORT ALL OBJECTS IN SCENE BY THE MINIMUM X        

        axisList = sortedObjects; //STORE THE SORTED OBJECTS

        List<AABB> activeList = new List<AABB>(); //CREATE A NEW LIST OF AABB

        for(int i = 0; i < axisList.Count - 1; i++)
        {
            activeList.Add(axisList[i]); // ADD THE FIRST ITEM FROM THE AXISLIST

            if (axisList[i + 1].Min.x > activeList[i].Max.x) //IF NEWITEM.RIGHT > CURRENTITEM.LEFT
            {
                activeList.Remove(activeList[i]); //REMOVE THE CURRENT ITEM
            }

            else
            {
                ColPair newPair = new ColPair();
                newPair.AddPair(activeList[i], axisList[i + 1]);
                pairs.Add(newPair); //REPORT A PAIR

                activeList.Add(axisList[i + 1]); //ADD NEWITEM TO THE ACTIVE LIST
            }
        }
    }
}