using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    public Material iron;
    public Material copper;
    public Material gold;
    public Material stone;


    public GameObject shopanel;

    public Button buttonApr;
    public Button buttonRej;

    public GameObject mine;

    public BuildHandler buildObj;

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
        SpawnMine(Resource.ResourceType.iron, iron);
    }

    public void SpawnCopperMine()
    {
        SpawnMine(Resource.ResourceType.copper, copper);
    }

    public void SpawnStoneMine()
    {
        SpawnMine(Resource.ResourceType.stone, stone);
    }

    public void SpawnGoldMine()
    {
        SpawnMine(Resource.ResourceType.gold, gold);
    }

    public void SpawnMine(Resource.ResourceType type, Material mat)
    {
        Vector3 sp = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z + 15);
        GameObject createdMine = Instantiate(mine, sp, Quaternion.Euler(0, -90, 0));
        createdMine.transform.Find("Ore").GetComponent<MeshRenderer>().material = mat;
        createdMine.GetComponent<Resource>().type = type;
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
