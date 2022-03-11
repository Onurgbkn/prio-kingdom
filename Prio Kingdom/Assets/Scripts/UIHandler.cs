using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{


    public GameObject shopanel;

    public Button buttonApr;
    public Button buttonRej;

    public GameObject ironmine;

    public Bildtest buildObj;

    public void ShowShopPanel()
    {
        shopanel.SetActive(true);
    }

    public void HideShopPanel()
    {
        shopanel.SetActive(false);
    }

    public void SpawnIronMine()
    {
        Vector3 sp = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z + 15);
        Instantiate(ironmine, sp, Quaternion.identity);
        shopanel.SetActive(false);
    }


    public void BuildApproved()
    {
        Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
        Destroy(buildObj);
        buttonApr.gameObject.SetActive(false);
        buttonRej.gameObject.SetActive(false);
    }

    public void BuildCanceled()
    {
        Destroy(buildObj.transform.parent.gameObject);
        buttonApr.gameObject.SetActive(false);
        buttonRej.gameObject.SetActive(false);
    }

}
