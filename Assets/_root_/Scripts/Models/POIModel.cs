using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class POIModel 
{
    public string displayName;
    public Feature[] features;
}


[Serializable]
public class Feature
{
    public Geometry geometry;
}

[Serializable]
public class Geometry
{
    public double x;
    public double y;
}

[Serializable]
public class AreaOfInterest
{
    public double xMin;
    public double yMin;
    public double xMax;
    public double yMax;
}

[Serializable]
public class UserPosition
{
    public double x;
    public double y;
}