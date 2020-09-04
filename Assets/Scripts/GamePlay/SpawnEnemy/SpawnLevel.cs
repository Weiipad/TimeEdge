using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    public int StartLevel = 0;
    public List<Level> levels;

    private LevelList levelList;

    private void Start()
    {
        levelList = new LevelList(levels);
    }

    public void StartSpawnLevel()
    {
        levelList.StartLevel(StartLevel);
    }

    private void Update()
    {
        if (GameStatus.CurrentGameStatus != GameStatus.GameStatusType.playing)
            return;
    }
}
