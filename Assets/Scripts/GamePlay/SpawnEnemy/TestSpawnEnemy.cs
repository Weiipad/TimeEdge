using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnEnemy : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var i in enemies)
            i?.StartAction();
    }
}
