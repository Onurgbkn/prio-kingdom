using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    public List<Resource> resources;
    public List<Storage> storages;
    public List<Slave> slaves;

    public List<string> available_jobs;




    public GameObject forest;

    private void Start()
    {
        StartCoroutine(SpawnTrees());
        UpdateJobs();
    }

    IEnumerator SpawnTrees() // coroutine
    {
        while (true)
        {
            if (GameObject.Find("Forest").transform.childCount < 20)
            {
                int x = Random.Range(-90, 90);
                int z = Random.Range(-90, 90);
                Vector3 sp = new Vector3(x, 0, z);
                var hitColliders = Physics.OverlapSphere(new Vector3(x, 5, z), 4);
                if (hitColliders.Length > 0)
                {
                    SpawnTrees();
                }
                else
                {
                    ;
                    GameObject tree = Instantiate(forest, sp, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    tree.transform.parent = GameObject.Find("Forest").transform;
                }
            }
            yield return new WaitForSeconds(15f);
        }
    }


    public void UpdateJobs()
    {
        available_jobs.Clear();
        foreach (Storage storage in storages)
        {
            // if there is an available storage
            if (storage.cur < storage.max && !available_jobs.Contains(storage.type.ToString()))
            {
                foreach (Resource source in resources)
                {
                    // also if there is available source
                    if (source.type.ToString() == storage.type.ToString())
                    {
                        available_jobs.Add(source.type.ToString());
                        break;
                    }
                }
            }
        }
    }

    public void GetJob4Slave() // Update current job for slaves
    {
        foreach (Slave slave in slaves)
        {
            slave.GetJob();
        }
    }

    public GameObject FindSource(string type, Vector3 pos)
    {
        GameObject source = null;
        float dist = 9999, temp;
        foreach (Resource s in resources)
        {
            if (s.type.ToString() == type && s.maxWorker > s.workerCount)
            {
                temp = Vector3.Distance(pos, s.transform.position);
                if (temp < dist)
                {
                    source = s.gameObject;
                    dist = temp;
                }
            }
        }
        return source;
    }

    public GameObject FindStorage(string type, Vector3 pos)
    {
        GameObject storage = null;
        float dist = 9999, temp;
        foreach (Storage s in storages)
        {
            if (s.type.ToString() == type && s.max > s.cur)
            {
                temp = Vector3.Distance(pos, s.transform.position);
                if (temp < dist)
                {
                    storage = s.gameObject;
                    dist = temp;
                }
            }
        }
        return storage;
    }

    public void GetSource(string type, int amount)
    {
        foreach (Storage s in storages)
        {
            if (s.type.ToString() == type && s.cur > 0)
            {
                if (s.cur < amount)
                {
                    amount -= s.cur;
                    s.cur = 0;
                    s.AddResource(0);
                }
                else
                {
                    s.cur -= amount;
                    s.AddResource(0);
                    break;
                }
            }
        }
    }
}
