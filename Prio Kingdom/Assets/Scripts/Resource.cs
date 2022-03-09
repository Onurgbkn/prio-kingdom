using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum ResourceType { wood, iron, copper };
    public ResourceType type;

    public int max; // max resource capacity held
    public int cur;

    public int workerCount = 0;

    public float progress;

    public Transform nearest_storage;

    private bool isQuitting;

    ResourceHandler reshand;
    private void Awake()
    {
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.resources.Add(this);
    }

    private void Start()
    {
        nearest_storage = reshand.GetNearestStorage(transform.position, type.ToString());
    }

    private void Update()
    {
        if (cur < max)
        {
            progress += workerCount * Time.deltaTime * 0.5f;
            if (progress > 1)
            {
                progress--;
                cur ++;
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
            reshand.SpawnTrees();
        }
    }
}
