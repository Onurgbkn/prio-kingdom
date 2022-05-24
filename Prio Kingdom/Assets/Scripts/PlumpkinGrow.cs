using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlumpkinGrow : MonoBehaviour
{
    int growRate;
    Resource res;
    ResourceHandler reshand;
    private void Start()
    {
        res = GetComponent<Resource>();
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        Begin2Grow();
    }

    IEnumerator GrowPumpkin()
    {
        for (growRate = 1; growRate < 4; growRate++)
        {
            yield return new WaitForSeconds(20f);
            Grow(growRate);
        }
        res.type = Resource.ResourceType.food;
        reshand.UpdateJobs();
        reshand.GetJob4Slave();
    }

    public void Grow(int rate)
    {
        if (rate == 0)
        {
            transform.Find("Plants 1").gameObject.SetActive(false);
            transform.Find("Plants 2").gameObject.SetActive(false);
            transform.Find("Plants 3").gameObject.SetActive(false);
        }
        else if (rate == 1)
        {
            transform.Find("Plants 1").gameObject.SetActive(true);
        }
        else if (rate == 2)
        {
            transform.Find("Plants 1").gameObject.SetActive(false);
            transform.Find("Plants 2").gameObject.SetActive(true);
        }
        else if (rate == 3)
        {
            transform.Find("Plants 3").gameObject.SetActive(true);
        }
    }

    public void Begin2Grow()
    {
        growRate = 0;
        res.cur = 0;
        res.type = Resource.ResourceType.grow;
        Grow(0);
        StartCoroutine(GrowPumpkin());
        reshand.UpdateJobs();
    }
}
