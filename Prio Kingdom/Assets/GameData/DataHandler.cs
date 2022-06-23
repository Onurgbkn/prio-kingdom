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
    public UIUpdate updateU;

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
        if (!Directory.Exists(Application.persistentDataPath + "/save"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/save");
        }
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
        SaveStorageData();
    }

    public void LoadGame()
    {
        LoadGameData();
        LoadWorkerData();
        LoadResourceData();
        LoadStorageData();
    }

    private void OnApplicationQuit()
    {
        SaveGame(); 
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveGame();
        }
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
        File.WriteAllText(Application.persistentDataPath + "/save/gamedata.json", json);
    }

    void LoadGameData()
    {
        try
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/save/gamedata.json");
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
            updateU.UpdateReqUI();
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
        File.WriteAllText(Application.persistentDataPath + "/save/workerdata.json", json);
    }

    void LoadWorkerData()
    {
        try
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/save/workerdata.json");
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
        File.WriteAllText(Application.persistentDataPath + "/save/resourcesdata.json", json);
    }

    void LoadResourceData()
    {
        try
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/save/resourcesdata.json");
            ResourcesData resourcesData = JsonUtility.FromJson<ResourcesData>(json);
            foreach (ResourceData rd in resourcesData.resources)
            {
                Vector3 sp = new Vector3(rd.position.x, 0, rd.position.z);
                if (rd.type.ToString() == "iron")
                {
                    GameObject createdSource = Instantiate(mine, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Resource>().cur = rd.cur;
                    createdSource.GetComponent<Resource>().maxWorker = sc.worker4Mine + 1;
                    createdSource.GetComponent<Resource>().type = Resource.ResourceType.iron;
                    createdSource.transform.Find("Ore").GetComponent<MeshRenderer>().material = iron;
                    createdSource.transform.SetParent(GameObject.Find("Mines").transform);

                    createdSource.GetComponent<Resource>().isFree = true;
                    createdSource.GetComponent<Resource>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "copper")
                {
                    GameObject createdSource = Instantiate(mine, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Resource>().cur = rd.cur;
                    createdSource.GetComponent<Resource>().maxWorker = sc.worker4Mine + 1;
                    createdSource.GetComponent<Resource>().type = Resource.ResourceType.copper;
                    createdSource.transform.Find("Ore").GetComponent<MeshRenderer>().material = copper;
                    createdSource.transform.SetParent(GameObject.Find("Mines").transform);
                    createdSource.GetComponent<Resource>().isFree = true;
                    createdSource.GetComponent<Resource>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "gold")
                {
                    GameObject createdSource = Instantiate(mine, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Resource>().cur = rd.cur;
                    createdSource.GetComponent<Resource>().maxWorker = sc.worker4Mine + 1;
                    createdSource.GetComponent<Resource>().type = Resource.ResourceType.gold;
                    createdSource.transform.Find("Ore").GetComponent<MeshRenderer>().material = gold;
                    createdSource.transform.SetParent(GameObject.Find("Mines").transform);
                    createdSource.GetComponent<Resource>().isFree = true;
                    createdSource.GetComponent<Resource>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "grow")
                {
                    GameObject createdSource = Instantiate(farm, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Resource>().cur = rd.cur;
                    createdSource.GetComponent<Resource>().maxWorker = sc.worker4Mine + 1;
                    createdSource.GetComponent<Resource>().type = Resource.ResourceType.grow;
                    createdSource.transform.SetParent(GameObject.Find("Mines").transform);
                    createdSource.GetComponent<Resource>().isFree = true;
                    createdSource.GetComponent<Resource>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
            }
        }
        catch (System.Exception) { }
    }

    void SaveStorageData()
    {
        StoragesData storages = new StoragesData();
        foreach (Storage storage in rh.storages)
        {
            StorageData storageData = new StorageData();
            storageData.cur = storage.cur;
            storageData.type = storage.type.ToString();
            storageData.position = storage.transform.position;
            storages.storages.Add(storageData);
        }

        string json = JsonUtility.ToJson(storages, true);
        File.WriteAllText(Application.persistentDataPath + "/save/storagesdata.json", json);
    }

    void LoadStorageData()
    {
        try
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/save/storagesdata.json");
            StoragesData storagesData = JsonUtility.FromJson<StoragesData>(json);
            foreach (StorageData rd in storagesData.storages)
            {
                Vector3 sp = new Vector3(rd.position.x, 0, rd.position.z);
                if (rd.type.ToString() == "iron")
                {
                    GameObject createdSource = Instantiate(storage, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Storage>().type = Storage.ResourceType.iron;
                    createdSource.GetComponent<Storage>().max = 50 * sc.storageBoost + 100;
                    createdSource.transform.Find("Ore 1").GetComponent<MeshRenderer>().material = iron;
                    createdSource.transform.Find("Ore 2").GetComponent<MeshRenderer>().material = iron;
                    createdSource.transform.Find("Ore 3").GetComponent<MeshRenderer>().material = iron;
                    createdSource.transform.Find("Woody").GetComponent<MeshRenderer>().material = iron;
                    createdSource.transform.SetParent(GameObject.Find("Storages").transform);
                    createdSource.GetComponent<Storage>().isFree = true;
                    createdSource.GetComponent<Storage>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "copper")
                {
                    GameObject createdSource = Instantiate(storage, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Storage>().cur = rd.cur;
                    createdSource.GetComponent<Storage>().type = Storage.ResourceType.copper;
                    createdSource.GetComponent<Storage>().max = 50 * sc.storageBoost + 100;
                    createdSource.transform.Find("Ore 1").GetComponent<MeshRenderer>().material = copper;
                    createdSource.transform.Find("Ore 2").GetComponent<MeshRenderer>().material = copper;
                    createdSource.transform.Find("Ore 3").GetComponent<MeshRenderer>().material = copper;
                    createdSource.transform.Find("Woody").GetComponent<MeshRenderer>().material = copper;
                    createdSource.transform.SetParent(GameObject.Find("Storages").transform);
                    createdSource.GetComponent<Storage>().isFree = true;
                    createdSource.GetComponent<Storage>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "gold")
                {
                    GameObject createdSource = Instantiate(storage, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Storage>().cur = rd.cur;
                    createdSource.GetComponent<Storage>().type = Storage.ResourceType.gold;
                    createdSource.GetComponent<Storage>().max = 50 * sc.storageBoost + 100;
                    createdSource.transform.Find("Ore 1").GetComponent<MeshRenderer>().material = gold;
                    createdSource.transform.Find("Ore 2").GetComponent<MeshRenderer>().material = gold;
                    createdSource.transform.Find("Ore 3").GetComponent<MeshRenderer>().material = gold;
                    createdSource.transform.Find("Woody").GetComponent<MeshRenderer>().material = gold;
                    createdSource.transform.SetParent(GameObject.Find("Storages").transform);
                    createdSource.GetComponent<Storage>().isFree = true;
                    createdSource.GetComponent<Storage>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
                else if (rd.type.ToString() == "wood")
                {
                    GameObject createdSource = Instantiate(logHolder, sp, Quaternion.Euler(0, -90, 0));
                    createdSource.GetComponent<Storage>().cur = rd.cur;
                    createdSource.GetComponent<Storage>().max = 50 * sc.storageBoost + 100;
                    createdSource.GetComponent<Storage>().type = Storage.ResourceType.wood;
                    createdSource.transform.SetParent(GameObject.Find("Storages").transform);
                    createdSource.GetComponent<Storage>().isFree = true;
                    createdSource.GetComponent<Storage>().enabled = true;

                    BuildHandler buildObj = createdSource.transform.Find("BuiltBox").GetComponent<BuildHandler>();
                    Destroy(buildObj.gameObject.GetComponent<MeshRenderer>());
                    Destroy(buildObj);
                }
            }
        }
        catch (System.Exception) { }
    }
}
