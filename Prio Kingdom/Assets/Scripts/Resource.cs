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
    SourceCounter sc;

    private void Start()
    {
        sc = Camera.main.GetComponent<SourceCounter>();
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

        if (type.ToString() == "iron")
        {
            reshand.GetSource("wood", 10);
            sc.GetWood(10);
        }
        else if (type.ToString() == "copper")
        {
            reshand.GetSource("wood", 20);
            reshand.GetSource("iron", 15);
            sc.GetWood(20);
            sc.GetIron(15);
        }
        else if (type.ToString() == "gold")
        {
            reshand.GetSource("wood", 30);
            reshand.GetSource("iron", 30);
            reshand.GetSource("copper", 30);
            sc.GetWood(30);
            sc.GetIron(30);
            sc.GetCopper(30);
        }
        else if (type.ToString() == "grow")
        {
            reshand.GetSource("wood", 50);
            sc.GetWood(50);
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
