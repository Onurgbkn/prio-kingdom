using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIKole : MonoBehaviour
{

    public GameObject worker;
    public Button workerIcon;
    public GameObject workerPanel;
    public GameObject shopPanel;
    public TMP_InputField wname;
    public UIHandler uihand;

    SourceCounter sc;

    void Start()
    {
        sc = Camera.main.GetComponent<SourceCounter>();
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void ChangeColor()
    {
        transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void CreateSlave()
    {
        if (sc.foodCount >= 25)
        {
            sc.GetFood(25);

            GameObject createdSlave = Instantiate(worker, new Vector3(100, 0, 100), Quaternion.identity);
            createdSlave.transform.parent = GameObject.Find("Slaves").transform;

            createdSlave.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material.color = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material.color;
            createdSlave.transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material.color = transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material.color;
            createdSlave.transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material.color = transform.GetChild(3).GetComponent<SkinnedMeshRenderer>().material.color;
            createdSlave.transform.GetChild(5).GetComponent<SkinnedMeshRenderer>().material.color = transform.GetChild(4).GetComponent<SkinnedMeshRenderer>().material.color;

            ChangeColor();

            string tempName = wname.text;
            if (tempName == "")
            {
                tempName = "Worker";
            }
            else if (tempName.Length > 10)
            {
                tempName = tempName.Substring(0, 10);
            }
            createdSlave.GetComponent<Slave>().wname = tempName;
            createdSlave.GetComponent<Slave>().maxHealth = sc.healthBoost * 10 + 100;
            createdSlave.GetComponent<Slave>().health = sc.healthBoost * 10 + 100;
            createdSlave.GetComponent<Slave>().power = sc.powerBoost * 5 + 10;
            wname.text = "";

            Button iconObj = Instantiate(workerIcon);
            iconObj.GetComponentInChildren<TextMeshProUGUI>().text = tempName;
            iconObj.transform.SetParent(workerPanel.transform.Find("Cont"));
            iconObj.transform.localScale = new Vector3(1, 1, 1);
            iconObj.onClick.AddListener(delegate { uihand.SlaveSelected(createdSlave); });

            uihand.SlaveSelected(createdSlave);
            shopPanel.SetActive(false);
        }
    }
}
