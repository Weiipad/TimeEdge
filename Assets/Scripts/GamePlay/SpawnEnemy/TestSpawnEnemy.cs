using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefabs;
    private float curTime;
    private int enemyCount;
    private Vector2 spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        curTime = 0f;
        enemyCount = 0;
    }

    void FixedUpdate()
    {
        curTime += 0.02f;
        if(curTime >= 0.1f)
        {
            var go = GameObject.Instantiate(enemyPrefabs, transform);
            enemyCount ++;
            if (enemyCount % 2 != 0)
                go.transform.localPosition = new Vector2(-spawnPos.x, spawnPos.y);
            else
                go.transform.localPosition = new Vector2(spawnPos.x, spawnPos.y);
            spawnPos.x += 0.5f;
            go.transform.parent = null;
            curTime = 0f;
        }
    }
}
