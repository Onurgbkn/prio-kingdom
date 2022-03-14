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
    private void Awake()
    {
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.storages.Add(this);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            AddResource(1);
        }
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
            if (cur > max) cur = max;
            reshand.UpdateStorages(type.ToString()); // update nearest storage of source
            reshand.UpdateJobs();
        }

        if (type.ToString() == "wood")
        {
            if ((float)cur / (float)max < 0.25f)
            {
                transform.Find("Log 1").gameObject.SetActive(false);
                transform.Find("Log 2").gameObject.SetActive(false);
                transform.Find("Log 3").gameObject.SetActive(false);
            }
            else if ((float)cur / (float)max < 0.50f)
            {
                transform.Find("Log 1").gameObject.SetActive(true);
                transform.Find("Log 2").gameObject.SetActive(false);
                transform.Find("Log 3").gameObject.SetActive(false);
            }
            else if ((float)cur / (float)max < 0.75f)
            {
                transform.Find("Log 1").gameObject.SetActive(true);
                transform.Find("Log 2").gameObject.SetActive(true);
                transform.Find("Log 3").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Log 1").gameObject.SetActive(true);
                transform.Find("Log 2").gameObject.SetActive(true);
                transform.Find("Log 3").gameObject.SetActive(true);
            }
        }
    }
}
