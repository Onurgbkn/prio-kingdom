using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataHandler : MonoBehaviour
{
    public SourceCounter sc;
    public ResourceHandler rh;

    public GameObject slave;
    public GameObject mine;
    public GameObject storage;
    public GameObject farm;
    public GameObject logHolder;

    public Button workerIcon;
    public GameObject workerPanel;
    public UIHandler uihand;

    public Material iron;
    public Material copper;
    public Material gold;

    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        
    }

    public void SaveGame()
    {
        SaveGameData();
        SaveWorkerData();
        SaveResourceData();
    }

    public void LoadGame()
    {
        LoadGameData();
        LoadWorkerData();
        LoadResourceData();
    }

    private void OnApplicationQuit()
    {
        SaveGame();   
    }

    void SaveGameData()
    {
        GameData gameData = new GameData();
        gameData.food = sc.foodCount;
        gameData.wood = sc.woodCount;
        gameData.iron = sc.ironCount;
        gameData.copper = sc.copperCount;
        gameData.gold = sc.goldCount;

        gameData.nextWorkerCost = sc.nextWorkerCost;
        gameData.worker4Mine = sc.worker4Mine;
        gameData.worker4Farm = sc.worker4Farm;
        gameData.storageBoost = sc.storageBoost;
        gameData.treeGrowrate = sc.treeGrowrate;
        gameData.foodGrowrate = sc.foodGrowrate;

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(Application.dataPath + "/gamedata.json", json);
    }

    void LoadGameData()
    {
        try
        {
            string json = File.ReadAllText(Application.dataPath + "/gamedata.json");
            GameData data = JsonUtility.FromJson<GameData>(json);
            sc.foodCount = data.food;
            sc.woodCount = data.wood;
            sc.ironCount = data.iron;
            sc.copperCount = data.copper;
            sc.goldCount = data.gold;

            sc.nextWorkerCost = data.nextWorkerCost;
            sc.worker4Mine = data.worker4Mine;
            sc.worker4Farm = data.worker4Farm;
            sc.storageBoost = data.storageBoost;
            sc.treeGrowrate = data.treeGrowrate;
            sc.foodGrowrate = data.foodGrowrate;

            sc.UpdateUI();
        }
        catch (System.Exception)
        {
            NewGame();
        }
    }

    void SaveWorkerData()
    {
        WorkersData workers = new WorkersData();
        foreach (Slave worker in rh.slaves)
        {
            WorkerData workerData = new WorkerData();
            workerData.wname = worker.wname;
            workerData.jobs = worker.jobs;
            workers.workers.Add(workerData);

        }
   
        string json = JsonUtility.ToJson(workers, true);
        File.WriteAllText(Application.dataPath + "/workerdata.json", json);
    }

    void LoadWorkerData()
    {
        try
        {
            string json = File.ReadAllText(Application.dataPath + "/workerdata.json");
            WorkersData workersData = JsonUtility.FromJson<WorkersData>(json);
            foreach (WorkerData workerData in workersData.workers)
            {
                GameObject createdSlave = Instantiate(slave, new Vector3(100, 0, 100), Quaternion.identity);
                createdSlave.transform.parent = GameObject.Find("Slaves").transform;

                createdSlave.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                createdSlave.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                createdSlave.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                createdSlave.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                
                createdSlave.GetComponent<Slave>().wname = workerData.wname;
                createdSlave.GetComponent<Slave>().jobs = workerData.jobs;

                Button iconObj = Instantiate(workerIcon);
                iconObj.GetComponentInChildren<TextMeshProUGUI>().text = workerData.wname;
                iconObj.transform.SetParent(workerPanel.transform.Find("Cont"));
                iconObj.transform.localScale = new Vector3(1, 1, 1);
                iconObj.onClick.AddListener(delegate { uihand.SlaveSelected(createdSlave); });
            }
        }catch (System.Exception) { }
    }

    void SaveResourceData()
    {
        ResourcesData resources = new ResourcesData();
        foreach (Resource resource in rh.resources)
        {
            ResourceData resourceData = new ResourceData();
            resourceData.cur = resource.cur;
            resourceData.type = resource.type.ToString();
            resourceData.position = resource.transform.position;
            resources.resources.Add(resourceData);
        }

        string json = JsonUtility.ToJson(resources, true);
        File.WriteAllText(Application.dataPath + "/resourcesdata.json", json);
    }

    void LoadResourceData()
    {
        try
        {
            string json = File.ReadAllText(Application.dataPath + "/resourcesdata.json");
            ResourcesData resourcesData = JsonUtility.FromJson<ResourcesData>(json);
            foreach (ResourceData rd in resourcesData.resources)
            {
                Vector3 sp = new Vector3(rd.position.x, 0, rd.position.z);
                if (rd.type.ToString() == "iron")
                {
                    GameObject createdSource = Instantiate(mine, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Resource>().cur = rd.cur;
                    createdSource.GetComponent<Resource>().type = Resource.ResourceType.iron;
                    createdSource.transform.Find("Ore").GetComponent<MeshRenderer>().material = iron;
                    createdSource.transform.SetParent(GameObject.Find("Mines").transform);

                    createdSource.GetComponent<Resource>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "copper")
                {
                    GameObject createdSource = Instantiate(mine, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Resource>().cur = rd.cur;
                    createdSource.GetComponent<Resource>().type = Resource.ResourceType.copper;
                    createdSource.transform.Find("Ore").GetComponent<MeshRenderer>().material = copper;
                    createdSource.transform.SetParent(GameObject.Find("Mines").transform);

                    createdSource.GetComponent<Resource>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "gold")
                {
                    GameObject createdSource = Instantiate(mine, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Resource>().cur = rd.cur;
                    createdSource.GetComponent<Resource>().type = Resource.ResourceType.gold;
                    createdSource.transform.Find("Ore").GetComponent<MeshRenderer>().material = gold;
                    createdSource.transform.SetParent(GameObject.Find("Mines").transform);

                    createdSource.GetComponent<Resource>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "grow")
                {
                    GameObject createdSource = Instantiate(farm, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Resource>().cur = rd.cur;
                    createdSource.GetComponent<Resource>().type = Resource.ResourceType.grow;
                    createdSource.transform.SetParent(GameObject.Find("Mines").transform);

                    createdSource.GetComponent<Resource>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
            }
        }
        catch (System.Exception) { }
    }
}
