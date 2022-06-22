using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CamHandler : MonoBehaviour
{
    private Vector3 dragPos;

    private bool onDrag;

    private float camSpeed = 2f;

    public bool isDragable;

    public GameObject selectedSlave;
    public GameObject jobsPanel;

    // Update is called once per frame
    void Update()
    {
        // Drag to move camera
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) // Check if clickted on ui
            {
                selectedSlave = null;
                jobsPanel.SetActive(false);

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
        }

        if (onDrag && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                dragPos = new Vector3(Input.GetTouch(0).deltaPosition.x * camSpeed * (transform.position.y / 20), 0, Input.GetTouch(0).deltaPosition.y * camSpeed * (transform.position.y / 20));
                dragPos = Quaternion.Euler(0, 45, 0) * dragPos * Time.deltaTime;

                bool leftmost = dragPos.x > 0 && transform.position.x - dragPos.x < -500;
                bool rightmost = dragPos.x < 0 && transform.position.x - dragPos.x > 500;
                bool botmost = dragPos.z > 0 && transform.position.z - dragPos.z < -500;
                bool topmost = dragPos.z < 0 && transform.position.z - dragPos.z > 500;

                if (!(leftmost || rightmost || topmost || botmost))
                {
                    transform.position = new Vector3(transform.position.x - dragPos.x, transform.position.y, transform.position.z - dragPos.z);
                }
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

            float difference = (currentMagnitude - prevMagnitude) * 4;

            if ((transform.position.y >80 && difference < 0) || (transform.position.y < 15 && difference > 0))
            {
                difference = 0;
            }

            transform.Translate(new Vector3(0, 0, difference) * Time.deltaTime, Space.Self);
        }

        // Follow slave
        if (selectedSlave != null)
        {
            float t = (Camera.main.transform.position.y - 1.55f) / 1.4f; // for the zoom
            transform.position = new Vector3(selectedSlave.transform.position.x - t, transform.position.y, selectedSlave.transform.position.z - t);
        }
    }
}
