﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trent
{
    [System.Serializable]
    public class aaBB : ScriptableObject
    {
        public int Id { get { return _id; } }
        private int _id;

        public Vector3 Position { get { return _position; } }
        private Vector3 _position;

        public Vector3 Min { get { return min; } }
        private Vector3 min;

        public Vector3 Max { get { return max; } }
        private Vector3 max;

        public Vector3 Scale { get { return _scale; } }
        private Vector3 _scale;

        public void _Init(int id, Vector3 position, Vector3 scale)
        {
            //XMIN = POSITION.X -= SIZE
            //XMAX = POSITION.X += SIZE
            //YMIN = POSITION.Y -= SIZE
            //YMAX = POSITION.Y += SIZE
            
            _id = id;
            _position = position;
            _scale = scale;
            min.x = position.x - scale.x;
            min.y = position.y - scale.y;
            min.z = position.z - scale.z;

            max.x = position.x + scale.x;
            max.y = position.y + scale.y;
            max.z = position.z + scale.z;
        }

        public void _Update(Vector3 position)
        {
            _position = position;

            min.x = position.x - Scale.x;
            min.y = position.y - Scale.y;
            min.z = position.z - Scale.z;

            max.x = position.x + Scale.x;
            max.y = position.y + Scale.y;
            max.z = position.z + Scale.z;
        }
        public void _Update(Vector3 position, Vector3 size)
        {
            _position = position;
            _scale = size;

            min.x = position.x - Scale.x;
            min.y = position.y - Scale.y;
            min.z = position.z - Scale.z;

            max.x = position.x + Scale.x;
            max.y = position.y + Scale.y;
            max.z = position.z + Scale.z;
        }

        public delegate void CollisionResolution(aaBB other);
        public CollisionResolution resolution;
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
            //SORT BY LOWEST ID(int)
            pair.Sort((x, y) => { return x.Id.CompareTo(y.Id); });

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
            //SORT BY LOWEST ID(int)
            pair.Sort((x, y) => { return x.Id.CompareTo(y.Id); });

            return pair;
        }

        public string GetPairAsString()
        {
            //SORT BY LOWEST ID(int)
            pair.Sort((x, y) => { return x.Id.CompareTo(y.Id); });

            if (pair.Count > 0)
            {
                string sPair = "";
                for (int i = 0; i < pair.Count; i++)
                {
                    sPair += ("COL:" + pair[i].Id + ",");
                }
                return sPair;
            }
            return "";
        }
    }

    public class ColliderSystem
    {
        private List<aaBB> XaxisList;
        private List<aaBB> YaxisList;
        private List<aaBB> ZaxisList;

        public List<ColPair> Xpairs;
        public List<ColPair> Ypairs;
        public List<ColPair> Zpairs;

        public List<aaBB> activeList;
        private List<ColPair> possibleCollision;
        public List<ColPair> currentColliding;

        public ColliderSystem()
        {
            XaxisList = new List<aaBB>();
            YaxisList = new List<aaBB>();
            ZaxisList = new List<aaBB>();

            Xpairs = new List<ColPair>();
            Ypairs = new List<ColPair>();
            Zpairs = new List<ColPair>();

            possibleCollision = new List<ColPair>();
            currentColliding = new List<ColPair>();
        }

        public bool TestOverLap(aaBB col1, aaBB col2)
        {
            var d1x = col2.Min.x - col1.Max.x;
            var d1y = col2.Min.y - col1.Max.y;
            var d1z = col2.Min.z - col1.Max.z;

            var d2x = col1.Min.x - col2.Max.x;
            var d2y = col1.Min.y - col2.Max.y;
            var d2z = col1.Min.z - col2.Max.z;

            if (d1x > 0 || d1y > 0 || d1z > 0)
            {
                return false;
            }

            if (d2x > 0 || d2y > 0 || d2z > 0)
            {
                return false;
            }

            return true;
        }

        public void ResolveCollision()
        {
            for(int i = 0; i < currentColliding.Count; i++)
            {
                var col1 = currentColliding[i].GetPair()[0];
                var col2 = currentColliding[i].GetPair()[1];

                col1.resolution.Invoke(col2);
                col2.resolution.Invoke(col1);
            }
        }

        //NEEDS WORK
        //TEST THIS
        public void TestCollision()
        {
            if(currentColliding != null)
            {
                currentColliding = null;
            }

            currentColliding = new List<ColPair>();
            //IF THERE ARE DUPLICATE PAIRS IN ALL THREE AXIES, THERE IS COLLISION
            //12:35 - CHECK THE X,Y,Z AXISLISTs FOR DUPLICATE PAIRS

            for (int i = 0; i < Xpairs.Count; i++)
            {
                //COMPARE XPAIR[I] TO YPAIR[J]
                for (int j = 0; j < Ypairs.Count; j++)
                {
                    if (Xpairs[i].ComparePair(Ypairs[j]) == true)
                    {
                        //ADD THIS PAIR TO THE POSSIBLECOLLISION LIST
                        if(TestOverLap(Xpairs[i].GetPair()[0], Xpairs[i].GetPair()[1]) == true)
                        {
                            if(possibleCollision.Contains(Xpairs[i]) == false)
                            {
                                possibleCollision.Add(Xpairs[i]);
                            }
                        }
                    }
                }
            }

            //COMPARE EACH PAIR FROM THE ZAXIS TO ALL OF THE PAIRS IN POSSIBLECOLLISION LIST
            for (int i = 0; i < Zpairs.Count; i++)
            {
                //COMPARE ZPAIR[I] TO POSSIBLECOLLISION[J]
                for (int j = 0; j < possibleCollision.Count; j++)
                {
                    if (Zpairs[i].ComparePair(possibleCollision[j]) == true)
                    {
                        if(TestOverLap(Zpairs[i].GetPair()[0], Zpairs[i].GetPair()[1]))
                        {
                            var pair = Zpairs[i];

                            if (currentColliding.Contains(pair) == false)
                            {
                                currentColliding.Add(pair); //ADD PAIR TO CURRENTCOLLIDING
                            }
                        }
                    }
                }
            }
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

            activeList = new List<aaBB>(); //CREATE A NEW LIST OF AABB

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
        }
    }
}