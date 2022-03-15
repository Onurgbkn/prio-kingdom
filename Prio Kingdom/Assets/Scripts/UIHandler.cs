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
    public GameObject minePanel;
    public GameObject storagePanel;

    public Button buttonApr;
    public Button buttonRej;

    public GameObject mine;
    public GameObject logolder;
    public GameObject storage;

    public BuildHandler buildObj;

    public void ShowShopPanel()
    {
        shopanel.SetActive(true);
        Camera.main.GetComponent<CamHandler>().isDragable = false;
    }

    public void HideShopPanel()
    {
        shopanel.SetActive(false);
        Camera.main.GetComponent<CamHandler>().isDragable = true;
    }

    public void ShowMines()
    {
        minePanel.SetActive(true);
        storagePanel.SetActive(false);
    }

    public void ShowStorages()
    {
        minePanel.SetActive(false);
        storagePanel.SetActive(true);
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
        Camera.main.GetComponent<CamHandler>().isDragable = true;
    }

    public void SpawnLogHolder()
    {
        Vector3 sp = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z + 15);
        GameObject createdStorage = Instantiate(logolder, sp, Quaternion.Euler(0, -90, 0));
        shopanel.SetActive(false);
        Camera.main.GetComponent<CamHandler>().isDragable = true;
    }

    public void SpawnIronStorage()
    {
        SpawnStorage(Storage.ResourceType.iron, iron);
    }

    public void SpawnCopperStorage()
    {
        SpawnStorage(Storage.ResourceType.copper, copper);
    }
    public void SpawnStoneStorage()
    {
        SpawnStorage(Storage.ResourceType.stone, stone);
    }
    public void SpawnGoldStorage()
    {
        SpawnStorage(Storage.ResourceType.gold, gold);
    }

    public void SpawnStorage(Storage.ResourceType type, Material mat)
    {
        Vector3 sp = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z + 15);
        GameObject createdStorage = Instantiate(storage, sp, Quaternion.Euler(0, -90, 0));
        createdStorage.transform.Find("Ore 1").GetComponent<MeshRenderer>().material = mat;
        createdStorage.transform.Find("Ore 2").GetComponent<MeshRenderer>().material = mat;
        createdStorage.transform.Find("Ore 3").GetComponent<MeshRenderer>().material = mat;
        createdStorage.GetComponent<Storage>().type = type;
        shopanel.SetActive(false);
        Camera.main.GetComponent<CamHandler>().isDragable = true;
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
