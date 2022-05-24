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
    SourceCounter sc;
    private void Start()
    {
        sc = Camera.main.GetComponent<SourceCounter>();
        reshand = GameObject.Find("GameHandler").GetComponent<ResourceHandler>();
        reshand.storages.Add(this);
        reshand.UpdateJobs();
        reshand.GetJob4Slave();
        if (type != ResourceType.food) GetComponent<BoxCollider>().enabled = true;

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
        else if (type.ToString() == "wood")
        {
            reshand.GetSource("wood", 10);
            sc.GetWood(10);
        }
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
