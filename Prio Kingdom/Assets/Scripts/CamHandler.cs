using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHandler : MonoBehaviour
{
    private Vector3 dragPos;

    private bool onDrag;

    private float camSpeed = 2.5f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
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
                dragPos = Quaternion.Euler(0, 45, 0) * dragPos * Time.deltaTime;
                transform.position = new Vector3(transform.position.x - dragPos.x, transform.position.y, transform.position.z - dragPos.z);
            }
        }

        if (onDrag && (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled) || Input.touchCount != 1)
        {
            onDrag = false;
        }


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
    }
}