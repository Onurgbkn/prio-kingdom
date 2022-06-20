using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StorageData
{
    public int cur;
    public string type;
    public Vector3 position;
}


[System.Serializable]
public class StoragesData
{
    public List<StorageData> storages = new List<StorageData>();
}
