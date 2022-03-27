using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamHandler : MonoBehaviour
{
    private Vector3 dragPos;

    private bool onDrag;

    private float camSpeed = 2.5f;

    public bool isDragable;

    public GameObject selectedSlave;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Drag to move camera
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            selectedSlave = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<BuildHandler>() == null && isDragable)
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
                dragPos = Quaternion.Euler(0, 45, 0) * dragPos * Time.deltaTime;
                transform.position = new Vector3(transform.position.x - dragPos.x, transform.position.y, transform.position.z - dragPos.z);
            }
        }

        if (onDrag && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) || Input.touchCount != 1)
        {
            onDrag = false;
        }


        // Pitch to zoom
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = (currentMagnitude - prevMagnitude) * 2;

            if ((transform.position.y >60 && difference < 0) || (transform.position.y < 15 && difference > 0))
            {
                difference = 0;
            }

            transform.Translate(new Vector3(0, 0, difference) * Time.deltaTime, Space.Self);
        }

        // Follow slave
        if (selectedSlave != null)
        {
            transform.position = new Vector3(selectedSlave.transform.position.x -10, transform.position.y, selectedSlave.transform.position.z -10);
        }
    }
}
