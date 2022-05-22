using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Storage : MonoBehaviour
{
    public enum ResourceType { wood, iron, copper, stone, gold, food };
    public ResourceType type;

    public int max;
    public int cur;

    ResourceHandler reshand;
    private void Start()
    {
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.storages.Add(this);
        reshand.UpdateJobs();
        reshand.GetJob4Slave();
        if (type != ResourceType.food) GetComponent<BoxCollider>().enabled = true;
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
            if (cur > max)
            {
                amount = max - cur;
                cur = max;
                
            }
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


        if (type.ToString() == "wood")
        {
            Camera.main.GetComponent<SourceCounter>().AddWood(amount);
        }
        else if (type.ToString() == "iron")
        {
            Camera.main.GetComponent<SourceCounter>().AddIron(amount);
        }
        else if (type.ToString() == "copper")
        {
            Camera.main.GetComponent<SourceCounter>().AddCopper(amount);
        }
        else if (type.ToString() == "gold")
        {
            Camera.main.GetComponent<SourceCounter>().AddGold(amount);
        }
    }
}
