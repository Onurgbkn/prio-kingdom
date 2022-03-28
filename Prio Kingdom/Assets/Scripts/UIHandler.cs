using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public ResourceHandler reshand;

    public Material iron;
    public Material copper;
    public Material gold;
    public Material stone;


    public GameObject shopanel;
    public GameObject minePanel;
    public GameObject storagePanel;
    public GameObject slavePanel;

    public Button buttonApr;
    public Button buttonRej;

    public GameObject mine;
    public GameObject logolder;
    public GameObject storage;
    public GameObject slave;

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
        slavePanel.SetActive(false);
        Camera.main.GetComponent<CamHandler>().isDragable = true;
    }

    public void SpawnLogHolder()
    {
        Vector3 sp = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z + 15);
        GameObject createdStorage = Instantiate(logolder, sp, Quaternion.Euler(0, -90, 0));
        shopanel.SetActive(false);
        slavePanel.SetActive(false);
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
        createdStorage.transform.Find("Woody").GetComponent<MeshRenderer>().material = mat;
        createdStorage.GetComponent<Storage>().type = type;
        shopanel.SetActive(false);
        slavePanel.SetActive(false);
        Camera.main.GetComponent<CamHandler>().isDragable = true;
    }


    public void BuildApproved()
    {
        string sourceType = buildObj.GetComponent<BuildHandler>().type.Split(' ')[0];
        string workType = buildObj.GetComponent<BuildHandler>().type.Split(' ')[1];

        if (workType == "mine") buildObj.transform.parent.GetComponent<Resource>().enabled = true;
        else if (workType == "storage") buildObj.transform.parent.GetComponent<Storage>().enabled = true;
        
        Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
        Destroy(buildObj);

        buttonApr.gameObject.SetActive(false);
        buttonRej.gameObject.SetActive(false);

        slavePanel.SetActive(true);
    }

    public void BuildCanceled()
    {
        Destroy(buildObj.transform.parent.gameObject);
        buttonApr.gameObject.SetActive(false);
        buttonRej.gameObject.SetActive(false);

        slavePanel.SetActive(true);
    }

    public void SlaveSelected(GameObject slave) {
        Camera.main.GetComponent<CamHandler>().selectedSlave = slave;

    }

    public void CreateSlave()
    {
        GameObject createdSlave = Instantiate(slave, new Vector3(100, 0, 100), Quaternion.identity);
        createdSlave.transform.parent = GameObject.Find("Slaves").transform;
    }
}
