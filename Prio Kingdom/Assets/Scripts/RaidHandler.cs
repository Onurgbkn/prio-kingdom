using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaidHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public AudioClip yell;

    public TextMeshProUGUI raidAlert;

    public List<Slave> alives;
    public List<Enemy> enemies;

    ResourceHandler rh;
    SourceCounter sc;
    AudioSource audioSource;

    public bool isRaidTime;
    public bool isWin;
    int enemyCount;

    void Start()
    {
        rh = GetComponent<ResourceHandler>();
        sc = Camera.main.GetComponent<SourceCounter>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(RaidStarter());
        StartCoroutine(AlertCorotineStarter());
    }

    private void RaidStarted(int difficulty)
    {
        audioSource.PlayOneShot(yell, 0.5f);
        isRaidTime = true;
        alives = new List<Slave>(rh.slaves);
        for (int i = 0; i < difficulty; i++)
        {
            int locX = 250;
            int locZ = Random.Range(-250, 250);
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(locX, 0, locZ), Quaternion.identity);
            enemy.GetComponent<Enemy>().power = sc.raidCount * 3 + 20;
            enemy.GetComponent<Enemy>().health = sc.raidCount * 7 + 100;
            enemies.Add(enemy.GetComponent<Enemy>());
        }
    }

    public void RaidEnded()
    {
        if (enemies.Count == 0 && isRaidTime)
        {
            isRaidTime = false;
            if (isWin)
            {
                sc.raidCount += 1;
                isWin = false;
            }
            foreach (Slave slave in rh.slaves)
            {
                slave.Revive();
            }
            rh.GetJob4Slave();
        }
    }

    IEnumerator RaidStarter()
    {
        while (true)
        {
            yield return new WaitForSeconds(240);// Wait a bit
            if (!isRaidTime)
            {
                enemyCount = sc.raidCount*2 + 1;
                RaidStarted(enemyCount);
            }
        }
    }

    IEnumerator AlertCorotineStarter()
    {
        yield return new WaitForSeconds(180);// Wait a bit
        AlertBlinker();
        StartCoroutine(AlertStarter());
    }

    IEnumerator AlertStarter()
    {
        while (true)
        {
            yield return new WaitForSeconds(240);// Wait a bit
            AlertBlinker();
        }
    }

    void AlertBlinker()
    {
        raidAlert.gameObject.SetActive(true);
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        for (int j = 0; j < 7; j++)
        {
            for (int i = 255; i > 100; i-=2)
            {
                yield return new WaitForSeconds(0.01f);// Wait a bit
                raidAlert.color = new Color32(255, 0, 0, (byte)i);
            }
        }
        raidAlert.gameObject.SetActive(false);
    }
}
