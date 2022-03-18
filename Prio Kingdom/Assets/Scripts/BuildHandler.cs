using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildHandler : MonoBehaviour
{
    public Material matApr;
    public Material matRej;

    private UIHandler uihandler;

    private Button buttonApr;
    private Button buttonRej;


    public bool onBuild;

    private bool onDrag;
    private Vector3 dragPos;

    public string type;

    public List<Transform> colliders;
    void Start()
    {
        uihandler = GameObject.Find("UI").GetComponent<UIHandler>();
        uihandler.buildObj = this;
        buttonApr = uihandler.buttonApr;
        buttonRej = uihandler.buttonRej;
        buttonRej.gameObject.SetActive(true);

        if (transform.parent.GetComponent<Storage>() != null)
        {
            type = transform.parent.GetComponent<Storage>().type.ToString() + " storage";
        }
        else if (transform.parent.GetComponent<Resource>() != null)
        {
            type = transform.parent.GetComponent<Resource>().type.ToString() + " mine";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (colliders.Count == 0)
        {
            GetComponent<MeshRenderer>().material = matApr;
            buttonApr.gameObject.SetActive(true);
        }
        else
        {
            GetComponent<MeshRenderer>().material = matRej;
            buttonApr.gameObject.SetActive(false);
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
