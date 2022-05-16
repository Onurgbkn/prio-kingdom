using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Resource : MonoBehaviour
{
    public enum ResourceType { wood, iron, copper, stone, gold };
    public ResourceType type;

    public int max; // max resource capacity held
    public int cur;

    public int workerCount = 0;
    public int maxWorker;

    public float progress;

    private bool isQuitting;

    ResourceHandler reshand;

    private void Start()
    {
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.resources.Add(this);
        reshand.UpdateJobs();
        reshand.GetJob4Slave();
        GetComponent<SphereCollider>().enabled = true;

    }

    public void AddSource()
    {
        if (cur < max)
        {
            progress += 0.5f;
            if (progress > 1)
            {
                progress--;
                cur++;
            }
            if (cur == max)
            {
                reshand.GetJob4Slave();
            }
        }
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (this.type.ToString() == "wood" && !isQuitting)
        {
            reshand.resources.Remove(this);
            reshand.UpdateJobs();
            //reshand.SpawnTrees();
        }
    }
}
