using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    [System.Serializable]
    public class aaBB : ScriptableObject
    {
        public void _Init(string name, Vector3 position, float size)
        {
            //XMIN = POSITION.X -= SIZE
            //XMAX = POSITION.X += SIZE
            //YMIN = POSITION.Y -= SIZE
            //YMAX = POSITION.Y += SIZE

            _name = name;
            _position = position;
            _size = size;
            min.x = position.x - size;
            min.y = position.y - size;
            min.z = position.z - size;

            max.x = position.x + size;
            max.y = position.y + size;
            max.z = position.z + size;
        }

        public void _Update(Vector3 position)
        {
            _position = position;

            min.x = position.x - Size;
            min.y = position.y - Size;
            min.z = position.z - Size;

            max.x = position.x + Size;
            max.y = position.y + Size;
            max.z = position.z + Size;
        }

        public string Name { get { return _name; } }
        private string _name;

        public Vector3 Position { get { return _position; } }
        private Vector3 _position;

        public Vector3 Min { get { return min; } }
        private Vector3 min;

        public Vector3 Max { get { return max; } }
        private Vector3 max;

        public float Size { get { return _size; } }
        private float _size;
    }

    public class ColPair
    {
        private List<aaBB> pair = new List<aaBB>();

        public void AddPair(aaBB one, aaBB two)
        {
            if (pair.Count <= 0)
            {
                pair.Add(one);
                if (two == one)
                {
                    return;
                }
                pair.Add(two);
            }
        }

        public bool ComparePair(ColPair other)
        {
            var one = other.GetPair()[0];
            var two = other.GetPair()[1];

            if (pair != null && pair.Count > 1)
            {
                var three = pair[0];
                var four = pair[1];

                if (one == three || one == four)
                {
                    if (two == three || two == four)
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public List<aaBB> GetPair()
        {
            return pair;
        }

        public string GetPairAsString()
        {
            if (pair.Count > 0)
            {
                string sPair = "";
                for (int i = 0; i < pair.Count; i++)
                {
                    sPair += (pair[i].Name + ",");
                }
                return sPair;
            }
            return "";
        }
    }

    public class Collider
    {
        private List<aaBB> XaxisList = new List<aaBB>();
        private List<aaBB> YaxisList = new List<aaBB>();
        private List<aaBB> ZaxisList = new List<aaBB>();

        public List<ColPair> Xpairs = new List<ColPair>();
        public List<ColPair> Ypairs = new List<ColPair>();
        public List<ColPair> Zpairs = new List<ColPair>();

        public List<string> agentsColliding = new List<string>();

        public bool TestOverLap(aaBB col1, aaBB col2)
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

        //NEEDS WORK
        public void TestCollision()
        {
            agentsColliding = new List<string>();
            //IF THERE ARE DUPLICATE PAIRS IN ALL THREE AXIES, THERE IS COLLISION

            //12:35 - CHECK THE X,Y,Z AXISLISTs FOR DUPLICATE PAIRS
        }

        public void SortandSweep(List<aaBB> objectsInScene)
        {
            Xpairs = new List<ColPair>();
            Ypairs = new List<ColPair>();
            Zpairs = new List<ColPair>();

            var sortedX = objectsInScene.OrderBy(x => x.Min.x).ToList(); //SORT ALL OBJECTS IN SCENE BY THE MINIMUM X
            var sortedY = objectsInScene.OrderBy(x => x.Min.y).ToList(); //SORT ALL OBJECTS IN SCENE BY THE MINIMUM Y
            var sortedZ = objectsInScene.OrderBy(x => x.Min.z).ToList(); //SORT ALL OBJECTS IN SCENE BY THE MINIMUM Z

            XaxisList = sortedX; //STORE THE SORTED OBJECTS
            YaxisList = sortedY; //STORE THE SORTED OBJECTS
            ZaxisList = sortedZ; //STORE THE SORTED OBJECTS

            List<aaBB> activeList = new List<aaBB>(); //CREATE A NEW LIST OF AABB

            #region XSortAndSweep
            for (int i = 0; i < XaxisList.Count - 1; i++)
            {
                int j = i;
                activeList.Add(XaxisList[i]); // ADD THE FIRST ITEM FROM THE AXISLIST

                //CHECK FOR THE INCREMENTOR BEING GREATER THAN THE SIZE OF ACTIVE LIST
                if (j > activeList.Count - 1)
                {
                    j = 0;
                }
                if (XaxisList[i + 1].Min.x > activeList[j].Max.x) //IF NEWITEM.RIGHT > CURRENTITEM.LEFT
                {
                    activeList.Remove(activeList[j]); //REMOVE THE CURRENT ITEM
                }

                else
                {
                    ColPair newPair = new ColPair();
                    newPair.AddPair(activeList[j], XaxisList[i + 1]);
                    if (Xpairs.Contains(newPair) == false) //DO NOT ADD PAIR IF ALREADY IN THE 'PAIRS' LIST
                    {
                        Xpairs.Add(newPair); //REPORT THE PAIR
                    }

                    activeList.Add(XaxisList[i + 1]); //ADD NEWITEM TO THE ACTIVE LIST
                }
            }
            #endregion

            #region YSortAndSweep
            activeList = new List<aaBB>();
            for (int i = 0; i < YaxisList.Count - 1; i++)
            {
                int j = i;
                activeList.Add(YaxisList[i]); // ADD THE FIRST ITEM FROM THE AXISLIST

                //CHECK FOR THE INCREMENTOR BEING GREATER THAN THE SIZE OF ACTIVE LIST
                if (j > activeList.Count - 1)
                {
                    j = 0;
                }

                if (YaxisList[i + 1].Min.y > activeList[j].Max.y) //IF NEWITEM.RIGHT > CURRENTITEM.LEFT
                {
                    activeList.Remove(activeList[j]); //REMOVE THE CURRENT ITEM
                }

                else
                {
                    ColPair newPair = new ColPair();
                    newPair.AddPair(activeList[j], YaxisList[i + 1]);
                    if (Ypairs.Contains(newPair) == false) //DO NOT ADD PAIR IF ALREADY IN THE 'PAIRS' LIST
                    {
                        Ypairs.Add(newPair); //REPORT THE PAIR
                    }

                    activeList.Add(YaxisList[i + 1]); //ADD NEWITEM TO THE ACTIVE LIST
                }
            }
            #endregion

            #region ZSortAndSweep
            activeList = new List<aaBB>();
            for (int i = 0; i < ZaxisList.Count - 1; i++)
            {
                int j = i;
                activeList.Add(ZaxisList[i]); // ADD THE FIRST ITEM FROM THE AXISLIST

                //CHECK FOR THE INCREMENTOR BEING GREATER THAN THE SIZE OF ACTIVE LIST
                if (j > activeList.Count - 1)
                {
                    j = 0;
                }
                if (ZaxisList[i + 1].Min.z > activeList[j].Max.z) //IF NEWITEM.RIGHT > CURRENTITEM.LEFT
                {
                    activeList.Remove(activeList[j]); //REMOVE THE CURRENT ITEM
                }

                else
                {
                    ColPair newPair = new ColPair();
                    newPair.AddPair(activeList[j], ZaxisList[i + 1]);
                    if (Zpairs.Contains(newPair) == false) //DO NOT ADD PAIR IF ALREADY IN THE 'PAIRS' LIST
                    {
                        Zpairs.Add(newPair); //REPORT THE PAIR
                    }

                    activeList.Add(ZaxisList[i + 1]); //ADD NEWITEM TO THE ACTIVE LIST
                }
            }
            #endregion

            TestCollision();
        }
    }
}