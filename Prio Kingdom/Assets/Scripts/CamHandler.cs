using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHandler : MonoBehaviour
{
    private Vector3 dragPos;

    private bool onDrag;

    private float camSpeed = 0.01f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<BuildHandler>() == null)
                {
                    onDrag = true;
                }
            }
        }


        if (onDrag && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                dragPos = new Vector3(Input.GetTouch(0).deltaPosition.x * camSpeed, 0, Input.GetTouch(0).deltaPosition.y * camSpeed);
                dragPos = Quaternion.Euler(0, 45, 0) * dragPos;
                transform.position = new Vector3(transform.position.x - dragPos.x, transform.position.y, transform.position.z - dragPos.z);
            }
        }

        if (onDrag && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled))
        {
            onDrag = false;
        }
    }
}
