using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public enum ResourceType { wood, iron, copper };
    public ResourceType type;

    public int max;
    public int cur;

    ResourceHandler reshand;
    private void Awake()
    {
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.storages.Add(this);
    }

    public void AddResource(int amount)
    {
        if (cur + amount < max)
        {
            cur += amount;
        }
        else
        {
            cur += amount;
            if (cur < max) cur = max;
            reshand.UpdateStorages(type.ToString()); // update nearest storage of source
            reshand.UpdateJobs();
        }
    }
}
