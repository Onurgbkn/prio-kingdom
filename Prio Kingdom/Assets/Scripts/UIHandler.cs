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
    public GameObject jobsPanel;
    public GameObject addJobPanel;

    public Button buttonApr;
    public Button buttonRej;

    public GameObject jobIcon;
    public Button slaveIcon;


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
        jobsPanel.SetActive(true);
        Camera.main.GetComponent<CamHandler>().selectedSlave = slave;

        foreach (Transform child in jobsPanel.transform.Find("Cont"))
        {
            Destroy(child.gameObject);
        }

        foreach (string job in slave.GetComponent<Slave>().jobs)
        {
            GameObject jobItem = Instantiate(jobIcon);
            jobItem.transform.SetParent(jobsPanel.transform.Find("Cont"));
            jobItem.transform.localScale = new Vector3(1, 1, 1);

            jobItem.transform.GetChild(0).GetComponentInChildren<Text>().text = job;
            jobItem.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { RemoveJob(job, slave); });
        }
    }

    public void CreateSlave()
    {
        GameObject createdSlave = Instantiate(slave, new Vector3(100, 0, 100), Quaternion.identity);
        createdSlave.transform.parent = GameObject.Find("Slaves").transform;

        Button iconObj = Instantiate(slaveIcon);
        iconObj.transform.SetParent(slavePanel.transform.Find("Cont"));
        iconObj.transform.localScale = new Vector3(1, 1, 1);
        iconObj.onClick.AddListener(delegate { SlaveSelected(createdSlave); });
    }

    public void ToggleSlavePanel()
    {
        if (slavePanel.activeSelf)
        {
            slavePanel.SetActive(false);
        }
        else
        {
            slavePanel.SetActive(true);
        }
    }

    public void ShowAddJobPanel()
    {
        addJobPanel.SetActive(true);
    }
    public void HideAddJobPanel()
    {
        addJobPanel.SetActive(false);
    }

    public void JobIronMine()
    {
        AddJob("iron");
    }

    public void JobCopperMine()
    {
        AddJob("copper");
    }


    private void AddJob(string job)
    {
        GameObject slave = Camera.main.GetComponent<CamHandler>().selectedSlave;
        if (slave && !slave.GetComponent<Slave>().jobs.Contains(job))
        {
            slave.GetComponent<Slave>().jobs.Add(job);
            SlaveSelected(slave);
        }
    }

    private void RemoveJob(string job, GameObject slave)
    {
        slave.GetComponent<Slave>().jobs.Remove(job);
        SlaveSelected(slave);
    }
}
