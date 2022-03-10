using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bildtest : MonoBehaviour
{
    public Material matApr;
    public Material matRej;

    public bool onBuild;

    private bool onDrag;
    private Vector3 dragPos;
    private float dist;

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


        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "BuiltBox")
                {
                    onDrag = true;
                }
            }

        }

        if (onDrag && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            Plane plane = new Plane(Vector3.up, transform.position);
            float distance = 0;
            if (plane.Raycast(ray, out distance))
            {
                transform.parent.position = ray.GetPoint(distance);
            }
        }

        if (onDrag && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
        {
            onDrag = false;
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
