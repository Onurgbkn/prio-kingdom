using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ResourceData
{
    public int cur;
    public string type;
    public Vector3 position;
}

[System.Serializable]
public class ResourcesData
{
    public List<ResourceData> resources = new List<ResourceData>();
}