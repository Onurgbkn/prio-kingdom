using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bildtest : MonoBehaviour
{
    public Material matApr;
    public Material matRej;

    public bool onBuild;


    public List<Transform> colliders;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colliders.Count == 0)
        {
            GetComponent<MeshRenderer>().material = matApr;
        }
        else
        {
            GetComponent<MeshRenderer>().material = matRej;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "Terrain")
        {
            colliders.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name != "Terrain")
        {
            colliders.Remove(other.transform);
        }
    }
}
