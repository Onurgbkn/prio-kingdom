using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidHandler : MonoBehaviour
{
    public GameObject enemyPrefab;

    public List<Slave> alives;
    public List<Enemy> enemies;

    ResourceHandler rh;

    public bool isRaidTime;

    void Start()
    {
        rh = GetComponent<ResourceHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            RaidStarted();
        }
    }

    private void RaidStarted()
    {
        isRaidTime = true;
        alives = new List<Slave>(rh.slaves);
        for (int i = 0; i < 1; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(i*-10+200, 0, i*-10+200), Quaternion.identity);
            enemies.Add(enemy.GetComponent<Enemy>());
        }
    }
}
