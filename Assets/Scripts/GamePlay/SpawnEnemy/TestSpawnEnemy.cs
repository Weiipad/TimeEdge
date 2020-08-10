using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnEnemy : MonoBehaviour
{
    public Transform Player;
    public GameObject enemyPrefabs;
    public int maxEnemy = 20;
    private int curEnemy = 0;
    private float curTime;
    private int enemyCount;
    private Vector2 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = Vector2.zero;
        curTime = 0f;
        enemyCount = 0;
    }

    void FixedUpdate()
    {
        curTime += 0.02f;
        if(curTime >= 0.1f && curEnemy < maxEnemy)
        {
            var go = GameObject.Instantiate(enemyPrefabs, transform);
            enemyCount ++;
            if (enemyCount % 2 != 0)
                go.transform.localPosition = new Vector2(-spawnPos.x, spawnPos.y);
            else
                go.transform.localPosition = new Vector2(spawnPos.x, spawnPos.y);
            spawnPos.x += 0.1f;
            go.transform.parent = null;
            go.GetComponent<Targeting>().target = Player;
            curTime = 0f;
            curEnemy++;
        }
    }
}
