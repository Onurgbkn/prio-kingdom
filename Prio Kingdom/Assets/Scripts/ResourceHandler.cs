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
            if (GameObject.Find("Forest").transform.childCount < 10)
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
            yield return new WaitForSeconds(30f);
        }
    }

    public List<Storage> GetStorages(string type)
    {
        List<Storage> restoraces = new List<Storage>();
        foreach (Storage storage in storages)
        {
            if (storage.type.ToString() == type)
            {
                restoraces.Add(storage);
            }
        }

        return restoraces;
    }

    public List<Resource> GetSources(string type)
    {
        List<Resource> reresources = new List<Resource>();
        foreach (Resource resource in resources)
        {
            if (resource.type.ToString() == type)
            {
                reresources.Add(resource);
            }
        }

        return reresources;
    }

    public Transform GetNearestStorage(Vector3 position, string type) // position is source
    {
        Transform nearest = null;
        float dist = 9999;
        foreach (Storage storage in storages)
        {
            if (storage.type.ToString() == type) // if storage is copper, iron etc.
            {
                if (storage.cur < storage.max) // if storage isn't full
                {
                    float tempDist = Vector3.Distance(position, storage.transform.position);
                    if (tempDist < dist)
                    {
                        nearest = storage.transform;
                        dist = tempDist;
                    }
                }
            }
        }
        return nearest;
    }

    public void UpdateStorages(string type) // updates nearest storage of source
    {
        foreach (Resource source in resources)
        {
            if (source.type.ToString() == type)
            {
                source.nearest_storage = GetNearestStorage(source.transform.position, type);
            }
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

}
