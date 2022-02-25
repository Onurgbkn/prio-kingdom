using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHandler : MonoBehaviour
{
    public List<Resource> resources;
    public List<Storage> storages;

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

    public void UpdateStorages(string type)
    {
        foreach (Resource source in resources)
        {
            if (source.type.ToString() == type)
            {
                source.nearest_storage = GetNearestStorage(source.transform.position, type);
            }
        }
    }
}