using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidHandler : MonoBehaviour
{
    public GameObject enemyPrefab;

    public List<Slave> alives;
    public List<Enemy> enemies;

    ResourceHandler rh;
    SourceCounter sc;

    public bool isRaidTime;
    public bool isWin;
    int enemyCount;

    void Start()
    {
        rh = GetComponent<ResourceHandler>();
        sc = Camera.main.GetComponent<SourceCounter>();
        StartCoroutine(RaidStarter());
    }

    private void RaidStarted(int difficulty)
    {
        isRaidTime = true;
        alives = new List<Slave>(rh.slaves);
        for (int i = 0; i < difficulty; i++)
        {
            int locX = -250;
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
            yield return new WaitForSeconds(300);// Wait a bit
            if (!isRaidTime)
            {
                enemyCount = sc.raidCount*2 + 1;
                RaidStarted(enemyCount);
            }
        }
    }
}
