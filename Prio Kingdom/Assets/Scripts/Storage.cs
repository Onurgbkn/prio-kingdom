using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public enum ResourceType { wood, iron, copper, stone, gold };
    public ResourceType type;

    public int max;
    public int cur;

    ResourceHandler reshand;
    private void Start()
    {
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.storages.Add(this);
        reshand.UpdateStorages(type.ToString());
        reshand.UpdateJobs();
        reshand.GetJob4Slave();
        GetComponent<BoxCollider>().enabled = true;

    }

    public void AddResource(int amount)
    {
        string subtype;

        if (cur + amount < max)
        {
            cur += amount;
        }
        else
        {
            cur += amount;
            if (cur > max) cur = max;
            reshand.UpdateStorages(type.ToString()); // update nearest storage of source
            reshand.UpdateJobs();
            reshand.GetJob4Slave();
        }

        if (type.ToString() == "wood") subtype = "Log";
        else subtype = "Ore";


        if ((float)cur / (float)max < 0.25f)
        {
            transform.Find(subtype + " 1").gameObject.SetActive(false);
            transform.Find(subtype + " 2").gameObject.SetActive(false);
            transform.Find(subtype + " 3").gameObject.SetActive(false);
        }
        else if ((float)cur / (float)max < 0.50f)
        {
            transform.Find(subtype + " 1").gameObject.SetActive(true);
            transform.Find(subtype + " 2").gameObject.SetActive(false);
            transform.Find(subtype + " 3").gameObject.SetActive(false);
        }
        else if ((float)cur / (float)max < 0.75f)
        {
            transform.Find(subtype + " 1").gameObject.SetActive(true);
            transform.Find(subtype + " 2").gameObject.SetActive(true);
            transform.Find(subtype + " 3").gameObject.SetActive(false);
        }
        else
        {
            transform.Find(subtype + " 1").gameObject.SetActive(true);
            transform.Find(subtype + " 2").gameObject.SetActive(true);
            transform.Find(subtype + " 3").gameObject.SetActive(true);
        }
    }
}
