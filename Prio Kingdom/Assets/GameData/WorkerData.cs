using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WorkerData
{
    public string wname;
    public List<string> jobs;
}


[System.Serializable]
public class WorkersData
{
    public List<WorkerData> workers = new List<WorkerData>();
}


