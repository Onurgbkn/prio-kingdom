using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Resource : MonoBehaviour
{
    public enum ResourceType { grow, wood, iron, copper, stone, gold, food };
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
        if (type.ToString() == "grow")
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<SphereCollider>().enabled = true;
        }
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
                if (type.ToString() == "food")
                {
                    GetComponent<PlumpkinGrow>().Begin2Grow();
                    workerCount = 0;
                    Camera.main.GetComponent<SourceCounter>().AddFood(9);
                }
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
        }
    }
}
