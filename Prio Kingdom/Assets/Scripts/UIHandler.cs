using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public ResourceHandler reshand;

    public Material iron;
    public Material copper;
    public Material gold;


    public GameObject shopanel;
    public GameObject minePanel;
    public GameObject storagePanel;
    public GameObject addWorkerPanel;
    public GameObject slavePanel;
    public GameObject jobsPanel;
    public GameObject addJobPanel;
    public GameObject upgradePanel;

    public Button buttonApr;
    public Button buttonRej;

    public GameObject jobIcon;


    public GameObject mine;
    public GameObject logolder;
    public GameObject storage;
    public GameObject farm;

    public BuildHandler buildObj;
    public SourceCounter sc;

    public void ShowShopPanel()
    {
        shopanel.SetActive(true);
        Camera.main.GetComponent<CamHandler>().isDragable = false;
        slavePanel.SetActive(false);
        upgradePanel.SetActive(false);
        if (sc.tutoCount == 2 || sc.tutoCount == 6 || sc.tutoCount == 10)
        {
            sc.th.TutoDone();
        }
    }

    public void HideShopPanel()
    {
        if (!(sc.tutoCount == 3 || sc.tutoCount == 7 || sc.tutoCount == 8 || sc.tutoCount == 11 || sc.tutoCount == 12))
        {
            shopanel.SetActive(false);
            Camera.main.GetComponent<CamHandler>().isDragable = true;
            slavePanel.SetActive(true);
        }
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        Camera.main.GetComponent<CamHandler>().isDragable = false;
        slavePanel.SetActive(false);
        shopanel.SetActive(false);
        if (sc.tutoCount == 19)
        {
            sc.th.TutoDone();
        }
    }

    public void HideUpgradePanel()
    {
        upgradePanel.SetActive(false);
        Camera.main.GetComponent<CamHandler>().isDragable = true;
        slavePanel.SetActive(true);
    }

    public void ShowMines()
    {
        minePanel.SetActive(true);
        storagePanel.SetActive(false);
        addWorkerPanel.SetActive(false);
    }

    public void ShowStorages()
    {
        minePanel.SetActive(false);
        storagePanel.SetActive(true);
        addWorkerPanel.SetActive(false);
        if (sc.tutoCount == 7)
        {
            sc.th.TutoDone();
        }
    }

    public void ShowWorker()
    {
        minePanel.SetActive(false);
        storagePanel.SetActive(false);
        addWorkerPanel.SetActive(true);
        if (sc.tutoCount == 11)
        {
            sc.th.TutoDone();
        }
    }

    public void SpawnIronMine()
    {
        if (sc.woodCount >= 10)
        {
            SpawnMine(Resource.ResourceType.iron, iron);
            if (sc.tutoCount == 3)
            {
                sc.th.TutoDone();
            }
        }
    }

    public void SpawnCopperMine()
    {
        if (sc.woodCount >= 20 && sc.ironCount >= 15)
        {
            SpawnMine(Resource.ResourceType.copper, copper);
        }
    }

    public void SpawnGoldMine()
    {
        if (sc.woodCount >= 30 && sc.ironCount >= 30 && sc.copperCount >= 30)
        {
            SpawnMine(Resource.ResourceType.gold, gold);
        }
    }

    public void SpawnMine(Resource.ResourceType type, Material mat)
    {
        float t = (Camera.main.transform.position.y - 1.55f) / 1.4f; // for the zoom
        Vector3 sp = new Vector3(Camera.main.transform.position.x + t, 0, Camera.main.transform.position.z + t);
        GameObject createdMine = Instantiate(mine, sp, Quaternion.Euler(0, -90, 0));
        createdMine.transform.Find("Ore").GetComponent<MeshRenderer>().material = mat;
        createdMine.GetComponent<Resource>().type = type;
        createdMine.GetComponent<Resource>().maxWorker = sc.worker4Mine + 1;
        shopanel.SetActive(false);
        slavePanel.SetActive(false);
        Camera.main.GetComponent<CamHandler>().isDragable = true;

        createdMine.transform.SetParent(GameObject.Find("Mines").transform);
    }

    public void SpawnFarm()
    {
        if (sc.woodCount >= 25)
        {
            float t = (Camera.main.transform.position.y - 1.55f) / 1.4f; // for the zoom
            Vector3 sp = new Vector3(Camera.main.transform.position.x + t, 0, Camera.main.transform.position.z + t);
            GameObject createdFarm = Instantiate(farm, sp, Quaternion.Euler(0, -90, 0));
            createdFarm.GetComponent<Resource>().maxWorker = sc.worker4Farm + 1;
            shopanel.SetActive(false);
            slavePanel.SetActive(false);
            Camera.main.GetComponent<CamHandler>().isDragable = true;

            createdFarm.transform.SetParent(GameObject.Find("Mines").transform);

            if (sc.tutoCount == 3)
            {
                sc.th.TutoDone();
            }
        }
    }

    public void SpawnLogHolder()
    {
        if (sc.woodCount >= 10)
        {
            float t = (Camera.main.transform.position.y - 1.55f) / 1.4f; // for the zoom
            Vector3 sp = new Vector3(Camera.main.transform.position.x + t, 0, Camera.main.transform.position.z + t);
            GameObject createdStorage = Instantiate(logolder, sp, Quaternion.Euler(0, -90, 0));
            createdStorage.GetComponent<Storage>().max = 50 * sc.storageBoost + 100;

            shopanel.SetActive(false);
            slavePanel.SetActive(false);
            Camera.main.GetComponent<CamHandler>().isDragable = true;

            createdStorage.transform.SetParent(GameObject.Find("Storages").transform);
        }
    }

    public void SpawnIronStorage()
    {
        if (sc.woodCount >= 10)
        {
            SpawnStorage(Storage.ResourceType.iron, iron);
            if (sc.tutoCount == 8)
            {
                sc.th.TutoDone();
            }
        }
    }

    public void SpawnCopperStorage()
    {
        if (sc.woodCount >= 20 && sc.ironCount >= 15)
        { 
            SpawnStorage(Storage.ResourceType.copper, copper); 
        }
    }

    public void SpawnGoldStorage()
    {
        if (sc.woodCount >= 30 && sc.ironCount >= 30 && sc.copperCount >= 30)
        { 
            SpawnStorage(Storage.ResourceType.gold, gold); 
        }
    }

    public void SpawnStorage(Storage.ResourceType type, Material mat)
    {
        float t = (Camera.main.transform.position.y - 1.55f) / 1.4f; // for the zoom
        Vector3 sp = new Vector3(Camera.main.transform.position.x + t, 0, Camera.main.transform.position.z + t);
        GameObject createdStorage = Instantiate(storage, sp, Quaternion.Euler(0, -90, 0));
        createdStorage.transform.Find("Ore 1").GetComponent<MeshRenderer>().material = mat;
        createdStorage.transform.Find("Ore 2").GetComponent<MeshRenderer>().material = mat;
        createdStorage.transform.Find("Ore 3").GetComponent<MeshRenderer>().material = mat;
        createdStorage.transform.Find("Woody").GetComponent<MeshRenderer>().material = mat;
        createdStorage.GetComponent<Storage>().type = type;
        createdStorage.GetComponent<Storage>().max = 50 * sc.storageBoost + 100;
        shopanel.SetActive(false);
        slavePanel.SetActive(false);
        Camera.main.GetComponent<CamHandler>().isDragable = true;

        createdStorage.transform.SetParent(GameObject.Find("Storages").transform);
    }


    public void BuildApproved()
    {
        if (sc.tutoCount == 4) return;
        string sourceType = buildObj.GetComponent<BuildHandler>().type.Split(' ')[0];
        string workType = buildObj.GetComponent<BuildHandler>().type.Split(' ')[1];
        
        if (workType == "mine") buildObj.transform.parent.GetComponent<Resource>().enabled = true;
        else if (workType == "storage") buildObj.transform.parent.GetComponent<Storage>().enabled = true;

        Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
        Destroy(buildObj);

        buttonApr.gameObject.SetActive(false);
        buttonRej.gameObject.SetActive(false);

        slavePanel.SetActive(true);

        if (sc.tutoCount == 5 || sc.tutoCount == 9)
        {
            sc.th.TutoDone();
        }
    }

    public void BuildCanceled()
    {
        if (sc.tutoCount == 4) return;
        Destroy(buildObj.transform.parent.gameObject);
        buttonApr.gameObject.SetActive(false);
        buttonRej.gameObject.SetActive(false);

        slavePanel.SetActive(true);
    }

    public void SlaveSelected(GameObject slave) {
        if (sc.tutoCount == 17)
        {
            sc.th.TutoDone();
        }
        jobsPanel.SetActive(true);
        Camera.main.GetComponent<CamHandler>().selectedSlave = slave;
        Camera.main.GetComponent<CamHandler>().isDragable = true;

        foreach (Transform child in jobsPanel.transform.Find("Cont"))
        {
            Destroy(child.gameObject);
        }

        foreach (string job in slave.GetComponent<Slave>().jobs)
        {
            GameObject jobItem = Instantiate(jobIcon);
            jobItem.transform.SetParent(jobsPanel.transform.Find("Cont"));
            jobItem.transform.localScale = new Vector3(1, 1, 1);

            jobItem.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = job;
            jobItem.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { RemoveJob(job, slave); });
        }
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
            if (sc.tutoCount == 16)
            {
                sc.th.TutoDone();
            }
        }
    }

    public void ShowAddJobPanel()
    {
        addJobPanel.SetActive(true);
        slavePanel.SetActive(false);
        shopanel.SetActive(false);
        upgradePanel.SetActive(false);
        if (sc.tutoCount == 13)
        {
            sc.th.TutoDone();
        }
    }
    public void HideAddJobPanel()
    {
        if (sc.tutoCount != 14)
        {
            addJobPanel.SetActive(false);
            if (sc.tutoCount == 15)
            {
                sc.th.TutoDone();
            }
        }
    }

    public void JobIronMine()
    {
        AddJob("iron");
        if (sc.tutoCount == 14)
        {
            sc.th.TutoDone();
        }
    }

    public void JobCopperMine()
    {
        AddJob("copper");
    }
    public void JobGoldMine()
    {
        AddJob("gold");
    }

    public void JobFellTree()
    {
        AddJob("wood");
    }

    public void JobFarming()
    {
        AddJob("food");
    }


    private void AddJob(string job)
    {
        GameObject slave = Camera.main.GetComponent<CamHandler>().selectedSlave;
        if (slave && !slave.GetComponent<Slave>().jobs.Contains(job))
        {
            slave.GetComponent<Slave>().jobs.Add(job);
            SlaveSelected(slave); // Not sure we need this
            slave.GetComponent<Slave>().GetJob();
        }
    }

    private void RemoveJob(string job, GameObject slave)
    {
        slave.GetComponent<Slave>().jobs.Remove(job);
        SlaveSelected(slave);
        slave.GetComponent<Slave>().GetJob();
    }
}
